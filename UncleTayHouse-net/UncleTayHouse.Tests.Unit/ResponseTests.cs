namespace UncleTayHouse.Tests.Unit
{
    [TestClass]
    public sealed class ResponseTests
    {
        [TestMethod]
        [DataRow("I", "[Inventory]")]
        [DataRow("SCORE", "Your score is 10 out of a possible 100.")]
        [DataRow("JUMP", "You jump up and down")]
        [DataRow("TAKE", "Take what?")]
        [DataRow("DROP", "Drop what?")]
        [DataRow("DOOR", "Do what with DOOR?")]
        [DataRow("LOOK NEWSPAPER", "Tays house unlikely ever to be sold. tales of gutted stairwells and booby traps have spooked buyers...")]
        [DataRow("TAKE NEWSPAPER", "NEWSPAPER: taken")]
        [DataRow("TAKE DIAMOND", "There is no DIAMOND here")]
        public void Input_Word_Should_Return_Response(string prompt, string response)
        {
            Game game = new();
            game.userInput = GameUserInput.ProcessInput(prompt);
            game.ActionProcessInput();

            Assert.IsTrue(game.gameState.Msg.Any());
            Assert.IsTrue(game.gameState.Msg[0].Contains(response));
        }
    }
}
