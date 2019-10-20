using System;
using System.Collections.Generic;

namespace ProcessorSimulationModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileParcer parser = new FileParcer(args);
                List<Command> commandsLs = parser.commandsLs;

                Processor processor = new Processor();
                foreach (Command com in commandsLs)
                {
                    processor.ExecuteCommand(com);
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine($"\n Exception: {ae.Message}");
            }
            catch (System.IO.IOException ioExp)
            {
                Console.WriteLine($"\n {ioExp.Message}");
            }
            finally
            {
                Console.WriteLine("\nEnd of executiom!");
                Console.ReadKey();
            }
        }
    }
}
