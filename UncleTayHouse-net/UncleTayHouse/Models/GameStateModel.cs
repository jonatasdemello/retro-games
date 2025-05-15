namespace UncleTayHouse.Models
{
    public class GameStateModel
    {
        public int PlayerAt { get; set; } = 1; // player: current location
        public int SafeDoor { get; set; } = Utils.RNG(3);
        public bool PlayerJump { get; set; } = false;

        public List<string> Msg { get; set; } = []; // responses

        // implement nmethod to check if player is in a location
        public bool IsPlayerAt(int location)
        {
            return PlayerAt == location;
        }

        public bool ClearMessages()
        {
            Msg.Clear();
            return true;
        }
        public bool AddMessage(string message)
        {
            Msg.Add(message);
            return true;
        }
        public bool PrintMessages()
        {
            foreach (var message in Msg)
            {
                if (message.Contains("---"))
                    Screen.PrintBlue(message);
                else
                    Screen.PrintResponse(message);
            }
            ClearMessages();
            return true;
        }
    }
}