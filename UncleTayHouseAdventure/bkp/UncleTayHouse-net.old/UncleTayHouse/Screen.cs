namespace UncleTayHouse
{
    public static class Screen
    {
        public static void ClearScreen()
        {
            Console.Clear();
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void print(string text) => Console.WriteLine(text);
        
        public static void print(string text1, string text2) => Console.WriteLine($"{text1} {text2}");

        public static void PrintResponse(string text)
        {
            string sep = new string('-', 80);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void PrintDgb(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("    ----------------------------- " + text + " ----------------------------- ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void PrintDgb1(string text)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("----------------------------- " + text + " ----------------------------- ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

    }
}