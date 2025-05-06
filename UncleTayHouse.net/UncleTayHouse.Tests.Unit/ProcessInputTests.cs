using UncleTayHouse.Models;

namespace UncleTayHouse.Tests.Unit
{
    [TestClass]
    public sealed class ProcessInputTests
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("xxxxx")]
        [DataRow("xxxxx yyy")]
        public void Input_Bad_Word_Should_return_0(string prompt)
        {
            GameUserInput game = UserInput.ProcessInput(prompt);

            Assert.IsTrue(game.CMD1 == 0);
            Assert.IsTrue(game.CMD2 == 0);
            Assert.IsTrue(game.CMD3 == 0);
        }

        [TestMethod]
        [DataRow("NORTH")]
        [DataRow("north")]
        public void Input_Direction_Ok(string prompt)
        {
            GameUserInput game = UserInput.ProcessInput(prompt);

            Assert.AreEqual("NORTH", Constants.VOCABS[game.CMD1]);
            Assert.AreEqual(1, game.CMD1); // "NORTH"
        }

        [TestMethod]
        [DataRow("TAKE NEWSPAPER")]
        [DataRow("TAKE THE NEWSPAPER")]
        [DataRow("TAKE THIS NEWSPAPER")]
        [DataRow("TAKE XYZ NEWSPAPER WWYA")]
        public void Input_Multi_Word(string prompt)
        {
            GameUserInput game = UserInput.ProcessInput(prompt);

            Assert.AreEqual("TAKE", Constants.VOCABS[game.CMD1]);
            Assert.AreEqual(18, game.CMD1); // "TAKE"

            Assert.AreEqual("NEWSPAPER", Constants.VOCABS[game.CMD2]);
            Assert.AreEqual(34, game.CMD2); // "NEWSPAPER"
        }

        [TestMethod]
        [DataRow("MOVE FRIDGE WITH JACK")]
        [DataRow("MOVE FRIDGE JACK")]
        [DataRow("MOVE THE FRIDGE WITH JACK")]
        public void Input_Multi_Word2(string prompt)
        {
            GameUserInput game = UserInput.ProcessInput(prompt);

            Assert.AreEqual("MOVE", Constants.VOCABS[game.CMD1]);
            Assert.AreEqual(26, game.CMD1);

            Assert.AreEqual("FRIDGE", Constants.VOCABS[game.CMD2]);
            Assert.AreEqual(54, game.CMD2);

            Assert.AreEqual("JACK", Constants.VOCABS[game.CMD3]);
            Assert.AreEqual(37, game.CMD3);
        }

        [TestMethod]
        [DataRow("OPEN LEFT DOOR")]
        [DataRow("OPEN CENTER DOOR")]
        [DataRow("OPEN RIGHT DOOR")]
        public void Input_Multi_Word3(string prompt)
        {
            GameUserInput game = UserInput.ProcessInput(prompt);

            Assert.AreEqual("OPEN", Constants.VOCABS[game.CMD1]);
            Assert.AreEqual(27, game.CMD1);

            string wd = Constants.VOCABS[game.CMD2];
            int wn = game.CMD2;
            Assert.IsTrue(wd == "LEFT" || wd == "CENTER" || wd == "RIGHT");
            Assert.IsTrue(wn == 31 || wn == 32 || wn == 33);

            Assert.AreEqual("DOOR", Constants.VOCABS[game.CMD3]);
            Assert.AreEqual(57, game.CMD3);
        }

        [TestMethod]
        [DataRow("OIL DUMBWAITER OILCAN")]
        [DataRow("OIL DUMBWAITER WITH OILCAN")]
        [DataRow("OIL THE DUMBWAITER WITH OILCAN")]
        public void Input_Multi_Word4(string prompt)
        {
            GameUserInput game = UserInput.ProcessInput(prompt);

            Assert.AreEqual("OIL", Constants.VOCABS[game.CMD1]);
            Assert.AreEqual(29, game.CMD1);

            Assert.AreEqual("DUMBWAITER", Constants.VOCABS[game.CMD2]);
            Assert.AreEqual(59, game.CMD2);

            Assert.AreEqual("OILCAN", Constants.VOCABS[game.CMD3]);
            Assert.AreEqual(48, game.CMD3);
        }
    }
}
