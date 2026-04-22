public class PlayerStatsLogic // This class manages the player's stats, including health level, damage level, and speed level.
{
    public int HealthLevel { get; private set; }
    public int DamageLevel { get; private set; }
    public int SpeedLevel { get; private set; }

    public int MaxLevel => 5;

    public int Health => 85 + HealthLevel * 15;
    public int Damage => 25 + DamageLevel * 5;
    public int Speed => 500 + SpeedLevel * 10;

    public PlayerStatsLogic(int healthLevel, int damageLevel, int speedLevel)
    {
        HealthLevel = UnityEngine.Mathf.Clamp(healthLevel, 1, MaxLevel);
        DamageLevel = UnityEngine.Mathf.Clamp(damageLevel, 1, MaxLevel);
        SpeedLevel = UnityEngine.Mathf.Clamp(speedLevel, 1, MaxLevel);
    }

    /// <summary>
    /// Increases the health level by one.
    /// </summary>
    /// <returns>true if the health level was upgraded.</returns>
    public bool UpgradeHealthLevel()
    {
        if (!CanUpgradeLevel(HealthLevel))
            return false;

        HealthLevel++;
        return true;
    }

    /// <summary>
    /// Increases the player's damage level by one.
    /// </summary>
    /// <returns>true if the damage level was successfully upgraded.</returns>
    public bool UpgradeDamageLevel() 
    {
        if (!CanUpgradeLevel(DamageLevel))
            return false;

        DamageLevel++;
        return true;
    }

    /// <summary>
    /// Increases the player's speed level by one.
    /// </summary>
    /// <returns>true if the speed level was successfully upgraded.</returns>
    public bool UpgradeSpeedLevel()
    {
        if (!CanUpgradeLevel(SpeedLevel))
            return false;

        SpeedLevel++;
        return true;
    }

    /// <summary>
    /// Determines whether the specified health level can be upgraded based on the maximum allowed level.
    /// </summary>
    /// <param name="level">The health level to evaluate.</param>
    /// <returns>true if the health level is less than or equal to the maximum level; otherwise, false.</returns>
    public bool CanUpgradeLevel(int level)
    {
        return level < MaxLevel;
    }

    /// <summary>
    /// Sets the current health level if the specified value is within the valid range.
    /// </summary>
    /// <param name="healthLevel">The health level to set.</param>
    /// <returns>true if the health level was set successfully; otherwise, false.</returns>
    public bool LoadHealthLevel(int healthLevel)
    {
        if (healthLevel < 0 || healthLevel > MaxLevel)
            return false;

        HealthLevel = healthLevel;

        return true;
    }

    /// <summary>
    /// Sets the current damage level if the specified value is within the valid range.
    /// </summary>
    /// <param name="damageLevel">The damage level to set.</param>
    /// <returns>true if the damagge level was set successfully; otherwise, false.</returns>
    public bool LoadDamageLevel(int damageLevel)
    {
        if (damageLevel < 0 || damageLevel > MaxLevel)
            return false;

        DamageLevel = damageLevel;

        return true;
    }

    /// <summary>
    /// Sets the current speed level if the specified value is within the valid range.
    /// </summary>
    /// <param name="speedLevel">The speed level to set.</param>
    /// <returns>true if the speed level was set successfully; otherwise, false.</returns>
    public bool LoadSpeedLevel(int speedLevel)
    {
        if (speedLevel < 0 || speedLevel > MaxLevel)
            return false;

        SpeedLevel = speedLevel;

        return true;
    }
}
