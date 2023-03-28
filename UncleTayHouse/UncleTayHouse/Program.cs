namespace UncleTayHouse
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** UNCLE TAYS HOUSE ADVENTURE ***");

            var game = new Game();
            game.Play();
        }
    }
}