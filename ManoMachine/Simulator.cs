using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManoMachine
{
    public class Simulator
    {
        public static readonly List<MicroOperation> uOps = new List<MicroOperation>
        {
            // Fetch
            new MicroOperation("AR ← PC", s => !s.R && s.SC == 0, s => s.AR = s.PC),
            new MicroOperation("IR ← M[AR], PC ← PC + 1", s => !s.R && s.SC == 1, s => { s.IR = s.Memory[s.AR]; unchecked { s.PC++; } }),

            // Decode
            new MicroOperation("AR ← IR(0-11), Decode IR(12-14), I ← IR(15)", s => !s.R && s.SC == 2,
                s => { s.AR = (ushort)(s.IR & 0x0FFFu); s.I = (s.IR & 1u << 15) != 0; }),

            // Memory Address Resolution
            new MicroOperation("AR ← M[AR]", s => s.SC == 3 && s.D != 7 && s.I, s => s.AR = (ushort)(s.Memory[s.AR] & 0x0FFFu)),
            new MicroOperation("NOP", s => s.SC == 3 && s.D != 7 && !s.I, s => { }),

            // AND
            new MicroOperation("DR ← M[AR]", s => s.SC == 4 && s.D == 0, s => s.DR = s.Memory[s.AR]),
            new MicroOperation("AC ← AC Λ DR, SC ← 0", s => s.SC == 5 && s.D == 0, s => { s.AC &= s.DR; s.SC = 0; }),

            // ADD
            new MicroOperation("DR ← M[AR]", s => s.SC == 4 && s.D == 1, s => s.DR = s.Memory[s.AR]),
            new MicroOperation("AC ← AC + DR, E ← C_out, SC ← 0", s => s.SC == 5 && s.D == 1,
                s => { s.E = (uint)s.AC + s.DR > ushort.MaxValue; unchecked { s.AC += s.DR; } s.SC = 0; }),

            // LDA
            new MicroOperation("DR ← M[AR]", s => s.SC == 4 && s.D == 2, s => s.DR = s.Memory[s.AR]),
            new MicroOperation("AC ← DR, SC ← 0", s => s.SC == 5 && s.D == 2, s => (s.AC, s.SC) = (s.DR, 0)),

            // STA
            new MicroOperation("M[AR] ← AC, SC ← 0", s => s.SC == 4 && s.D == 3, s => (s.Memory[s.AR], s.SC) = (s.AC, 0)),

            // BUN
            new MicroOperation("PC ← AR, SC ← 0", s => s.SC == 4 && s.D == 4, s => (s.PC, s.SC) = (s.AR, 0)),

            // BSA
            new MicroOperation("M[AR] ← PC, AR ← AR + 1", s => s.SC == 4 && s.D == 5, s => { s.Memory[s.AR] = s.PC; unchecked{ s.AR++; } }),
            new MicroOperation("PC ← AR, SC ← 0", s => s.SC == 5 && s.D == 5, s => (s.PC, s.SC) = (s.AR, 0)),

            // ISZ
            new MicroOperation("DR ← M[AR]", s => s.SC == 4 && s.D == 6, s => s.DR = s.Memory[s.AR]),
            new MicroOperation("DR ← DR + 1", s => s.SC == 5 && s.D == 6, s => { unchecked { s.DR++; } }),
            new MicroOperation("M[AR] ← DR, SC ← 0", s => s.SC == 6 && s.D == 6, s => (s.Memory[s.AR], s.SC) = (s.DR, 0)),
            new MicroOperation("PC ← PC + 1", s => s.SC == 6 && s.D == 6 && s.DR == 0, s => { unchecked { s.PC++; } }),


            // CLA
            new MicroOperation("AC ← 0", s => s.r && s.B(11), s => s.AC = 0),
            // CLE
            new MicroOperation("E ← 0", s => s.r && s.B(10), s => s.E = false),
            // CMA
            new MicroOperation("AC ← ~AC", s => s.r && s.B(9), s =>  s.AC = unchecked((ushort)~s.AC)),
            // CME
            new MicroOperation("E ← ~E", s => s.r && s.B(8), s => s.E = !s.E),
            // CIR
            new MicroOperation("AC ← shr AC, AC(15) ← E, E ← AC(0)", s => s.r && s.B(7),
                s => { unchecked { (s.E, s.AC) = ((s.AC & 1) != 0, (ushort)((s.AC >> 1) | (s.E ? (1 << 15) : 0))); } }),
            // CIL
            new MicroOperation("AC ← shl AC, AC(0) ← E, E ← AC(15)", s => s.r && s.B(6),
                s => { unchecked { (s.E, s.AC) = ((s.AC & (1 << 15)) != 0, (ushort)((s.AC << 1) | (s.E ? 1 : 0))); } }),
            // INC
            new MicroOperation("AC ← AC + 1", s => s.r && s.B(5), s => { unchecked { s.AC++; } }),
            // SPA
            new MicroOperation("PC ← PC + 1", s => s.r && s.B(4) && (s.AC & (1 << 15)) == 0, s => { unchecked { s.PC++; } }),
            // SNA
            new MicroOperation("PC ← PC + 1", s => s.r && s.B(3) && (s.AC & (1 << 15)) == 1, s => { unchecked { s.PC++; } }),
            // SZA
            new MicroOperation("PC ← PC + 1", s => s.r && s.B(2) && s.AC == 0, s => { unchecked { s.PC++; } }),
            // SZE
            new MicroOperation("PC ← PC + 1", s => s.r && s.B(1) && !s.E, s => { unchecked { s.PC++; } }),
            // HLT
            new MicroOperation("S ← 0", s => s.r && s.B(0), s => s.S = false),

            new MicroOperation("SC ← 0", s => s.r, s => s.SC = 0),


            // INP
            new MicroOperation("AC(0-7) ← INPR, FGI ← 0", s => s.p && s.B(11), s => (s.AC, s.FGI) = (s.INPR, false)),
            // OUT
            new MicroOperation("OUTR ← AC(7-0), FGO ← 0", s => s.p && s.B(10), s => (s.OUTR, s.FGO) = (unchecked((byte)s.AC), false)),
            // SKI
            new MicroOperation("PC ← PC + 1", s => s.p && s.B(9) && s.FGI, s => { unchecked { s.PC++; } }),
            // SKO
            new MicroOperation("PC ← PC + 1", s => s.p && s.B(8) && s.FGO, s => { unchecked { s.PC++; } }),
            // ION
            new MicroOperation("IEN ← 1", s => s.p && s.B(7), s => s.IEN = true),
            // IOF
            new MicroOperation("IEN ← 0", s => s.p && s.B(6), s => s.IEN = false),

            new MicroOperation("SC ← 0", s => s.p, s => s.SC = 0),


            // Interrupt
            new MicroOperation("R ← 1", s => s.SC > 2 && s.IEN && (s.FGI || s.FGO), s => s.R = true),

            new MicroOperation("AR ← 0, TR ← PC", s => s.R && s.SC == 0, s => (s.AR, s.TR) = (0, s.PC)),
            new MicroOperation("M[AR] ← TR, PC ← 0", s => s.R && s.SC == 1, s => (s.Memory[s.AR], s.PC) = (s.TR, 0)),
            new MicroOperation("PC ← PC + 1, IEN ← 0, SC ← 0, R ← 0", s => s.R && s.SC == 2,
                s => { unchecked { s.PC++; } s.IEN = s.R = false; s.SC = 0; }),
        };

        public Simulator(string path = null)
        {
            if (path != null)
                LoadRom(path);
            else
                State = new MachineState();
        }

        Thread thread;
        bool stop = false;
        volatile bool running = false;

        public int Ticks { get; set; } = 0;
        public MachineState State { get; set; }
        public bool Running { get => running; }

        public void LoadRom(string path)
        {
            State = new MachineState();

            int i = 0;
            foreach (var line in File.ReadLines(path))
            {
                var splits = line.Split('/');
                State.Memory[i] = Convert.ToUInt16(splits[0], 16);
                i++;
            }
        }

        public void Run()
        {
            if (running)
                throw new InvalidOperationException("Thread is running");

            thread = new Thread(() =>
            {
                running = true;

                while (!stop)
                {
                    Next();
                }

                running = false;
            });

            stop = false;
            thread.Start();
        }

        public void Stop()
        {
            stop = true;
        }

        public void Tick()
        {
            if (running)
                throw new InvalidOperationException("Thread is running");

            Iterate();
        }

        public void Next()
        {
            do
            {
                Tick();
            } while (State.SC != 0);
        }

        public void Reset()
        {
            if (running)
                throw new InvalidOperationException("Thread is running");

            MachineState state = new MachineState();
            state.Memory = State.Memory;
            State = state;
        }

        private void Iterate()
        {
            Ticks++;


        }
    }
}
