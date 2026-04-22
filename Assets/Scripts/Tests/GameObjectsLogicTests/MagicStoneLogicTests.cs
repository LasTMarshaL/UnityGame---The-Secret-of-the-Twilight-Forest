using NUnit.Framework;


public class MagicStoneLogicTests
{
    [TestCase(true)]
    public void MagicStoneLogicTests_WasFound_ReturnExpectedResult(bool expectedResult)
    {
        // Arrange
        var magicStone = new MagicStoneLogic();

        // Act
        bool result = magicStone.WasFound();

        // Assert
        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(1, magicStone.IsFound);
    }
}
