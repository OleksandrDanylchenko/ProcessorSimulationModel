using System;
using System.Linq;
using System.Text;

namespace ProcessorSimulationModel
{
    internal class Processor
    {
        // readonly for reference types only makes the reference readonly not the values
        private readonly ProcessorCurrentState _ProcCurState = new ProcessorCurrentState();

        public void ExecuteCommand(Command com)
        {
            Tact1(com);
            PrintCurrState();
            ProceedFurther();
            Tact2(com);
            PrintCurrState();
            ProceedFurther();
        }

        private void Tact1(Command com)
        {
            _ProcCurState.Command = com.Operation + ' ' + string.Join(",", com.Operands);

            if (com.Operation.Equals("mov", StringComparison.CurrentCultureIgnoreCase))
            {
                // mov | R2 | 1111 1011
                string binaryInitVal = Converter.GetBinaryString(int.Parse(com.Operands[1]));
                _ProcCurState.Ins = com.Operation + " | " + com.Operands[0] + " | " + binaryInitVal;
            }
            else
            {
                //shl | R1 | 2
                _ProcCurState.Ins = com.Operation + " | " + string.Join(" | ", com.Operands);
            }

            ++_ProcCurState.PC;
            _ProcCurState.TC = 1;
        }

        private void Tact2(Command com)
        {
            int registerIndex = Converter.GetIndexOfRegister(com.Operands[0]);
            int workVal = int.Parse(com.Operands[1]);

            if (com.Operation.Equals("mov", StringComparison.CurrentCultureIgnoreCase))
            {
                Mov(registerIndex, workVal);
            }
            else if (com.Operation.Equals("shl", StringComparison.CurrentCultureIgnoreCase))
            {
                Shl(registerIndex, workVal);
            }
            else // shr
            {
                Shr(registerIndex, workVal);
            }

            _ProcCurState.TC = 2;
            _ProcCurState.PS = _ProcCurState.R[registerIndex].FirstOrDefault() - '0';
        }

        private void PrintCurrState()
        {
            Console.WriteLine($"Command = {_ProcCurState.Command}");
            Console.WriteLine($"R0 = {_ProcCurState.R[0]}  Ins = {_ProcCurState.Ins}");
            Console.WriteLine($"R1 = {_ProcCurState.R[1]}  PC = {_ProcCurState.PC}");
            Console.WriteLine($"R2 = {_ProcCurState.R[2]}  TC = {_ProcCurState.TC}");
            Console.WriteLine($"R3 = {_ProcCurState.R[3]}  PS = {_ProcCurState.PS}");
            Console.WriteLine("");
        }

        private void Mov(int registerIndex, int initVal)
        {
            string binaryInitVal = Converter.GetBinaryString(initVal);
            _ProcCurState.R[registerIndex] = binaryInitVal;
        }

        private void Shl(int registerIndex, int moveVal)
        {
            if (moveVal >= 0)
            {
                string joinedBinaryStr = Converter.JoinBinaryString(_ProcCurState.R[registerIndex]);
                StringBuilder modReg = new StringBuilder(joinedBinaryStr);
                for (int i = 0; i < moveVal; ++i)
                {
                    for (int j = 0; j < modReg.Length - 1; ++j)
                    {
                        modReg[j] = modReg[j + 1];
                    }
                    modReg[modReg.Length - 1] = '0';
                }
                _ProcCurState.R[registerIndex] = Converter.SeparateString(modReg.ToString());
            }
            else
            {
                throw new ArgumentException("Cannot operate shl with negative move value!");
            }
        }

        private void Shr(int registerIndex, int moveVal)
        {
            if (moveVal >= 0)
            {
                string joinedBinaryStr = Converter.JoinBinaryString(_ProcCurState.R[registerIndex]);
                StringBuilder modReg = new StringBuilder(joinedBinaryStr);
                for (int i = 0; i < moveVal; ++i)
                {
                    for (int j = modReg.Length - 1; j > 0; --j)
                    {
                        modReg[j] = modReg[j - 1];
                    }
                    modReg[0] = '0';
                }
                _ProcCurState.R[registerIndex] = Converter.SeparateString(modReg.ToString());
            }
            else
            {
                throw new ArgumentException("Cannot operate shr with negative move value!");
            }
        }

        private void ProceedFurther()
        {
            Console.Write("Press F to proceed:");
            char pf = Console.ReadKey().KeyChar;
            if (pf != 'F' && pf != 'f')
            {
                throw new System.IO.IOException("Escape character has been used!");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
