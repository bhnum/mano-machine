using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ManoMachine
{
    public class Config
    {
        static Config()
        {
            try
            {
                using (var fs = File.Open(HighlighterXshdPath, FileMode.Open))
                {
                    using (var reader = new XmlTextReader(fs))
                    {
                        HighlightingDefinition = HighlightingLoader.LoadXshd(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot Load {HighlighterXshdPath}! Syntax highlighting will be unavailable." +
                    $"\nError is logged in {LogPath}. Error message:\n\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log(ex.ToString());
            }
        }

        public static string LogPath { get; set; } = "data/log.txt";
        public static string HighlighterXshdPath { get; set; } = "data/highlights.xshd";

        public static XshdSyntaxDefinition HighlightingDefinition { get; set; }
    }
}
