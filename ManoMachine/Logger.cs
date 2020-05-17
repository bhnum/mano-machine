using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManoMachine
{
    public class Logger
    {
        public static void Log(string text)
        {
            var writer = File.AppendText(Config.LogPath);
            writer.WriteLine($"+ {DateTime.Now,20} :");
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
