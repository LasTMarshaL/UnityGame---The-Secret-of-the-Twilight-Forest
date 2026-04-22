public class HealthGainPointLogic // This class manages the logic for a health gain point.
{
    public int IsUsed { get; private set; } = 0; // Int is used for PlayerPrefs saves.

    /// <summary>
    /// Updates the player's health using the specified health points.
    /// </summary>
    /// <param name="healthPoints">The number of health points to set.</param>
    /// <param name="health">The player health logic instance to update.</param>
    /// <returns>true if the health was updated; otherwise, false.</returns>
    public bool GetHealthReward(int healthPoints, PlayerHealthLogic health)
    {
        if (healthPoints < 0 || health == null)
            return false;

        health.GetHealth(healthPoints);
        return true;
    }

    
    /// <summary>
    /// Sets the usage flag and returns a value indicating successful operation.
    /// </summary>
    /// <returns>true if the usage flag was set; otherwise, false.</returns>
    public bool WasUsed()
    {
        IsUsed = 1;

        return true;
    }
}
