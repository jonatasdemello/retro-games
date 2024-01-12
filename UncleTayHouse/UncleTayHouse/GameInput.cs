namespace UncleTayHouse
{
    public partial class Game
    {
        /// <summary>
        /// Read user input text from console, convert to Uppercase and return.
        /// </summary>
        public string ReadInput()
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
        /// </summary>
        /// <param name="inputText"></param>
        public void ProcessInput(string inputText)
        {
            // InputWordTotal => number of words
            // InputWordText_INWS => contain only valid words now
            // InputWordNum_INPTK => contain the verb number

            string[] InputWordText_INWS = new string[4];
            int[] InputWordNum_INPTK = new int[4];

            // reset previous input
            CMD1 = 0;
            CMD2 = 0;
            CMD3 = 0;
            InputWordTotal = 0;

            if (String.IsNullOrEmpty(inputText))
            {
                return;
            }

            inputText = inputText.ToUpper();
            string[] words = inputText.Split(" ");

            int idx = 0;
            for (int i = 0; i < words.Length; i++)
            {
                // remove null words
                for (int j = 0; j < NULLWORDS.Length; j++)
                {
                    if (words[i] == NULLWORDS[j])
                    {
                        words[i] = "";
                    }
                }
                // only add if not null
                if (words[i] != "")
                {
                    // find the verb number for this word
                    for (int k = 0; k < VOCABS.Length; k++)
                    {
                        // only add if it is a known word
                        if (words[i] == VOCABS[k])
                        {
                            idx++;
                            InputWordText_INWS[idx] = VOCABS[k]; // words[i] or VOCABS[k]
                            InputWordNum_INPTK[idx] = k; // current verb number
                            break;
                        }
                    }
                }
            }
            InputWordTotal = idx; // used later: number of words

            CMD1 = InputWordNum_INPTK[1]; // first word
            CMD2 = InputWordNum_INPTK[2]; // second word
            CMD3 = InputWordNum_INPTK[3]; // third word
        }
    }
}
