public class PlayerWalletLogic // This class manages the player's wallet.
{
    public int Coins { get; private set; }

    public PlayerWalletLogic(int initialCoins) 
    {
        Coins = initialCoins > 0 ? initialCoins : 0;
    }

    /// <summary>
    /// Handles getting coins by the player.
    /// </summary>
    /// <param name="amount">The number of coins to add.</param>
    /// <returns>true if the coins were successfully added; otherwise, false.</returns>
    public bool GetCoins(int amount)
    {
        if (amount < 0) 
            return false; 

        Coins += amount;

        return true; 
    }

    /// <summary>
    /// Attempts to deduct the specified number of coins if sufficient balance is available.
    /// </summary>
    /// <param name="amount">The number of coins to spend.</param>
    /// <returns>true if the coins were successfully spent; otherwise, false.</returns>
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

    /// <summary>
    /// Sets the number of coins if the specified amount is non-negative.
    /// </summary>
    /// <param name="amount">The number of coins to load. Must be zero or greater.</param>
    /// <returns>true if the coins were loaded successfully; otherwise, false.</returns>
    public bool LoadCoins(int amount) 
    {
        if (amount < 0) 
            return false; 

        Coins = amount;

        return true; 
    }
}
