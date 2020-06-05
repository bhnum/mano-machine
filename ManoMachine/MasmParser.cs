using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManoMachine
{
    public class ParserError : Exception
    {
        public ParserError(string message, int linenumber) : base(message)
        {
            LineNumber = linenumber;
        }

        public int LineNumber { get; set; }
    }

    public class MasmParser
    {
        static readonly List<string> MemoryOpcodes = new List<string> {
            "and", "add", "lda", "sta", "bun", "bsa", "isz",
        };
        static readonly List<string> RegisterOpcodes = new List<string> {
            "cla", "cle", "cma", "cir", "cil", "inc", "spa", "sna", "sza", "sze", "hlt",
        };
        static readonly List<string> IOOpcodes = new List<string> {
            "inp", "out", "ski", "sko", "ion", "iof",
        };
        static readonly List<string> Directives = new List<string> {
            "org", "dec", "hex", "end",
        };

        public MasmParser(string path)
        {
            int i = 0;
            foreach (var temp_line in File.ReadLines(path))
            {
                string line = temp_line;

                // remove comments and empty lines
                if (line.Contains('/'))
                    line = temp_line.Substring(0, temp_line.IndexOf('/'));

                if (!string.IsNullOrWhiteSpace(line))
                    lines.Add((i, line));
                i++;

                OriginalLines.Add(temp_line);
            }
        }

        public MasmParser(TextReader reader)
        {
            for (int i = 0; ; i++)
            {
                string temp_line = reader.ReadLine();
                if (temp_line == null)
                    break;

                string line = temp_line;

                // remove comments and empty lines
                if (line.Contains('/'))
                    line = temp_line.Substring(0, temp_line.IndexOf('/'));

                if (!string.IsNullOrWhiteSpace(line))
                    lines.Add((i, line));

                OriginalLines.Add(temp_line);
            }
        }

        private List<(int linenumber, string text)> lines = new List<(int linenumber, string text)>();
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public List<string> OriginalLines { get; set; } = new List<string>();

        private bool IsLabel(string str)
        {
            if (!IsHex(str))
                return true;

            if (!str.TrimStart().StartsWith("0"))
                return true;

            return false;
        }

        private bool IsHex(string str)
        {
            try
            {
                Convert.ToInt32(str, 16);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool IsDec(string str)
        {
            try
            {
                Convert.ToInt32(str, 10);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public ushort FromHex(string str)
        {
            return unchecked((ushort)Convert.ToInt32(str, 16));
        }

        public ushort FromDec(string str)
        {
            return unchecked((ushort)Convert.ToInt32(str, 10));
        }

        public void Reset()
        {
            Index = 0;
        }

        public bool Advance(out int linenumber, out string label, out string opcode, out string address, out bool pureaddress, out bool directive, out bool indirect)
        {
            linenumber = -1;
            label = null;
            opcode = null;
            address = null;
            pureaddress = false;
            directive = false;
            indirect = false;

            if (index >= lines.Count)
                return false;

            linenumber = lines[index].linenumber;
            var splits = lines[index].text.Split(new[] { ',' });
            if (splits.Length > 2)
                throw new ParserError("Only one jump label can be declared in a line.", linenumber);

            string instruction;
            if (splits.Length == 1)
                instruction = splits[0].Trim();
            else
            {
                label = splits[0].Trim();
                instruction = splits[1].Trim();
            }

            if (!string.IsNullOrWhiteSpace(label))
            {
                if (label.Any(c => char.IsWhiteSpace(c)))
                    throw new ParserError("Label cannot have white space", linenumber);
            }
            else
                label = null;

            if (!string.IsNullOrWhiteSpace(instruction))
            {
                var isplits = instruction.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                opcode = isplits[0].ToLower();
                if (MemoryOpcodes.Contains(opcode))
                {
                    if (isplits.Length > 3 || isplits.Length == 1)
                        throw new ParserError("Invalid number of operands", linenumber);

                    address = isplits[1];
                    pureaddress = !IsLabel(address);

                    if (isplits.Length == 3)
                    {
                        if (isplits[2].ToLower() == "i")
                            indirect = true;
                        else
                            throw new ParserError($"Invalid indirect memory opcode indication {isplits[2]}", linenumber);
                    }
                }
                else if (RegisterOpcodes.Contains(opcode))
                {
                    if (isplits.Length > 1)
                        throw new ParserError("Invalid number of operands", linenumber);
                }
                else if (IOOpcodes.Contains(opcode))
                {
                    if (isplits.Length > 1)
                        throw new ParserError("Invalid number of operands", linenumber);
                }
                else if (Directives.Contains(opcode))
                {
                    if (opcode == "end")
                    {
                        if (isplits.Length > 1)
                            throw new ParserError("Invalid number of operands", linenumber);
                    }
                    else
                    {
                        if (isplits.Length > 2)
                            throw new ParserError("Invalid number of operands", linenumber);

                        address = isplits[1];
                        if (opcode == "org")
                        {
                            if (!IsHex(address))
                                throw new ParserError("ORG cannot have a label operand", linenumber);
                            pureaddress = true;
                        }
                        if (opcode == "hex")
                            pureaddress = IsHex(address);
                        else if (opcode == "dec")
                            pureaddress = IsDec(address);
                    }
                    directive = true;
                }
                else
                    throw new ParserError($"Nonexistent opcode or directive {isplits[0]}", linenumber);
            }

            index++;
            return true;
        }
    }
}
