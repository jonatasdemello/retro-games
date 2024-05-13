using UncleTayHouse;

namespace TestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("xxxxx")]
        [DataRow("xxxxx yyy")]
        public void Input_Bad_Word(string prompt)
        {
            Game game = new Game();

            game.ProcessInput(prompt);

            Assert.IsFalse(Array.Exists(game.VOCABS, item => item != ""));

            Assert.IsTrue(game.CMD1 == 0);
            Assert.IsTrue(game.CMD2 == 0);
            Assert.IsTrue(game.CMD3 == 0);
        }

        [TestMethod]
        [DataRow("NORTH")]
        [DataRow("north")]
        public void Input_Direction_Ok(string prompt)
        {
            Game game = new Game();

            game.ProcessInput(prompt);

            Assert.AreEqual(game.VOCABS[game.CMD1], "NORTH");
            Assert.AreEqual(game.CMD1, 1); // "NORTH"
        }

        [TestMethod]
        [DataRow("TAKE NEWSPAPER")]
        [DataRow("TAKE THE NEWSPAPER")]
        [DataRow("TAKE THIS NEWSPAPER")]
        [DataRow("TAKE XYZ NEWSPAPER WWYA")]
        public void Input_Multi_Word(string prompt)
        {
            Game game = new Game();

            game.ProcessInput(prompt);

            Assert.AreEqual(game.VOCABS[game.CMD1], "TAKE");
            Assert.AreEqual(game.CMD1, 18); // "TAKE"

            Assert.AreEqual(game.VOCABS[game.CMD2], "NEWSPAPER");
            Assert.AreEqual(game.CMD2, 34); // "NEWSPAPER"
        }

        [TestMethod]
        [DataRow("MOVE FRIDGE WITH JACK")]
        [DataRow("MOVE FRIDGE JACK")]
        [DataRow("MOVE THE FRIDGE WITH JACK")]
        public void Input_Multi_Word2(string prompt)
        {
            Game game = new Game();

            game.ProcessInput(prompt);

            Assert.AreEqual(game.VOCABS[game.CMD1], "MOVE");
            Assert.AreEqual(game.CMD1, 26);

            Assert.AreEqual(game.VOCABS[game.CMD2], "FRIDGE");
            Assert.AreEqual(game.CMD2, 54);

            Assert.AreEqual(game.VOCABS[game.CMD3], "JACK");
            Assert.AreEqual(game.CMD3, 37);
        }

        [TestMethod]
        [DataRow("OPEN LEFT DOOR")]
        [DataRow("OPEN CENTER DOOR")]
        [DataRow("OPEN RIGHT DOOR")]
        public void Input_Multi_Word3(string prompt)
        {
            Game game = new Game();

            game.ProcessInput(prompt);

            Assert.AreEqual(game.VOCABS[game.CMD1], "OPEN");
            Assert.AreEqual(game.CMD1, 27);

            string wd = game.VOCABS[game.CMD2];
            int wn = game.CMD2;
            Assert.IsTrue(wd == "LEFT" || wd == "CENTER" || wd =="RIGHT");
            Assert.IsTrue(wn == 31 || wn ==32 || wn == 33);

            Assert.AreEqual(game.VOCABS[game.CMD3], "DOOR");
            Assert.AreEqual(game.CMD3, 57);
        }

        [TestMethod]
        [DataRow("OIL DUMBWAITER OILCAN")]
        [DataRow("OIL DUMBWAITER WITH OILCAN")]
        [DataRow("OIL THE DUMBWAITER WITH OILCAN")]
        public void Input_Multi_Word4(string prompt)
        {
            Game game = new Game();

            game.ProcessInput(prompt);

            Assert.AreEqual(game.VOCABS[game.CMD1], "OIL");
            Assert.AreEqual(game.CMD1, 29);

            Assert.AreEqual(game.VOCABS[game.CMD2], "DUMBWAITER");
            Assert.AreEqual(game.CMD2, 59);

            Assert.AreEqual(game.VOCABS[game.CMD3], "OILCAN");
            Assert.AreEqual(game.CMD3, 48);
        }

        [TestMethod]
        public void RNG_test()
        {
            Game game = new Game();

            var result = game.RNG(3);

            Assert.IsTrue(result >= 1 && result <= 3);

            result = game.RNG(100);

            Assert.IsTrue(result >= 1 && result <= 100);

            int[] arr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = game.RNG(3);
                Assert.IsTrue(arr[i] >= 1 && arr[i] <= 3);
            }
        }
    }
}