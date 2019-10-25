namespace ProcessorSimulationModel
{
    internal class ProcessorCurrentState
    {
        public string Command { get; set; }
        public string Ins { get; set; }

        // registers
        public string[] R { get; set; }

        public int PC { get; set; } // commands counter
        public int TC { get; set; } // tacts counter
        public int PS { get; set; } // number sign

        public ProcessorCurrentState()
        {
            Command = string.Empty;
            Ins = string.Empty;

            System.Random random = new System.Random();
            R = new string[4];
            for (int i = 0; i < R.Length; ++i)
            {
                R[i] = Converter.GetBinaryString(random.Next(127));
            }

            PC = 0;
            TC = 1;
            PS = 0;
        }
    }
}
