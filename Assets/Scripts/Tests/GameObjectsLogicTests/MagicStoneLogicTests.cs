using NUnit.Framework;


public class MagicStoneLogicTests
{
    [TestCase(true)]
    public void MagicStoneLogicTests_WasFound_ReturnExpectedResult(bool expectedResult)
    {
        var magicStone = new MagicStoneLogic();

        bool result = magicStone.WasFound();

        Assert.AreEqual(expectedResult, result);
        Assert.AreEqual(1, magicStone.IsFound);
    }
}
