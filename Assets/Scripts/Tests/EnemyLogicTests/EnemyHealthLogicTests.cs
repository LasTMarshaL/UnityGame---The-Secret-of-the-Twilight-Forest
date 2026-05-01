using NUnit.Framework;

public class EnemyHealthLogicTests
{
    [TestCase(20, 100, 40, true, 0)]
    [TestCase(80, 100, 40, true, 40)]
    [TestCase(70, 100, -76, false, 70)]
    [TestCase(-80, 30, 4, true, 0)]
    [TestCase(90, 40, 3, true, 37)]
    [TestCase(-40, -60, 78, true, 0)]
    public void EnemyHealthLogicTests_TakeDamage_ReturnExpectedResult(int initilalHealth, int maxHealth, int amount, bool expectedResult, int expectedAmount)
    {
        var health = new EnemyHealthLogic(initilalHealth, maxHealth);

        bool result = health.TakeDamage(amount);

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, health.Health);
    }

    [TestCase(100, 20, true, 20)]
    [TestCase(80, 40, true, 40)]
    [TestCase(70, -76, false, 1)]
    [TestCase(-80, 30, true, 30)]
    [TestCase(90, 3, true, 3)]
    [TestCase(-40, -60, false, 1)]
    public void EnemyHealthLogicTests_LoadHealth_ReturnExpectedResult(int maxHealth, int amount, bool expectedResult, int expectedAmount)
    {
        var health = new EnemyHealthLogic(1, maxHealth);

        bool result = health.LoadHealth(amount);

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(expectedAmount, health.Health);
    }


    [TestCase(true)]
    public void EnemyHealthLogicTests_WasKilled_ReturnExpectedResult(bool expectedResult)
    {
        var health = new EnemyHealthLogic(1, 1);

        bool result = health.WasKilled();

        Assert.AreEqual(expectedResult, result);
    }
}
