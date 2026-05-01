using NUnit.Framework;

public class PlayerStatsLogicTests
{
    [TestCase(1, 2, 1, 1, true)]
    [TestCase(6, 5, 1, 1, false)]
    [TestCase(-3, 2, 1, 1, true)]
    public void PlayerStatsLogicTests_UpgradeHealthLevel_ReturnExpectedResult(int healthLevel, int expectedHealthLevel, int damageLevel, int speedLevel, bool expectedResult)
    {
        var stats = new PlayerStatsLogic(healthLevel, damageLevel, speedLevel);

        bool result = stats.UpgradeHealthLevel();

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(stats.HealthLevel, expectedHealthLevel);
    }

    [TestCase(1, 1, 2, 1, true)]
    [TestCase(1, 6, 5, 1, false)]
    [TestCase(1, -3, 2, 1, true)]
    public void PlayerStatsLogicTests_UpgradeDamageLevel_ReturnExpectedResult(int healthLevel, int damageLevel, int expectedDamageLevel, int speedLevel, bool expectedResult)
    {
        var stats = new PlayerStatsLogic(healthLevel, damageLevel, speedLevel);

        bool result = stats.UpgradeDamageLevel();

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(stats.DamageLevel, expectedDamageLevel);
    }

    [TestCase(1, 2, true)]
    [TestCase(6, 5, false)]
    [TestCase(-3, 2, true)]
    public void PlayerStatsLogicTests_UpgradeSpeedLevel_ReturnExpectedResult(int speedLevel, int expectedSpeedLevel, bool expectedResult)
    {
        var stats = new PlayerStatsLogic(1, 1, speedLevel);

        bool result = stats.UpgradeSpeedLevel();

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(stats.SpeedLevel, expectedSpeedLevel);
    }

    [TestCase(1, true)]
    [TestCase(10, false)]
    [TestCase(-1, true)]
    [TestCase(5, false)]
    public void PlayerStatsLogicTests_CanUpgradeLevel_ReturnExpectedOutput(int level, bool expectedOutput)
    {
        var stats = new PlayerStatsLogic(level, 1, 1);

        bool result = stats.CanUpgradeLevel(level);

        Assert.AreEqual(expectedOutput, result);
    }
}
