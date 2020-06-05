using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManoMachine
{
    public partial class IDEForm : Form
    {
        public IDEForm()
        {
            InitializeComponent();

            tabControl.TabPages.Clear();
            newMenuItem_Click(this, EventArgs.Empty);
        }

        bool GetCurrentEditor(out TextEditor editor)
        {
            editor = null;
            var currentTab = tabControl.SelectedTab;
            if (currentTab == null)
                return false;
            if (currentTab.Tag as string != "editor")
                return false;

            editor = (TextEditor)currentTab.Controls["editor"];
            return true;
        }

        bool DoSaveAs(TextEditor editor)
        {
            saveDialog.FileName = editor.Path;
            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                string path = saveDialog.FileName;
                editor.Path = path;

                tabControl.SelectedTab.Text = Path.GetFileName(path);
                tabControl.SelectedTab.ToolTipText = path;

                editor.Save(path);
                return true;
            }
            return false;
        }

        void StartSemulation(string path)
        {
            TabPage newpage = new TabPage($"Simulation of {Path.GetFileName(path)}") {
                ImageIndex = 0,
                Tag = "simulator",
            };
            newpage.Controls.Add(new SimulatorBox(path) {
                Name = "simulator",
                Dock = DockStyle.Fill
            });

            tabControl.TabPages.Add(newpage);
            tabControl.SelectedTab = newpage;
            errorsPanel.Hide();
        }

        private void newMenuItem_Click(object sender, EventArgs e)
        {
            TabPage newpage = new TabPage("Untitled") {
                ImageIndex = 0,
                Tag = "editor",
            };
            newpage.Controls.Add(new TextEditor() { 
                Name = "editor",
                Dock = DockStyle.Fill
            });

            tabControl.TabPages.Add(newpage);
            tabControl.SelectedTab = newpage;
            errorsPanel.Show();
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog(this) == DialogResult.OK)
            {
                string path = openDialog.FileName;

                TabPage newpage = new TabPage(Path.GetFileName(path)) {
                    ImageIndex = 0,
                    ToolTipText = path,
                    Tag = "editor",
                };
                newpage.Controls.Add(new TextEditor(path) {
                    Name = "editor", 
                    Dock = DockStyle.Fill
                });

                tabControl.TabPages.Add(newpage);
                tabControl.SelectedTab = newpage;
                errorsPanel.Show();
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCurrentEditor(out var editor))
                return;

            string path = editor.Path;
            if (string.IsNullOrWhiteSpace(path))
                DoSaveAs(editor);
            else
                editor.Save(path);
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCurrentEditor(out var editor))
                return;

            DoSaveAs(editor);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var current = tabControl.SelectedTab;
            if (current == null)
                return;

            tabControl.TabPages.Remove(current);
            tabControl_SelectedIndexChanged(sender, e);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                int closingTabIndex = -1;
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    var tabRect = tabControl.GetTabRect(i);
                    var closeRect = new Rectangle(tabRect.Location + new Size(tabControl.Padding), imageList.ImageSize);
                    if (closeRect.Contains(e.Location))
                    {
                        closingTabIndex = i;
                        break;
                    }
                }

                if (closingTabIndex >= 0)
                {
                    Console.WriteLine("Close clicked on tab {0}", closingTabIndex);
                    tabControl.TabPages.RemoveAt(closingTabIndex);
                    tabControl_SelectedIndexChanged(sender, e);
                }
            }
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left && tabControl.RectangleToScreen(
                new Rectangle(new Point(), tabControl.Size)).Contains(MousePosition))
            {
                int closingTabIndex = -1;
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    var tabRect = tabControl.GetTabRect(i);
                    var closeRect = new Rectangle(tabRect.Location + new Size(tabControl.Padding), imageList.ImageSize);
                    if (closeRect.Contains(tabControl.PointToClient(MousePosition)))
                    {
                        closingTabIndex = i;
                        break;
                    }
                }

                if (closingTabIndex >= 0)
                {
                    Console.WriteLine("Close clicked anicipation on tab {0} in selecting tab {1}", closingTabIndex, e.TabPageIndex);
                    e.Cancel = true;
                }
            }
        }

        private void assembleAndRunMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetCurrentEditor(out var editor))
                return;

            string path = editor.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                if (!DoSaveAs(editor))
                    return;
            }
            else
                editor.Save(path);

            string output = Path.ChangeExtension(path, ".mrom");
            statusLabel.Text = $"Assembling {output}";

            Massembler bler = new Massembler(editor.Path);
            bool success = bler.Assemble(out var errors, output);

            PopulateErrors(editor, errors);

            if (success)
            {
                statusLabel.Text = $"Assemble succeeded - {errors.Count} errors";

                StartSemulation(output);
            }
            else
            {
                editor.SelectLine(errors[0].LineNumber + 1);

                statusLabel.Text = $"Assemble failed - {errors.Count} errors";
            }
        }

        void PopulateErrors(TextEditor editor, List<ParserError> errors)
        {
            errorsGridView.Rows.Clear();
            for (int i = 0; i < errors.Count; i++)
                errorsGridView.Rows.Add(errors[i].Message, errors[i].LineNumber + 1);
            errorsGridView.Tag = tabControl.SelectedTab;

            editor.ClearErrors();
            for (int i = 0; i < errors.Count; i++)
                editor.AddError(errors[i].LineNumber + 1);
        }

        private void runFromFileMenuItem_Click(object sender, EventArgs e)
        {
            if (openMromDialog.ShowDialog(this) == DialogResult.OK)
            {
                string path = openMromDialog.FileName;

                StartSemulation(path);
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void errorsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            TabPage tab = (TabPage)errorsGridView.Tag;
            if (tab == null)
                return;

            tabControl.SelectedTab = tab;

            int linenumber = (int)errorsGridView.Rows[e.RowIndex].Cells[lineCulomn.Name].Value;
            var editor = (TextEditor)tab.Controls["editor"];
            editor.SelectLine(linenumber);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorsPanel.Visible = GetCurrentEditor(out _);
        }

        private void highlightTimer_Tick(object sender, EventArgs e)
        {
            if (!GetCurrentEditor(out var editor))
                return;

            List<string> labels = new List<string>();
            using (var reader = new StringReader(editor.Editor.Text))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        break;

                    if (line.Contains('/'))
                        line = line.Substring(0, line.IndexOf('/'));

                    if (!line.Contains(','))
                        continue;
                    var label = line.Substring(0, line.IndexOf(','));
                    if (!string.IsNullOrWhiteSpace(label))
                        labels.Add(label);
                }
            }
            editor.SetLabels(labels);
        }
    }
}
