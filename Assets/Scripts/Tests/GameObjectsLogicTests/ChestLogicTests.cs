using NUnit.Framework;

public class ChestLogicTests
{
    [TestCase(10, true)]
    [TestCase((int)12.5, true)]
    [TestCase(0, true)]
    [TestCase(-2, false)]
    public void ChestLogicTests_GetReward_ReturnExpectedResult(int cois, bool expectedResult)
    {
        // Arrange
        var wallet = new PlayerWalletLogic(0);
        var chest = new ChestLogic();

        // Act
        bool result = chest.GetCoinsReward(cois, wallet);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [TestCase(true)]
    public void ChestLogicTests_WasOpened_ReturnExpectedResult(bool expectedResult)
    {
        // Arrange
        var chest = new ChestLogic();

        // Act
        bool result = chest.WasOpened();

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(1, chest.IsOpened);
    }

}
