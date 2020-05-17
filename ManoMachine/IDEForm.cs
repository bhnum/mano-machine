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
            tabControl.Show();
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
                tabControl.Show();
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            var current = tabControl.SelectedTab;
            if (current == null)
                return;
            if (current.Tag as string != "editor")
                return;

            var editor = (TextEditor)current.Controls["editor"];

            string path = editor.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                saveDialog.FileName = "Untitled";
                if (saveDialog.ShowDialog(this) != DialogResult.OK)
                    return;
                path = saveDialog.FileName;
                editor.Path = path;

                current.Text = Path.GetFileName(path);
                current.ToolTipText = path;
            }
            editor.Save(path);
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            var current = tabControl.SelectedTab;
            if (current == null)
                return;
            if (current.Tag as string != "editor")
                return;

            var editor = (TextEditor)current.Controls["editor"];

            saveDialog.FileName = editor.Path;
            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                string path = saveDialog.FileName;
                editor.Path = path;

                current.Text = Path.GetFileName(path);
                current.ToolTipText = path;

                editor.Save(path);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var current = tabControl.SelectedTab;
            if (current == null)
                return;

            tabControl.TabPages.Remove(current);
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
            var current = tabControl.SelectedTab;
            if (current == null)
                return;
            if (current.Tag as string != "editor")
                return;

            var editor = (TextEditor)current.Controls["editor"];

            string path = editor.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                saveDialog.FileName = "Untitled";
                if (saveDialog.ShowDialog(this) != DialogResult.OK)
                    return;
                path = saveDialog.FileName;
                editor.Path = path;

                current.Text = Path.GetFileName(path);
                current.ToolTipText = path;
            }
            editor.Save(path);

            string output = Path.ChangeExtension(path, ".mrom");
            statusLabel.Text = $"Assembling {output}";
            UseWaitCursor = true;

            Massembler bler = new Massembler(editor.Path);
            if (bler.Assemble(out var errors, output))
            {
                statusLabel.Text = "Assembled successfully";

                // run sim
            }
            else
            {
                editor.SelectLine(errors[0].LineNumber + 1);

                statusLabel.Text = "Assemble failed";
            }

            PopulateErrorList(errors);
            errorsGridView.Tag = current;

            UseWaitCursor = false;
        }

        void PopulateErrorList(List<ParserError> errors)
        {
            errorsGridView.Rows.Clear();
            for (int i = 0; i < errors.Count; i++)
                errorsGridView.Rows.Add(errors[i].Message, errors[i].LineNumber + 1);
        }

        private void runFromFileMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
            var current = tabControl.SelectedTab;

            bool showErrors = current != null && current.Tag as string != "editor";

            errorsPanel.Visible = showErrors;
        }

        private void errorsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TabPage tab = (TabPage)errorsGridView.Tag;
            if (tab == null)
                return;

            tabControl.SelectedTab = tab;

            int linenumber = (int)errorsGridView.Rows[e.RowIndex].Cells[lineCulomn.Name].Value;
            var editor = (TextEditor)tab.Controls["editor"];
            editor.SelectLine(linenumber);
        }
    }
}
