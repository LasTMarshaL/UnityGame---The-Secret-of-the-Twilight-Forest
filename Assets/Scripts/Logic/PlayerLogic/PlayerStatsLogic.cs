public class PlayerStatsLogic
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

    public bool UpgradeHealthLevel()
    {
        if (!CanUpgradeLevel(HealthLevel))
            return false;

        HealthLevel++;
        return true;
    }

    public bool UpgradeDamageLevel() 
    {
        if (!CanUpgradeLevel(DamageLevel))
            return false;

        DamageLevel++;
        return true;
    }

    public bool UpgradeSpeedLevel()
    {
        if (!CanUpgradeLevel(SpeedLevel))
            return false;

        SpeedLevel++;
        return true;
    }

    public bool CanUpgradeLevel(int level)
    {
        return level < MaxLevel;
    }

    public bool LoadHealthLevel(int healthLevel)
    {
        if (healthLevel < 0 || healthLevel > MaxLevel)
            return false;

        HealthLevel = healthLevel;

        return true;
    }

    public bool LoadDamageLevel(int damageLevel)
    {
        if (damageLevel < 0 || damageLevel > MaxLevel)
            return false;

        DamageLevel = damageLevel;

        return true;
    }

    public bool LoadSpeedLevel(int speedLevel)
    {
        if (speedLevel < 0 || speedLevel > MaxLevel)
            return false;

        SpeedLevel = speedLevel;

        return true;
    }
}
