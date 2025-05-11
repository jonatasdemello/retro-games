using UncleTayHouse.Models;

namespace UncleTayHouse
{
    public static class UserInput
    {
        // words to be ignored
        public static string[] NULLWORDS { get; } = [
            "THE", "TO", "WITH", "USING", "IN", "GO", "THIS"
        ];
        /// <summary>
        /// Read userInput input text from console, convert to Uppercase and return.
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
            GameUserInput result = new();

            if (String.IsNullOrEmpty(inputText))
            {
                return result;
            }
            if (inputText == "EXIT" || inputText == "QUIT" || inputText == "END")
            {
                result.Exit = true;
                return result;
            }

            inputText = inputText.ToUpper();
            string[] words = inputText.Split(" ");

            // Remove null words
            words = words.Where(word => !NULLWORDS.Contains(word)).ToArray();
            // remove all words that are not in VOCABS
            words = words.Where(word => Texts.VOCABS.Contains(word)).ToArray();

            // NumWords => number of words
            // InputWordText_INWS => contain only valid words now
            // InputWordNum_INPTK => contain the verb number

            string[] InputWordText_INWS = new string[4];
            int[] InputWordNum_INPTK = new int[4];
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
                    for (int k = 0; k < Texts.VOCABS.Length; k++)
                    {
                        // only add if it is a known word
                        if (words[i] == Texts.VOCABS[k])
                        {
                            idx++;
                            InputWordText_INWS[idx] = Texts.VOCABS[k]; // words[i] or VOCABS[k]
                            InputWordNum_INPTK[idx] = k; // current verb number
                            break;
                        }
                    }
                }
            }

            //int c1 = Array.IndexOf(Texts.VOCABS, words[0]);
            //int c2 = Array.IndexOf(Texts.VOCABS, words[1]);
            //int c3 = Array.IndexOf(Texts.VOCABS, words[2]);

            result.CMD1 = FindWord(words, 0); // first word.
            result.CMD2 = FindWord(words, 1); // second word.
            result.CMD3 = FindWord(words, 2); // third word.
            result.NumWords = words.Length; // used later: number of words

            return result;
            //return new GameUserInput
            //{
            //    CMD1 = InputWordNum_INPTK[1], // first word,
            //    CMD2 = InputWordNum_INPTK[2], // second word,
            //    CMD3 = InputWordNum_INPTK[3], // third word,
            //    NumWords = idx, // used later: number of words
            //};
        }
        public static int FindWord(string[] words, int idx)
        {
            if (idx < words.Length)
            {
                var res = Array.IndexOf(Texts.VOCABS, words[idx]);
                return res > 0 ? res : 0;
            }
            return 0;
        }
    }
}
