using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode;
using ICSharpCode.AvalonEdit;
using Editor = ICSharpCode.AvalonEdit.TextEditor;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using System.IO;

namespace ManoMachine
{
    public partial class TextEditor : UserControl
    {
        // https://stackoverflow.com/a/17979246
        public char ErrorIndicator { get; set; } = '\u200c';

        public TextEditor(string path = "")
        {
            InitializeComponent();

            editor = new Editor();
            host.Child = editor;

            editor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            editor.FontSize = 12;
            if (Config.HighlightingDefinition != null)
                editor.SyntaxHighlighting = Config.HighlightingDefinition;

            editor.Options.IndentationSize = 8;
            editor.Options.ConvertTabsToSpaces = false;
            editor.ShowLineNumbers = true;
            editor.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            editor.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;

            if (!string.IsNullOrWhiteSpace(path))
                Open(path);
        }

        private Editor editor;

        public string Path { get; set; }

        public Editor Editor => editor;

        public bool Open(string path)
        {
            try
            {
                editor.Load(path);
                Path = path;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot open {path}! Error message:\n\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log(ex.ToString());
                return false;
            }
            return true;
        }

        public bool Save(string path)
        {
            ClearErrors();

            try
            {
                editor.Save(path);
                Path = path;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot save {path}! Error message:\n\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log(ex.ToString());
                return true;
            }
            return false;
        }

        public void ClearErrors()
        {
            while (true)
            {
                int offset = editor.Document.IndexOf(ErrorIndicator, 0, editor.Document.TextLength);
                if (offset == -1)
                    break;
                editor.Document.Replace(offset, 1, "");
            }
        }

        public void AddError(int lineNumber)
        {
            var line = editor.Document.GetLineByNumber(lineNumber);
            editor.Document.Insert(line.Offset, ErrorIndicator.ToString());
        }

        //public void ClearLabels()
        //{
            
        //}

        //public void AddLabel(string str)
        //{
            
        //}

        public void SelectLine(int lineNumber)
        {
            // https://geek-questions.github.io/articles/1340178/index.html

            //Get the line number based off the offset.
            var line = editor.Document.GetLineByNumber(lineNumber);
            //Select the text.
            editor.Select(line.Offset, line.Length);
            //Scroll the textEditor to the selected line.
            var visualTop = editor.TextArea.TextView.GetVisualTopByDocumentLine(lineNumber);
            editor.ScrollToVerticalOffset(visualTop); 
        }
    }
}
