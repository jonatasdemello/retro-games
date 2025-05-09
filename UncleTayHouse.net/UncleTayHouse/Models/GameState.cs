namespace UncleTayHouse.Models
{
    public class GameState
    {
        public int PlayerAt { get; set; } = 1; // player: current location
        public int SafeDoor { get; set; } = Utils.RNG(3);
        public bool PlayerJump { get; set; } = false;

        // implement nmethod to check if player is in a location with an exit
        public bool IsAt(int location)
        {
            return PlayerAt == location;
        }
    }
}