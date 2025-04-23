namespace UncleTayHouse
{
    public static class UserInput
    {
        /// <summary>
        /// Read user input text from console, convert to Uppercase and return.
        /// </summary>
        public static string ReadInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ");

            Console.Write("] ");
            string? res = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine(" ");

            if (res == null || res.Length == 0 || res.Length > 100)
                return String.Empty;

            res = res.ToUpper();

            if (res == "EXIT")
            {
                Environment.Exit(0);
            }
            return res;
        }

        /// <summary>
        /// Process input string and return:
        /// CMD1: first word
        /// CMD2: second word
        /// CMD3: third word
        /// </summary>
        /// <param name="inputText"></param>
        public static GameUserInput ProcessInput(string inputText)
        {
            // InputWordTotal => number of words
            // InputWordText_INWS => contain only valid words now
            // InputWordNum_INPTK => contain the verb number

            string[] InputWordText_INWS = new string[4];
            int[] InputWordNum_INPTK = new int[4];

            // reset previous input
            int CMD1 = 0;
            int CMD2 = 0;
            int CMD3 = 0;
            int InputWordTotal = 0;

            if (String.IsNullOrEmpty(inputText))
            {
                return new GameUserInput();
            }

            inputText = inputText.ToUpper();
            string[] words = inputText.Split(" ");

            int idx = 0;
            for (int i = 0; i < words.Length; i++)
            {
                // remove null words
                for (int j = 0; j < Constants.NULLWORDS.Length; j++)
                {
                    if (words[i] == Constants.NULLWORDS[j])
                    {
                        words[i] = "";
                    }
                }
                // only add if not null
                if (words[i] != "")
                {
                    // find the verb number for this word
                    for (int k = 0; k < Constants.VOCABS.Length; k++)
                    {
                        // only add if it is a known word
                        if (words[i] == Constants.VOCABS[k])
                        {
                            idx++;
                            InputWordText_INWS[idx] = Constants.VOCABS[k]; // words[i] or VOCABS[k]
                            InputWordNum_INPTK[idx] = k; // current verb number
                            break;
                        }
                    }
                }
            }

            CMD1 = InputWordNum_INPTK[1]; // first word
            CMD2 = InputWordNum_INPTK[2]; // second word
            CMD3 = InputWordNum_INPTK[3]; // third word
            InputWordTotal = idx; // used later: number of words

            return new GameUserInput
            {
                CMD1 = CMD1,
                CMD2 = CMD2,
                CMD3 = CMD3,
                InputWordTotal = InputWordTotal
            };
        }
    }
}
