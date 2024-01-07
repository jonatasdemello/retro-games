namespace UncleTayHouse
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("*** UNCLE TAYS HOUSE ADVENTURE ***");
            Console.WriteLine("**********************************");

            Game game = new Game();
            game.Play();
        }
    }
}