public class EnemyHealthLogic // This class manages the health of an enemy.
{
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }

    public int IsKilled { get; private set; } = 0;

    public EnemyHealthLogic(int initialHealth, int maxHealth)
    {
        MaxHealth = maxHealth > 0 ? maxHealth: 100;
        Health = UnityEngine.Mathf.Clamp(initialHealth, 1, MaxHealth);
    
    }

    /// <summary>
    /// Reduces health by the specified damage amount if valid.
    /// </summary>
    /// <param name="amount">The amount of damage to apply. Must be non-negative.</param>
    /// <returns>true if damage was applied; otherwise, false.</returns>
    public bool TakeDamage(int amount)
    {
        if (amount < 0)
            return false;


        if (Health > amount)
        {
            Health -= amount;
            return true; 
        }

        Health = 0;
        return true;
    }

    /// <summary>
    /// Sets the current health to the specified amount if it is within valid bounds.
    /// </summary>
    /// <param name="amount">The health value to set. Must be between 0 and MaxHealth.</param>
    /// <returns>true if the health was set successfully; otherwise, false.</returns>
    public bool LoadHealth(int amount)
    {
        if (amount < 0 || amount > MaxHealth)
            return false;

        Health = amount;

        return true;
    }

    /// <summary>
    /// Sets flag, that enemy was killed.
    /// </summary>
    public bool WasKilled()
    {
        IsKilled = 1;

        return true;
    }
}
