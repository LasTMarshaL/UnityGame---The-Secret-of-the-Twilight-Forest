public class HealthGainPointLogic
{
    public int IsUsed { get; private set; } = 0;

    public bool GetHealthReward(int healthPoints, PlayerHealthLogic health)
    {
        if (healthPoints < 0 || health == null)
            return false;

        health.GetHealth(healthPoints);
        return true;
    }

    
    public bool WasUsed()
    {
        IsUsed = 1;

        return true;
    }
}
