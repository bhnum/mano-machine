using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace ManoMachine
{
    public abstract class Binder<T>
    {
        protected PropertyInfo GetPropertyInfo<U>(Expression<Func<T, U>> expression)
        {
            if (expression.Body is UnaryExpression)
                return (PropertyInfo)((MemberExpression)((UnaryExpression)expression.Body).Operand).Member;
            else
                return (PropertyInfo)((MemberExpression)expression.Body).Member;
        }

        public abstract Control Control { get; }

        public abstract void Update(T target);
        public abstract void Apply(T target);
    }

    public class BooleanBinder<T> : Binder<T>
    {
        public BooleanBinder(Expression<Func<T, bool>> expression, CheckBox checkBox)
        {
            Expression = expression;
            CheckBox = checkBox;
        }

        public CheckBox CheckBox { get; set; }
        public Expression<Func<T, bool>> Expression { get; set; }

        public override Control Control => CheckBox;

        public override void Apply(T target)
        {
            GetPropertyInfo(Expression).SetValue(target, CheckBox.Checked);
        }

        public override void Update(T target)
        {
            CheckBox.Checked = (bool)GetPropertyInfo(Expression).GetValue(target);
        }
    }

    public class HexBinder<T> : Binder<T>
    {
        public HexBinder(Expression<Func<T, uint>> expression, TextBox textBox, uint maxValue)
        {
            Expression = expression;
            TextBox = textBox;
            MaxValue = maxValue;
        }

        public TextBox TextBox { get; set; }
        public uint MaxValue { get; set; }
        public Expression<Func<T, uint>> Expression { get; set; }

        public override Control Control => TextBox;

        public override void Apply(T target)
        {
            uint value;
            try
            {
                value = Convert.ToUInt32(TextBox.Text.Trim(), 16);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Value of \"{GetPropertyInfo(Expression).Name}\" " +
                    $"must be a hexadecimal unsigned integer", ex);
            }

            if (value > MaxValue)
                throw new InvalidCastException($"Value of \"{GetPropertyInfo(Expression).Name}\" " +
                    $"must be less than {MaxValue}");

            var pinfo = GetPropertyInfo(Expression);
            pinfo.SetValue(target, Convert.ChangeType(value, pinfo.PropertyType));
        }

        public override void Update(T target)
        {
            int length = (int)(Math.Ceiling(Math.Log(MaxValue, 16)) + 0.5);
            TextBox.Text = string.Format($"{{0:X{length}}}", 
                Convert.ToUInt32(GetPropertyInfo(Expression).GetValue(target)));
        }
    }
}
