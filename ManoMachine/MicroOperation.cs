using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManoMachine
{
    public class MicroOperation
    {
        public MicroOperation(string desc, Func<MachineState, bool> condition, Action<MachineState> apply)
        {
            Description = desc;
            Condition = condition;
            Apply = apply;
        }

        public string Description { get; set; }

        public Func<MachineState, bool> Condition { get; set; }

        public Action<MachineState> Apply { get; set; }
    }
}
