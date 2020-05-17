using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManoMachine
{
    public class LabelTable
    {
        public LabelTable(int initialAddress = 0)
        {
            CurrentAddress = initialAddress;
            Table = new Dictionary<string, int>();
        }

        public int CurrentAddress { get; set; }

        public Dictionary<string, int> Table { get; set; }

        public int this[string label]
        {
            get => Table[label];
        }

        public void Advance()
        {
            CurrentAddress++;
        }

        public void SetAddress(int address)
        {
            CurrentAddress = address;
        }

        public bool Contains(string label)
        {
            return Table.ContainsKey(label);
        }

        public void AddLabel(string label)
        {
            Table.Add(label, CurrentAddress);
        }

        public void Clear()
        {
            Table.Clear();
        }
    }
}
