using NUnit.Framework;

public class PauseLogicTests
{
    [TestCase(true, true)]
    [TestCase(false, false)]
    public void PauseLogicTests_ChangePauseState_ReturnExpectedResult(bool state, bool expectedResult)
    {
        bool result = PauseLogic.ChangePauseState(state);

        Assert.AreEqual(expectedResult, result);
    }
}
