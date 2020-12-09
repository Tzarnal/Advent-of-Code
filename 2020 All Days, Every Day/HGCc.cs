using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Advent
{
    public class HGCc
    {
        public long Accumulator { get; set; }
        public int Cursor { get; private set; }
        public HashSet<int> ExecutionRecord { get; private set; }
        public List<(string operation, int argument)> Instructions { get; private set; }

        private Dictionary<string, Action<int>> _operators;

        private HGCc()
        {
            _operators = new Dictionary<string, Action<int>>
            {
                ["nop"] = NOPoperation,
                ["acc"] = ACCoperation,
                ["jmp"] = JMPoperation
            };
        }

        public HGCc(string input) : this()
        {
            LoadInstructions(input);
        }

        public HGCc(List<(string operation, int argument)> instructions) : this()
        {
            LoadInstructions(instructions);
        }

        /*
         * nop
         *
         * nop stands for No OPeration - it does nothing. The instruction immediately
         * below it is executed next.
         */

        private void NOPoperation(int argument)
        {
            Cursor++;
        }

        /*
         * acc
         *
         * acc increases or decreases a single global value called the accumulator
         * by the value given in the argument. For example, acc +7 would increase
         * the accumulator by 7. The accumulator starts at 0. After an acc
         * instruction, the instruction immediately below it is executed next.
         */

        private void ACCoperation(int argument)
        {
            Accumulator += argument;
            Cursor++;
        }

        /*
         * jmp
         *
         * jmp jumps to a new instruction relative to itself. The next instruction
         * to execute is found using the argument as an offset from the jmp
         * instruction; for example, jmp +2 would skip the next instruction,
         * jmp +1 would continue to the instruction immediately below it, and
         * jmp -20 would cause the instruction 20 lines above to be executed next.
         */

        private void JMPoperation(int argument)
        {
            Cursor += argument;
        }

        public HGCcExecutionResult Run()
        {
            ExecutionRecord = new HashSet<int>();

            while (Cursor < Instructions.Count)
            {
                //The current instruction has already been executed. Without conditional jmp's this problem is an infinite loop
                if (ExecutionRecord.Contains(Cursor))
                {
                    return new HGCcExecutionResult
                    {
                        Accumulator = Accumulator,
                        Cursor = Cursor,
                        InfiniteLoopDetected = true
                    };
                }

                ExecutionRecord.Add(Cursor);

                ExecuteNextInstruction();
            }

            //Cursor was outside of list of instructions. This is considered a normal exit.
            return new HGCcExecutionResult
            {
                Accumulator = Accumulator,
                Cursor = Cursor,
                NormalExit = true
            };
        }

        private void ExecuteNextInstruction()
        {
            var (operation, argument) = Instructions[Cursor];

            _operators[operation](argument);
        }

        public bool LoadInstructions(string code)
        {
            var instructions = ReadCode(code);

            return LoadInstructions(instructions);
        }

        public bool LoadInstructions(List<(string operation, int argument)> instructions)
        {
            Accumulator = 0;
            Cursor = 0;
            ExecutionRecord = new HashSet<int>();

            Instructions = instructions;
            return true;
        }

        private List<(string operation, int argument)> ReadCode(string code)
        {
            var input = code.Split(Environment.NewLine);

            var instructions = new List<(string operation, int argument)>();
            foreach (var line in input)
            {
                var elements = line.Split(' ');
                var instruction = (elements[0], int.Parse(elements[1]));

                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}