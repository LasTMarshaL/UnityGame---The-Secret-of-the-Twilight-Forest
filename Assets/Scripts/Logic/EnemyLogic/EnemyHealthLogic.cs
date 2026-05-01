public class EnemyHealthLogic 
{
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }

    public int IsKilled { get; private set; } = 0;

    public EnemyHealthLogic(int initialHealth, int maxHealth)
    {
        MaxHealth = maxHealth > 0 ? maxHealth: 100;
        Health = UnityEngine.Mathf.Clamp(initialHealth, 1, MaxHealth);
    
    }

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

    public bool LoadHealth(int amount)
    {
        if (amount < 0 || amount > MaxHealth)
            return false;

        Health = amount;

        return true;
    }

    public bool WasKilled()
    {
        IsKilled = 1;

        return true;
    }
}
