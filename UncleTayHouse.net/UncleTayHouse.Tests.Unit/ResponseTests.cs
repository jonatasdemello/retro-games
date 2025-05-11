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
        public void Input_One_Word_Should_Return_Response(string prompt, string response)
        {
            Game game = new();
            game.userInput = GameUserInput.ProcessInput(prompt);
            game.ActionProcessInput();

            Assert.IsTrue(game.gameState.Msg.Any());
            Assert.IsTrue(game.gameState.Msg[0].Contains(response));
        }
    }
}
