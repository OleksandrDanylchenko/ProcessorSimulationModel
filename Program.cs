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
                FileParser parser = new FileParser(args);
                List<Command> commandsLs = parser.commandsLs;

                Processor processor = new Processor();
                foreach (Command com in commandsLs)
                {
                    processor.ExecuteCommand(com);
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine($"\n ArgException: {ae.Message}");
            }
            catch (OverflowException oe)
            {
                Console.WriteLine($"\n OverflowException: {oe.Message}");
            }
            catch (System.IO.IOException ioExp)
            {
                Console.WriteLine($"\n IOException:{ioExp.Message}");
            }
            finally
            {
                Console.WriteLine("\nEnd of executiom!");
                Console.ReadKey();
            }
        }
    }
}
