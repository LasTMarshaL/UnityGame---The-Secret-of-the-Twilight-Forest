using NUnit.Framework;

public class PlayerHealthLogicTests
{
    [TestCase(20, 100, 40, true, 60)]
    [TestCase(80, 100, 40, true, 100)]
    [TestCase(70, 100, -76, false, 70)]
    [TestCase(-80, 30, 4, true, 5)]
    [TestCase(90, 40, 89, true, 40)]
    [TestCase(-40, -60, 78, true, 79)]
    public void PlyerHealthLogicTests_GetHealth_ReturnExpectedResult(int initilalHealth, int maxHealth, int amount, bool expectedResult, int expectedAmount)
    {
        // Arrange
        var health = new PlayerHealthLogic(initilalHealth, maxHealth);

        // Act
        bool result = health.GetHealth(amount);

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, health.Health);
    }


    [TestCase(20, 100, 40, true, 0)]
    [TestCase(80, 100, 40, true, 40)]
    [TestCase(70, 100, -76, false, 70)]
    [TestCase(-80, 30, 4, true, 0)]
    [TestCase(90, 40, 3, true, 37)]
    [TestCase(-40, -60, 78, true, 0)]
    public void PLayerHealthLogicTests_TakeDamage_ReturnExpectedResult(int initialHealth, int maxHealth, int amount, bool expectedResult, int expectedAmount)
    {
        // Arrange
        var health = new PlayerHealthLogic(initialHealth, maxHealth);

        // Act
        bool result = health.TakeDamage(amount);

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, health.Health);
    }

    [TestCase(100, 60, true, 60)]
    [TestCase(100, -76, false, 1)]
    [TestCase(-40, 78, true, 78)]
    [TestCase(100, (int)70.5f, true, 70)]
    public void PlayerHealthLogicTests_LoadHealth_ReturnExpectedResult(int maxHealth, int amount, bool expectedResult, int expectedAmount)
    {
        // Arrange
        var health = new PlayerHealthLogic(1, maxHealth);

        // Act
        bool result = health.LoadHealth(amount);

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, health.Health);
    }

    [TestCase(60, true, 60)]
    [TestCase(-76, false, 100)]
    [TestCase(90, true, 90)]
    public void PlayerHealthLogicTests_LoadMaxHealth_ReturnExpectedResult(int amount, bool expectedResult, int expectedAmount)
    {
        // Arrange
        var health = new PlayerHealthLogic(1, 100);

        // Act
        bool result = health.LoadMaxHealth(amount);

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, health.MaxHealth);
    }
}
