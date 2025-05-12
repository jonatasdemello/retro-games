namespace UncleTayHouse.Tests.Unit
{
    [TestClass]
    public sealed class RngTests
    {
        [TestMethod]
        public void RNG_test()
        {
            var result = Utils.RNG(3);

            Assert.IsTrue(result >= 1 && result <= 3);

            result = Utils.RNG(100);

            Assert.IsTrue(result >= 1 && result <= 100);

            int[] arr = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Utils.RNG(3);
                Assert.IsTrue(arr[i] >= 1 && arr[i] <= 3);
            }
        }
    }
}
