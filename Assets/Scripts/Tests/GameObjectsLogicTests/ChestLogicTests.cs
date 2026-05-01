using NUnit.Framework;

public class ChestLogicTests
{
    [TestCase(10, true)]
    [TestCase((int)12.5, true)]
    [TestCase(0, true)]
    [TestCase(-2, false)]
    public void ChestLogicTests_GetReward_ReturnExpectedResult(int cois, bool expectedResult)
    {
        var wallet = new PlayerWalletLogic(0);
        var chest = new ChestLogic();

        bool result = chest.GetCoinsReward(cois, wallet);

        Assert.AreEqual(expectedResult, result);
    }

    [TestCase(true)]
    public void ChestLogicTests_WasOpened_ReturnExpectedResult(bool expectedResult)
    {
        var chest = new ChestLogic();

        bool result = chest.WasOpened();

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(1, chest.IsOpened);
    }

}
