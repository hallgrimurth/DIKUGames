using NUnit.Framework;
namespace galagaTests;

public class testPlayer {

    [Test]
    public void TestPlayer()
    {
        var player = new Galaga.Player.Player(new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)), new ImageStride(160, playerStrides));
        
        player.Move();

        Assert.AreEqual(player.Shape.Position.X, 0.1f);
        Assert.AreEqual(player.Shape.Position.Y, 0.1f);
    }
}