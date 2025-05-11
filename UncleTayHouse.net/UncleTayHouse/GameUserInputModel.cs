using UncleTayHouse.Models;

namespace UncleTayHouse
{
    public static class GameUserInputModel
    {
        // words to be ignored
        public static string[] NULLWORDS { get; } = [
            "THE", "TO", "WITH", "USING", "IN", "GO", "THIS", "AT"
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
        public static GameResponseModel ProcessInput(string inputText)
        {
            GameResponseModel result = new();

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

            // remove null words
            words = words.Where(word => !NULLWORDS.Contains(word)).ToArray();
            // remove all words that are not in AllWords
            words = words.Where(word => Constants.AllWords.Contains(word)).ToArray();

            result.CMD1 = FindWord(words, 0); // first word.
            result.CMD2 = FindWord(words, 1); // second word.
            result.CMD3 = FindWord(words, 2); // third word.
            result.NumWords = words.Length;

            // adjust indexes
            //if (result.CMD2 > 0)
            //{
            //    result.CMD2 -= Constants.OBJECTOFFSET;
            //}
            //if (result.CMD3 > 0)
            //{
            //    result.CMD3 -= Constants.OBJECTOFFSET;
            //}
            return result;
        }

        // retrun the word if exists (in index)
        public static int FindWord(string[] words, int idx)
        {
            if (idx < words.Length)
            {
                var res = Array.IndexOf(Constants.AllWords, words[idx]);
                return res > 0 ? res : 0;
            }
            return 0;
        }

        // original code - left here for reference
        // ---------------------------------------

        /// <summary>
        /// Process input string and return:
        /// CMD1: first word
        /// CMD2: second word
        /// CMD3: third word
        /// </summary>
        /// <param name="inputText"></param>
        public static GameResponseModel ProcessInputOrig(string inputText)
        {
            GameResponseModel result = new();
            if (inputText == "EXIT" || inputText == "QUIT" || inputText == "END")
            {
                result.Exit = true;
                return result;
            }
            if (String.IsNullOrEmpty(inputText))
            {
                return new GameResponseModel();
            }
            // NumWords => number of words
            // InputWordText_INWS => contain only valid words now
            // InputWordNum_INPTK => contain the verb number

            string[] InputWordText_INWS = new string[4];
            int[] InputWordNum_INPTK = new int[4];

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
                    for (int k = 0; k < Constants.AllWords.Length; k++)
                    {
                        // only add if it is a known word
                        if (words[i] == Constants.AllWords[k])
                        {
                            idx++;
                            InputWordText_INWS[idx] = Constants.AllWords[k]; // words[i] or AllWords[k]
                            InputWordNum_INPTK[idx] = k; // current verb number
                            break;
                        }
                    }
                }
            }

            return new GameResponseModel
            {
                CMD1 = InputWordNum_INPTK[1], // first word,
                CMD2 = InputWordNum_INPTK[2], // second word,
                CMD3 = InputWordNum_INPTK[3], // third word,
                NumWords = idx // used later: number of words
            };
        }
    }
}

