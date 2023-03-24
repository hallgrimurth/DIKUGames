using NUnit.Framework;
namespace galagaTests;

public class testPlayer {

    [Test]
    public void TestPlayer()
    {
        //var player = new Galaga.Player.Player(new DynamicShape(new DIKUArcade.Math.Vec2F(0.1f, 0.1f), new DIKUArcade.Math.Vec2F(0.1f, 0.1f), new IBaseImage()); 
        
        player.Move();

        Assert.AreEqual(player.Shape.Position.X, 0.1f);
        Assert.AreEqual(player.Shape.Position.Y, 0.1f);
    }
}