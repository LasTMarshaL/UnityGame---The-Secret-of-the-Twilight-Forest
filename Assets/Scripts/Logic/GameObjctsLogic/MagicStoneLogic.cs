public class MagicStoneLogic
{
    public int IsFound { get; private set; } = 0; 

    public bool WasFound()
    {
        IsFound = 1;

        return true;
    }
}
