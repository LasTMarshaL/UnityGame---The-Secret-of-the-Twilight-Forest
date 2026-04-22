public class ChestLogic // This class manages the logic for a chest.
{
    public int IsOpened { get; private set; } = 0; // Int is used for PlayerPrefs saves.

   /// <summary>
   /// Adds the specified number of coins to the player's wallet if valid.
   /// </summary>
   /// <param name="coins">The number of coins to add.</param>
   /// <param name="wallet">The player's wallet logic instance.</param>
   /// <returns>true if the reward was successfully added; otherwise, false.</returns>
    public bool GetCoinsReward(int coins, PlayerWalletLogic wallet)
    {
        if (coins < 0 || wallet == null)
            return false;

        wallet.GetCoins(coins);
        return true;
    }

    /// <summary>
    /// Indicates that the chest has been opened and updates its state accordingly.
    /// </summary>
    /// <returns>true if the chest was marked as opened.</returns>
    public bool WasOpened()
    {
        IsOpened = 1;

        return true;
    }
}
