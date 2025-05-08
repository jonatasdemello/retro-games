namespace UncleTayHouse
{
    public static class Constants
    {
        // to avoid magic numbers
        public static int CARRYING { get; } = 0;
        public static int HIDDEN { get; } = -1;
        public static int TIED { get; } = -12;

        // item offset - where objects start in Vocab array
        public static int OBJECTOFFSET { get; } = 33;
    }
}