namespace UncleTayHouse.Models
{
    /* Example:
        move couch with brace
        move fridge with jack
        move clothes with gloves
        open [direction] door
        oil dumbwaiter with oilcan
        tie bungee to railing
        put fuse in fusebox
        read note in mirror
    */
    public class GameResponseModel
    {
        public int CMD1 = 0; // verb (action)
        public int CMD2 = 0; // noun (object)
        public int CMD3 = 0; // noun (object)
        public int NumWords = 0;
        public bool Exit = false;

        // keep the original index, but use a get property
        public int CMD2obj { get => CMD2 - Constants.OBJECTOFFSET; }
        public int CMD3obj { get => CMD3 - Constants.OBJECTOFFSET; }
    }
}
