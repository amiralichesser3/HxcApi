namespace HxcApi.UnitTests;

[TestClass]
public class HeartbeatTest
{
    [TestMethod]
    public void AssertOneIsOne()
    {
        Assert.AreEqual(1, 1);
    }

    [TestMethod]
    public void AssertOneIsTwo()
    {
        Assert.AreEqual(1, 3);
    }
}