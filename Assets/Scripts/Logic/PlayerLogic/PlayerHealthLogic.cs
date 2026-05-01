public class PlayerHealthLogic
{
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public bool IsAlive => Health > 0;

    public PlayerHealthLogic(int initialHealth, int maxHealth) 
    {
        MaxHealth = maxHealth > 1 ? maxHealth : 100;
        Health = UnityEngine.Mathf.Clamp(initialHealth, 1, MaxHealth);
    }

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

    public bool LoadHealth(int amount) 
    {
        if (amount < 0 || amount > MaxHealth) 
            return false; 

        Health = amount; 
        return true;
    }

    public bool LoadMaxHealth(int amount) 
    {
        if (amount < 1) 
            return false; 

        MaxHealth = amount; 
        return true;
    }
}
