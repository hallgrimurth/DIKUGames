using NUnit.Framework;
namespace galagaTests;

public class testMovementStrategy {

    [Test]
    public void TestMovementStrategy()
    {
        numEnemies = 8;
        var rowSquad = new Galaga.Squadron.Row(numEnemies);
        rowSquad.CreateEnemies(enemyStridesBlue, enemyStridesRed, 0.01f);
        rowZigZagMovement = new Galaga.MovementStrategy.ZigZagDown.MoveEnemies(rowSquad);
        rowNoMoveMovement = new Galaga.MovementStrategy.NoMove.MoveEnemies(rowSquad);

        Assert.AreNotEqual(rowZigZagMovement, rowNoMoveMovement);
    }

    [Test]
    public void TestMovementStrategy2()
    {
        numEnemies = 8;
        var rowSquad = new Galaga.Squadron.Row(numEnemies);
        var waveSquad = new Galaga.Squadron.Wave(numEnemies);
        rowSquad.CreateEnemies(enemyStridesBlue, enemyStridesRed, 0.00f);
        waveSquad.CreateEnemies(enemyStridesBlue, enemyStridesRed, 0.00f);
        rowZigZagMovement = new Galaga.MovementStrategy.ZigZagDown.MoveEnemies(rowSquad);
        waveZigZagMovement = new Galaga.MovementStrategy.ZigZagDown.MoveEnemies(waveSquad);

        Assert.AreNotEqual(rowZigZagMovement, waveZigZagMovement);
    }
}