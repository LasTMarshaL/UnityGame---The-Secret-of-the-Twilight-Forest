using NUnit.Framework;

public class PlayerWalletLogicTests
{
    [TestCase(0, 20, true, 20)]
    [TestCase(0, -20, false, 0)]
    [TestCase(0, 0, true, 0)]
    [TestCase(-50, 0, true, 0)]
    public void PlayerWalletLogicTests_GetCoins_ReturnExpetedResult(int initialCoins, int amount, bool expectedResult, int expectedAmount)
    {
        // Arrange
        var wallet = new PlayerWalletLogic(initialCoins);

        // Act
        bool result = wallet.GetCoins(amount);

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, wallet.Coins);
    }

    [TestCase(100, 20, true, 80)]
    [TestCase(3, 20, false, 3)]
    [TestCase(170, -89, false, 170)]
    [TestCase(-9, 20, false, 0)]
    public void PlayerWalletLogicTests_SpendCoins_ReturnExpectedResult(int initialCoins, int amount, bool expectedResult, int expectedAmount)
    {
        // Arrange
        var wallet = new PlayerWalletLogic(initialCoins);

        // Act
        bool result = wallet.SendCoins(amount);

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, wallet.Coins);
    }


    [TestCase(100, 80, true, 80)]
    [TestCase(170, -89, false, 170)]
    [TestCase(-9, 20, true, 20)]
    public void PlayerWalletLogicTests_LoadCoins_ReturnExpectedResult(int initialCoins, int amount, bool expectedResult, int expectedAmount)
    {
        // Arrange
        var wallet = new PlayerWalletLogic(initialCoins);

        // Act
        bool result = wallet.LoadCoins(amount);

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, wallet.Coins);
    }
}
