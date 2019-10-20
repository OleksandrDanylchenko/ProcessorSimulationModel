using System;
using System.Collections.Generic;
using System.IO;

namespace ProcessorSimulationModel
{
    internal class FileParser
    {
        private string FilePath;
        public List<Command> commandsLs { get; private set; } = new List<Command>();

        public FileParser(string[] args)
        {
            if (args.Length == 1)
            {
                FilePath = args[0];

                if (!FilePath.Contains(".txt"))
                {
                    throw new ArgumentException("Given path doesn't contain input txt file!");
                }

                ParseAndCreateCommandsLs();
            }
            else
            {
                throw new ArgumentException("Command Line argument formatting error!");
            }
        }

        private void ParseAndCreateCommandsLs()
        {
            StreamReader sr = File.OpenText(FilePath);
            while (!sr.EndOfStream)
            {
                string commandLine = sr.ReadLine();

                string[] operatinAndOperands = commandLine.Split(' ');
                string operation = operatinAndOperands[0];
                string[] operands = operatinAndOperands[1].Split(',');

                commandsLs.Add(new Command(operation, operands));
            }
        }
    }
}
