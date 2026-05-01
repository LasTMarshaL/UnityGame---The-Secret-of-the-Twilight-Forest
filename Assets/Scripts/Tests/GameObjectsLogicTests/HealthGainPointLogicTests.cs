using NUnit.Framework;

public class HealthGainPointLogicTests
{
    [TestCase(10, true)]
    [TestCase(0, true)]
    [TestCase(-2, false)]
    public void HealthGainPointLogicTests_GetHealthReward_ReturnExpectedResult(int healthPoints, bool expectedResult)
    {
        var healthGainPoint = new HealthGainPointLogic();
        var health = new PlayerHealthLogic(50, 100);

        bool result = healthGainPoint.GetHealthReward(healthPoints, health);

        Assert.AreEqual(expectedResult, result);
    }

    [TestCase(true)]
    public void HealthGainPointLogicTests_WasUsed_ReturnExpectedResult(bool expectedResult)
    {
        var healthGainPoint = new HealthGainPointLogic();

        bool result = healthGainPoint.WasUsed();

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(1, healthGainPoint.IsUsed);
    }
}
