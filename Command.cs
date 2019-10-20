using System;
using System.Linq;

namespace ProcessorSimulationModel
{
    internal class Command
    {
        public string Operation { get; private set; }
        public string[] Operands { get; private set; } = new string[3];

        private string[] _AvailableOperation = new string[] { "mov", "shl", "shr" };
        public Command(string operation, string[] operands)
        {
            if (operands.Length <= 3)
            {
                if (IsAvailableOperation(operation))
                {
                    Operation = operation;
                    Operands = operands;
                }
                else
                {
                    throw new ArgumentException("Unexpected operation!");
                }
            }
            else
            {
                throw new ArgumentException("Cannot use more than 3 operands.");
            }
        }

        private bool IsAvailableOperation(string oper)
        {
            bool contained = _AvailableOperation.Any(c => c.Equals(oper, StringComparison.CurrentCultureIgnoreCase));
            return contained;
        }
    }
}