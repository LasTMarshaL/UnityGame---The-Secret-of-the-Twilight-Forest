public class MagicStoneLogic // This class manages the logic for a magic stone in the game.
{
    public int IsFound { get; private set; } = 0; // Int is used for PlayerPrefs saves.

    /// <summary>
    /// Indicates that the chesthas been opened and updates its state accordingly.
    /// </summary>
    public bool WasFound()
    {
        IsFound = 1;

        return true;
    }
}
