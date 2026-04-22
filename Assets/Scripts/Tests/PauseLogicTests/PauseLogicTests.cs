using NUnit.Framework;

public class PauseLogicTests
{
    [TestCase(true, true)]
    [TestCase(false, false)]
    public void PauseLogicTests_ChangePauseState_ReturnExpectedResult(bool state, bool expectedResult)
    {
        // Arrange
        // Do not need

        // Act
        bool result = PauseLogic.ChangePauseState(state);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
