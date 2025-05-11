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
            GameResponseModel game = GameUserInputModel.ProcessInput(prompt);

            Assert.IsTrue(game.CMD1 == 0);
            Assert.IsTrue(game.CMD2 == 0);
            Assert.IsTrue(game.CMD3 == 0);
        }

        [TestMethod]
        [DataRow("NORTH")]
        [DataRow("north")]
        public void Input_Direction_Ok(string prompt)
        {
            GameResponseModel game = GameUserInputModel.ProcessInput(prompt);

            Assert.AreEqual("NORTH", Constants.AllWords[game.CMD1]);
            Assert.AreEqual(CteVerbs.NORTH, game.CMD1); // "NORTH"
        }

        [TestMethod]
        [DataRow("TAKE NEWSPAPER")]
        [DataRow("TAKE THE NEWSPAPER")]
        [DataRow("TAKE THIS NEWSPAPER")]
        [DataRow("TAKE XYZ NEWSPAPER WWYA")]
        public void Input_Multi_Word(string prompt)
        {
            GameResponseModel game = GameUserInputModel.ProcessInput(prompt);

            Assert.AreEqual("TAKE", Constants.AllWords[game.CMD1]);
            Assert.AreEqual(CteVerbs.TAKE, game.CMD1); // "TAKE"

            Assert.AreEqual("NEWSPAPER", Constants.AllWords[game.CMD2]);
            Assert.AreEqual(CteObjects.NEWSPAPER, game.CMD2obj); // "NEWSPAPER"
        }

        [TestMethod]
        [DataRow("MOVE FRIDGE WITH JACK")]
        [DataRow("MOVE FRIDGE JACK")]
        [DataRow("MOVE THE FRIDGE WITH JACK")]
        public void Input_Multi_Word2(string prompt)
        {
            GameResponseModel game = GameUserInputModel.ProcessInput(prompt);

            Assert.AreEqual("MOVE", Constants.AllWords[game.CMD1]);
            Assert.AreEqual(CteVerbs.MOVE, game.CMD1);

            Assert.AreEqual("FRIDGE", Constants.AllWords[game.CMD2]);
            Assert.AreEqual(CteObjects.FRIDGE, game.CMD2obj);

            Assert.AreEqual("JACK", Constants.AllWords[game.CMD3]);
            Assert.AreEqual(CteObjects.JACK, game.CMD3obj);
        }

        [TestMethod]
        [DataRow("OPEN LEFT DOOR")]
        [DataRow("OPEN CENTER DOOR")]
        [DataRow("OPEN RIGHT DOOR")]
        public void Input_Multi_Word3(string prompt)
        {
            GameResponseModel game = GameUserInputModel.ProcessInput(prompt);

            Assert.AreEqual("OPEN", Constants.AllWords[game.CMD1]);
            Assert.AreEqual(CteVerbs.OPEN, game.CMD1);

            string wd = Constants.AllWords[game.CMD2];
            int wn = game.CMD2;
            Assert.IsTrue(wd == "LEFT" || wd == "CENTER" || wd == "RIGHT");
            Assert.IsTrue(wn == CteVerbs.LEFT || wn == CteVerbs.CENTER || wn == CteVerbs.RIGHT);

            Assert.AreEqual("DOOR", Constants.AllWords[game.CMD3]);
            Assert.AreEqual(CteObjects.DOOR, game.CMD3obj);
        }

        [TestMethod]
        [DataRow("OIL DUMBWAITER OILCAN")]
        [DataRow("OIL DUMBWAITER WITH OILCAN")]
        [DataRow("OIL THE DUMBWAITER WITH OILCAN")]
        public void Input_Multi_Word4(string prompt)
        {
            GameResponseModel game = GameUserInputModel.ProcessInput(prompt);

            Assert.AreEqual("OIL", Constants.AllWords[game.CMD1]);
            Assert.AreEqual(CteVerbs.OIL, game.CMD1);

            Assert.AreEqual("DUMBWAITER", Constants.AllWords[game.CMD2]);
            Assert.AreEqual(CteObjects.DUMBWAITER, game.CMD2obj);

            Assert.AreEqual("OILCAN", Constants.AllWords[game.CMD3]);
            Assert.AreEqual(CteObjects.OILCAN, game.CMD3obj);
        }
    }
}
