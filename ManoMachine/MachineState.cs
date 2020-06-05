using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManoMachine
{
    public class MachineState
    {
        public MachineState()
        {
            Memory = new ushort[0x1000];
        }

        public ushort[] Memory { get; set; }

        public byte SC { get; set; }
        public ushort PC { get; set; } = 0x100;
        public ushort AR { get; set; }
        public ushort IR { get; set; }
        public ushort DR { get; set; }
        public ushort AC { get; set; }
        public ushort TR { get; set; }
        public byte INPR { get; set; }
        public byte OUTR { get; set; }

        public bool I { get; set; }
        public bool S { get; set; } = true;
        public bool E { get; set; }
        public bool R { get; set; }
        public bool IEN { get; set; }
        public bool FGI { get; set; }
        public bool FGO { get; set; } = true;

        // helpers
        public byte D => (byte)((IR >> 12) & 0b111);
        public bool B(int i) => (IR & (1u << i)) != 0;
        public bool r => D == 7 && !I && SC == 3;
        public bool p => D == 7 && I && SC == 3;
    }
}
