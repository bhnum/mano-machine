using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;

namespace ManoMachine
{
    public partial class SimulatorBox : UserControl
    {
        public SimulatorBox(string romPath)
        {
            InitializeComponent();

            Path = romPath;
            simulator = new Simulator(romPath);
            simulator.Stopped += simulator_Stopped;

            this.Disposed += (sende, e) => simulator.Stop();

            var rows = new DataGridViewRow[simulator.State.Memory.Length];
            for (int i = 0; i < simulator.State.Memory.Length; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(memoryGridView);
                row.Cells[0].Value = "";
                row.Cells[1].Value = $"{i:X3}";
                row.Cells[2].Value = $"{simulator.State.Memory[i]:X4}";
                row.Cells[3].Value = simulator.Comments[i];
                rows[i] = row;
            }
            memoryGridView.Rows.AddRange(rows);
            memoryGridView.GetType().GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic).SetValue(memoryGridView, true);

            binders = new List<Binder<MachineState>>
            {
                new HexBinder<MachineState>(s => s.SC, scTextBox, 0xFF),
                new HexBinder<MachineState>(s => s.PC, pcTextBox, 0xFFF),
                new HexBinder<MachineState>(s => s.AR, arTextBox, 0xFFF),
                new HexBinder<MachineState>(s => s.IR, irTextBox, 0xFFFF),
                new HexBinder<MachineState>(s => s.DR, drTextBox, 0xFFFF),
                new HexBinder<MachineState>(s => s.AC, acTextBox, 0xFFFF),
                new HexBinder<MachineState>(s => s.INPR, inprTextBox, 0xFF),
                new HexBinder<MachineState>(s => s.OUTR, outrTextBox, 0xFF),
                new HexBinder<MachineState>(s => s.TR, trTextBox, 0xFFFF),

                new BooleanBinder<MachineState>(s => s.S, sCheckBox),
                new BooleanBinder<MachineState>(s => s.I, iCheckBox),
                new BooleanBinder<MachineState>(s => s.E, eCheckBox),
                new BooleanBinder<MachineState>(s => s.FGI, fgiCheckBox),
                new BooleanBinder<MachineState>(s => s.FGO, fgoCheckBox),
                new BooleanBinder<MachineState>(s => s.R, rCheckBox),
                new BooleanBinder<MachineState>(s => s.IEN, ienCheckBox),
            };

            UpdateNextuOp();
            UpdateValues();

            initialized = true;
        }

        bool updating = false;
        bool initialized = false;
        Simulator simulator;

        Queue<byte> inputBuffer = new Queue<byte>();
        List<Binder<MachineState>> binders;

        public string Path { get; set; }

        void SetEnabled(bool enabled)
        {
            for (int i = 0; i < binders.Count; i++)
            {
                binders[i].Control.Enabled = enabled;
            }
            resetButton.Enabled = tickButton.Enabled = nextButton.Enabled = enabled;

            contentColumn.ReadOnly = !enabled;
        }

        void UpdateValues()
        {
            updating = true;
            for (int i = 0; i < binders.Count; i++)
            {
                binders[i].Update(simulator.State);
            }

            int currentRow = simulator.State.PC;

            for (int i = 0; i < simulator.State.Memory.Length; i++)
            {
                memoryGridView.Rows[i].Cells[contentColumn.Name].Value = $"{simulator.State.Memory[i]:X4}";
                memoryGridView.Rows[i].Cells[pcPointerColumn.Name].Value = i == currentRow ? ">" : " ";
            }

            memoryGridView.ClearSelection();
            memoryGridView.Rows[currentRow].Selected = true;
            if (Math.Abs(memoryGridView.FirstDisplayedScrollingRowIndex - currentRow) > 
                memoryGridView.Height / memoryGridView.Rows[0].Height - 1)
                memoryGridView.FirstDisplayedScrollingRowIndex = currentRow > 5 ? currentRow - 5 : 0;

            updating = false;
        }

        void ApplyValues()
        {
            if (updating)
                return;

            for (int i = 0; i < binders.Count; i++)
            {
                binders[i].Apply(simulator.State);
            }
        }

        void UpdateNextuOp(string text = null)
        {
            uOpsList.Items.Add(nextuOpBox.Text);
            uOpsList.SelectedIndex = uOpsList.Items.Count - 1;

            if (text != null)
            {
                nextuOpBox.Text = text;
                return;
            }

            var nextuops = simulator.NextuOps();
            string[] descs = new string[nextuops.Count];
            for (int i = 0; i < nextuops.Count; i++)
                descs[i] = nextuops[i].Description;
            nextuOpBox.Text = string.Join(", ", descs);
        }

        private void value_Changed(object sender, EventArgs e)
        {
            if (!initialized)
                return;

            ApplyValues();
        }

        private void simulator_Stopped()
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
                Invoke((Action)simulator_Stopped);
            else
            {
                SetEnabled(true);

                runButton.Checked = false;
                runButton.Text = "Start";

                UpdateValues();
                UpdateNextuOp("- Machine Stopped");
                UpdateNextuOp();
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                simulator.Reset();
                inputBuffer.Clear();

                nextuOpBox.Text = "- Machine Reset";
                UpdateNextuOp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error reseting machine!\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log(ex.ToString());
            }
        }

        private void tickButton_Click(object sender, EventArgs e)
        {
            try
            {
                simulator.Tick();
                UpdateNextuOp();
                UpdateValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log(ex.ToString());
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            try
            {
                do
                {
                    simulator.Tick();
                    UpdateNextuOp();
                }
                while (simulator.State.SC != 0 && simulator.State.S);
                UpdateValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log(ex.ToString());
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (!initialized)
                return;

            try
            {
                if (runButton.Checked)
                {
                    simulator.Stop();
                }
                else
                {
                    simulator.Run();

                    runButton.Checked = true;
                    runButton.Text = "Stop";

                    SetEnabled(false);
                    nextuOpBox.Text = "- Machine is Running";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log(ex.ToString());
            }
        }

        private void memoryGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!initialized || updating)
                return;

            if (simulator.Running)
            {
                MessageBox.Show(this, "Error modifying memory! Machine is running.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (e.ColumnIndex != contentColumn.Index || e.RowIndex < 0)
                return;

            ushort value;
            try
            {
                value = Convert.ToUInt16((string)memoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, 16);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Content entered is not a valid hexadecimal integer",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            updating = true;
            memoryGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                (simulator.State.Memory[e.RowIndex] = value).ToString("X4");
            updating = false;
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            if (!initialized)
                return;

            lock (simulator.State)
            {
                ticksCountLabel.Text = $"{simulator.Ticks} Ticks" + 
                    (simulator.State.S ? "" : ", Machine Halted");

                if (inputBuffer.Count > 0)
                    if (!simulator.State.FGI)
                    {
                        simulator.State.FGI = true;
                        simulator.State.INPR = inputBuffer.Dequeue();
                    }

                if (!simulator.State.FGO)
                {
                    char output = (char)simulator.State.OUTR;
                    if (output == '\0') ;
                    else if (output == '\n')
                        outputBox.AppendText("\r\n");
                    else
                        outputBox.AppendText(output.ToString());
                    simulator.State.FGO = true;
                }
            }
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;

            switch (e.KeyCode)
            {
                case Keys.Back:
                    inputBox.AppendText("⌫");
                    inputBuffer.Enqueue((byte)'\b');
                    break;

                case Keys.Delete:
                    inputBox.AppendText("⌦");
                    inputBuffer.Enqueue(127);
                    break;

                case Keys.Escape:
                    inputBox.AppendText("⎋");
                    inputBuffer.Enqueue(27);
                    break;

                case Keys.Up:
                    inputBox.AppendText("↑");
                    inputBuffer.Enqueue(128);
                    break;

                case Keys.Down:
                    inputBox.AppendText("↓");
                    inputBuffer.Enqueue(129);
                    break;

                case Keys.Left:
                    inputBox.AppendText("←");
                    inputBuffer.Enqueue(130);
                    break;

                case Keys.Right:
                    inputBox.AppendText("→");
                    inputBuffer.Enqueue(131);
                    break;

                default:
                    if (e.KeyCode == Keys.Enter)
                        inputBuffer.Enqueue((byte)'\n');
                    if (e.KeyCode == Keys.Tab)
                        inputBuffer.Enqueue((byte)'\t');

                    e.SuppressKeyPress = false;
                    e.Handled = false;
                    break;
            }
        }

        private void inputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check for ascii chars
            if (e.KeyChar >= 32 && e.KeyChar <= 126)
            {
                inputBuffer.Enqueue((byte)e.KeyChar);
            }
            inputBox.SelectionLength = 0;
            inputBox.SelectionStart = inputBox.Text.Length;
        }
    }
}
