public class ChestLogic
{
    public int IsOpened { get; private set; } = 0;


    public bool GetCoinsReward(int coins, PlayerWalletLogic wallet)
    {
        if (coins < 0 || wallet == null)
            return false;

        wallet.GetCoins(coins);
        return true;
    }

    public bool WasOpened()
    {
        IsOpened = 1;

        return true;
    }
}
