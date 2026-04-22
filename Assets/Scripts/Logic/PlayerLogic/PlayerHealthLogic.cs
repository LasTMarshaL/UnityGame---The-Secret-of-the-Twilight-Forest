public class PlayerHealthLogic // This class manages the player's health.
{
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public bool IsAlive => Health > 0;

    public PlayerHealthLogic(int initialHealth, int maxHealth) 
    {
        MaxHealth = maxHealth > 1 ? maxHealth : 100;
        Health = UnityEngine.Mathf.Clamp(initialHealth, 1, MaxHealth); // Set Health between 1 and maxHealth
    }

    /// <summary>
    /// Adds the specified amount to the current health, ensuring it does not exceed the maximum health.
    /// </summary>
    /// <param name="amount">The amount of health to add.</param>
    /// <param name="maxHealth">The maximum allowable health value.</param>
    /// <returns>true if health was added; otherwise, false.</returns>
    public bool GetHealth(int amount) 
    {
        if (amount < 0) 
            return false; 


        if (amount + Health <= MaxHealth) 
        {
            Health += amount; 
            return true; 
        }
        
        Health = MaxHealth; 
        return true; 
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


        if (Health >= amount) 
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
    /// <param name="amount"></param>
    /// <returns>true if the current health was set successfully; otherwise, false.</returns>
    public bool LoadHealth(int amount) 
    {
        if (amount < 0 || amount > MaxHealth) 
            return false; 

        Health = amount; 
        return true;
    }

    /// <summary>
    /// Sets the maximum health to the specified amount if it is within valid bounds.
    /// </summary>
    /// <param name="amount">The health value to set. Must be more then 1.</param>
    /// <returns>true if the maximum health was set successfully; otherwise, false.</returns>
    public bool LoadMaxHealth(int amount) 
    {
        if (amount < 1) 
            return false; 

        MaxHealth = amount; 
        return true;
    }
}
