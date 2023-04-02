using NUnit.Framework;
namespace galagaTests;

public class testHealth {

    [Test]
    public void TestHealth()
    {
        var health = new Health(new DIKUArcade.Math.Vec2F(0.75f, -0.2f), new DIKUArcade.Math.Vec2F(0.4f, 0.4f));
        health.LoseHealth();
        Assert.AreEqual(2, health);
    }
}