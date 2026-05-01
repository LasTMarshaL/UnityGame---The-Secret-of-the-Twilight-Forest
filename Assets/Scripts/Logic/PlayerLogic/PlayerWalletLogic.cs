public class PlayerWalletLogic
{
    public int Coins { get; private set; }

    public PlayerWalletLogic(int initialCoins) 
    {
        Coins = initialCoins > 0 ? initialCoins : 0;
    }

    public bool GetCoins(int amount)
    {
        if (amount < 0) 
            return false; 

        Coins += amount;

        return true; 
    }

    public bool SendCoins(int amount) 
    {
        if (amount < 0) 
            return false; 

        if (Coins >= amount)
        {
            Coins -= amount;

            return true; 
        }

        return false; 
    }

    public bool LoadCoins(int amount) 
    {
        if (amount < 0) 
            return false; 

        Coins = amount;

        return true; 
    }
}
