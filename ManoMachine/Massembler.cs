﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManoMachine
{
    public class Massembler
    {
        static readonly Dictionary<string, ushort> MemoryOpcodes = new Dictionary<string, ushort> {
            ["and"] = 0x0000, 
            ["add"] = 0x1000, 
            ["lda"] = 0x2000, 
            ["sta"] = 0x3000,
            ["bun"] = 0x4000, 
            ["bsa"] = 0x5000, 
            ["isz"] = 0x6000,
        };

        static readonly Dictionary<string, ushort> RegisterOpcodes = new Dictionary<string, ushort> {
            ["cla"] = 0x7800,
            ["cle"] = 0x7400,
            ["cma"] = 0x7200,
            ["cme"] = 0x7100,
            ["cir"] = 0x7080,
            ["cil"] = 0x7040,
            ["inc"] = 0x7020, 
            ["spa"] = 0x7010,
            ["sna"] = 0x7008,
            ["sza"] = 0x7004,
            ["sze"] = 0x7002,
            ["hlt"] = 0x7001,
        };

        static readonly Dictionary<string, ushort> IOOpcodes = new Dictionary<string, ushort> {
            ["inp"] = 0xF800,
            ["out"] = 0xF400,
            ["ski"] = 0xF200,
            ["sko"] = 0xF100,
            ["ion"] = 0xF080,
            ["iof"] = 0xF040,
        };

        public Massembler(string path)
        {
            parser = new MasmParser(path);
            table = new LabelTable();
        }

        MasmParser parser;
        LabelTable table;

        public int MaxErrors { get; set; } = 100;
        public int MaxProgramSize { get; set; } = 1 << 12 - 1;

        public LabelTable Table { get; set; }

        public bool PassOne(out List<ParserError> errors)
        {
            table.Clear();
            parser.Reset();
            errors = new List<ParserError>();
            bool endoccurred = false;

            while (true)
            {
                int linenumber;
                string label, opcode, address;
                bool directive, pureaddress;
                try
                {
                    if (!parser.Advance(out linenumber, out label, out opcode, out address, out pureaddress, out directive, out _))
                        break;
                }
                catch (ParserError er)
                {
                    errors.Add(er);

                    if (errors.Count < MaxErrors)
                    {
                        parser.Index++;
                        continue;
                    }
                    else
                        break;
                }

                if (errors.Count > 0)
                    continue;


                if (label != null)
                {
                    if (table.Contains(label))
                    {
                        errors.Add(new ParserError($"Label {label} already exists", linenumber));
                        continue;
                    }
                    table.AddLabel(label);
                }

                if (opcode != null && (!directive || opcode == "hex" || opcode == "dec"))
                    table.Advance();

                if (opcode == "org")
                {
                    if (pureaddress)
                        table.SetAddress(Convert.ToUInt16(address, 16));
                    else
                    {
                        errors.Add(new ParserError($"ORG cannot have a label operand", linenumber));
                        continue;
                    }
                }

                if (opcode == "end")
                {
                    endoccurred = true;
                    break;
                }
            }

            if (!endoccurred)
            {
                errors.Add(new ParserError("END directive not found", parser.Index - 1));
            }

            if (table.CurrentAddress >= MaxProgramSize)
                errors.Add(new ParserError($"Program size {table.CurrentAddress} exceeds memory limit {MaxProgramSize}", parser.Index - 1));

            return errors.Count == 0;
        }

        public bool Assemble(out List<ParserError> errors, string output)
        {
            if (!PassOne(out errors))
                return false;

            int last_pc = table.CurrentAddress;

            int pc = 0;
            parser.Reset();
            using (StreamWriter writer = new StreamWriter(output, false, Encoding.UTF8))
            {
                while (parser.Advance(out int linenumber, out _, out var opcode, out var address, out var pureaddress, out var directive, out var indirect))
                {
                    if (opcode == null)
                        continue;

                    ushort instruction = 0;
                    if (MemoryOpcodes.ContainsKey(opcode))
                    {
                        ushort resolvedAddress = 0;
                        if (pureaddress)
                            resolvedAddress = Convert.ToUInt16(address, 16);
                        else
                        {
                            if (!table.Contains(address))
                                errors.Add(new ParserError($"Label {address} does not exist. " +
                                    $"If a lable was not intended, add a leading 0 to the address operand.", linenumber));
                            else
                                resolvedAddress = (ushort)table[address];
                        }
                        instruction = (ushort)(MemoryOpcodes[opcode] | resolvedAddress);
                        if (indirect)
                            instruction |= 0x8000;
                    }
                    else if (RegisterOpcodes.ContainsKey(opcode))
                        instruction = RegisterOpcodes[opcode];
                    else if (IOOpcodes.ContainsKey(opcode))
                        instruction = IOOpcodes[opcode];
                    else if (opcode == "hex" || opcode == "dec")
                    {
                        ushort resolvedAddress = 0;
                        if (pureaddress)
                            if (opcode == "hex")
                                resolvedAddress = Convert.ToUInt16(address, 16);
                            else
                                resolvedAddress = unchecked((ushort)Convert.ToInt32(address, 10));
                        else
                        {
                            if (!table.Contains(address))
                                errors.Add(new ParserError($"Label {address} does not exist", linenumber));
                            else
                                resolvedAddress = (ushort)table[address];
                        }
                        instruction = resolvedAddress;
                    }
                    else if (opcode == "end")
                        break;

                    if (opcode == "org")
                    {
                        int count = Convert.ToUInt16(address, 16) - pc;
                        if (count < 0)
                            errors.Add(new ParserError("Memory overwrite", linenumber));
                        for (int i = 0; i < count; i++)
                        {
                            pc++;
                            writer.WriteLine($"{instruction:X4}");
                        }
                    }
                    else
                    {
                        pc++;
                        writer.WriteLine($"{instruction:X4} /\t{parser.OriginalLines[linenumber]}");
                    }
                }
            }

            if (errors.Count == 0 && pc != last_pc)
                throw new InvalidOperationException($"Program counter mismatch ({last_pc} != {pc})");

            return errors.Count == 0;
        }
    }
}