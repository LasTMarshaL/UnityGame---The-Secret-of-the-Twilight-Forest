public class PauseLogic // This class manages the pause state of the game.
{
    public static bool IsPause { get; private set; } = false;

    /// <summary>
    /// Changes state of pause for dialog.
    /// </summary>
    /// <param name="state"></param>
    public static bool ChangePauseState(bool state)
    {
        IsPause = state;

        return state;
    }
}