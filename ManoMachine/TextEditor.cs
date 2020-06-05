using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using ICSharpCode;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

using Editor = ICSharpCode.AvalonEdit.TextEditor;

namespace ManoMachine
{
    public partial class TextEditor : UserControl
    {
        // Implements AvalonEdit ICompletionData interface to provide the entries in the
        // completion drop down.
        public class MasmCompletionData : ICompletionData
        {
            public MasmCompletionData(string text, string description = "", double priority = 0.0)
            {
                Text = text;
                Description = description;
                Priority = priority;
            }

            public System.Windows.Media.ImageSource Image
            {
                get { return null; }
            }

            public string Text { get; set; }
            public object Description { get; set; }
            public double Priority { get; set; }

            // Use this property if you want to show a fancy UIElement in the list.
            public object Content
            {
                get { return this.Text; }
            }

            public void Complete(TextArea textArea, ISegment completionSegment,
                EventArgs insertionRequestEventArgs)
            {
                textArea.Document.Replace(completionSegment, this.Text);
            }
        }

        static readonly Dictionary<string, string> OpcodeDescriptions = new Dictionary<string, string>
        {
            ["ORG"] = "Hexadecimal number as the memory location for the following instruction",
            ["DEC"] = "Denotes the end of symbolic program",
            ["HEX"] = "Signed decimal number to be converted to binary",
            ["END"] = "Hexadecimal number to be converted to binary",
            ["AND"] = "AND memory word to AC",
            ["ADD"] = "Add memory word to AC",
            ["LDA"] = "Load memory word to AC",
            ["STA"] = "Store content of AC in memory",
            ["BUN"] = "Branch unconditionally",
            ["BSA"] = "Branch and save return address",
            ["ISZ"] = "Increment and skip if zero",
            ["CLA"] = "Clear AC",
            ["CLE"] = "Clear E",
            ["CMA"] = "Complement AC",
            ["CME"] = "Complement E",
            ["CIR"] = "Circulate right AC and E",
            ["CIL"] = "Circulate left AC and E",
            ["INC"] = "Increment AC",
            ["SPA"] = "Skip next instruction if AC positive",
            ["SNA"] = "Skip next instruction if AC negative",
            ["SZA"] = "Skip next instruction if AC zero",
            ["SZE"] = "Skip next instruction if E is 0",
            ["HLT"] = "Halt computer",
            ["INP"] = "Input character to AC",
            ["OUT"] = "Output character from AC",
            ["SKI"] = "Skip on input flag",
            ["SKO"] = "Skip on output flag",
            ["ION"] = "Interrupt on",
            ["IOF"] = "Interrupt off",
        };

        public TextEditor(string path = "")
        {
            InitializeComponent();

            editor = new Editor();
            host.Child = editor;

            editor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            editor.FontSize = 12;
            if (Config.HighlightingDefinition != null)
                editor.SyntaxHighlighting = HighlightingLoader.Load(
                    Config.HighlightingDefinition, HighlightingManager.Instance);

            editor.Options.IndentationSize = 8;
            editor.Options.ConvertTabsToSpaces = false;
            editor.ShowLineNumbers = true;
            editor.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            editor.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;

            if (!string.IsNullOrWhiteSpace(path))
                Open(path);

            editor.TextArea.TextEntering += editor_TextArea_TextEntering;
            editor.TextArea.TextEntered += editor_TextArea_TextEntered;
        }

        private Editor editor;
        CompletionWindow completionWindow;
        List<string> labels = new List<string>();

        public string Path { get; set; }

        public Editor Editor => editor;

        // https://stackoverflow.com/a/17979246
        public char ErrorIndicator { get; set; } = '\u200c';

        private void editor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Text) && e.Text != "," && completionWindow == null)
            {
                var line = editor.Document.GetLineByOffset(editor.CaretOffset);
                if (editor.CaretOffset - line.Offset <= 0)
                    return;

                string before = editor.Document.GetText(line.Offset, editor.CaretOffset - line.Offset);
                if (!before.Contains('/'))
                    ShowCompletionWindow();
            }
        }

        private void editor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (completionWindow != null)
            {
                if (e.Text == ",")
                {
                    completionWindow.Close();
                }
                if (string.IsNullOrWhiteSpace(e.Text))
                {
                    // Whenever white space is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // Do not set e.Handled=true.
            // We still want to insert the character that was typed.
        }

        void ShowCompletionWindow()
        {
            // Open code completion after the user has pressed anything:
            completionWindow = new CompletionWindow(editor.TextArea);
            completionWindow.StartOffset--;
            completionWindow.CloseWhenCaretAtBeginning = true;

            IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
            foreach (var item in OpcodeDescriptions)
                data.Add(new MasmCompletionData(item.Key, item.Value, 2));
            for (int i = 0; i < labels.Count; i++)
                data.Add(new MasmCompletionData(labels[i], "Label"));

            completionWindow.Show();

            completionWindow.Closed +=
                (sender2, e2) => completionWindow = null;
        }

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

        public bool Save(string path, bool temp = false)
        {
            try
            {
                string contents = editor.Text;
                contents = contents.Replace(ErrorIndicator.ToString(), "");
                File.WriteAllText(path, contents, Encoding.UTF8);

                //editor.Save(path);
                if (!temp)
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
            editor.Document.UndoStack.StartUndoGroup();
            while (true)
            {
                int offset = editor.Document.IndexOf(ErrorIndicator, 0, editor.Document.TextLength);
                if (offset == -1)
                    break;

                editor.Document.Replace(offset, 1, "");
            }
            editor.Document.UndoStack.EndUndoGroup();
        }

        public void AddError(int lineNumber)
        {
            var line = editor.Document.GetLineByNumber(lineNumber);
            editor.Document.Insert(line.Offset, ErrorIndicator.ToString());
        }

        public void SetLabels(List<string> labels)
        {
            if (Config.HighlightingDefinition == null)
                return;

            var keys = (XshdKeywords)((XshdRuleSet)Config.HighlightingDefinition.Elements.First(
                x => (x as XshdRuleSet)?.Name == "LabelsRuleSet")).Elements[0];
            keys.Words.Clear();
            keys.Words.Add("SomeVeryLongName_______________________________________________________");
            labels.ForEach(s => keys.Words.Add(s));
            editor.SyntaxHighlighting = HighlightingLoader.Load(Config.HighlightingDefinition, HighlightingManager.Instance);

            this.labels = labels;
        }

        //public void ClearLabels()
        //{
        //    var ruleset = editor.SyntaxHighlighting.GetNamedRuleSet("LabelsRuleSet");
        //    ruleset.Rules.Clear();
        //}

        //public void AddLabel(string name)
        //{
        //    var color = editor.SyntaxHighlighting.GetNamedColor("Label");
        //    var ruleset = editor.SyntaxHighlighting.GetNamedRuleSet("LabelsRuleSet");
        //    ruleset.Rules.Add(new HighlightingRule() {
        //        Color = color,
        //        Regex = new Regex($"\\b(?>{Regex.Escape(name)})\\b",
        //            RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant)
        //    });
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
