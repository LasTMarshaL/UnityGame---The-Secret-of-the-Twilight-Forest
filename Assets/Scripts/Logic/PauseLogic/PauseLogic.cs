public class PauseLogic
{
    public static bool IsPause { get; private set; } = false;

    public static bool ChangePauseState(bool state)
    {
        IsPause = state;

        return state;
    }
}