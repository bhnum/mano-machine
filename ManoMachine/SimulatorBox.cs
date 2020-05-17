using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManoMachine
{
    public partial class SimulatorBox : UserControl
    {
        public SimulatorBox(string romPath)
        {
            InitializeComponent();

            Path = romPath;
            simulator = new Simulator(romPath);
        }

        Simulator simulator;

        public string Path { get; set; }
    }
}
