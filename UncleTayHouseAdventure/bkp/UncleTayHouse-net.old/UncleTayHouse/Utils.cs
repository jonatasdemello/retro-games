namespace UncleTayHouse
{
    public static class Utils
    {
        public static int RNG(int max)
        {
            Random rnd = new Random();
            int value = rnd.Next(1, max + 1);
            return value;
        }
    }
}
