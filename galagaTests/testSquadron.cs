using NUnit.Framework;
namespace galagaTests;

public class testSquadron {

    [Test]
    public void TestSquadron()
    {   
        numEnemies = 8;
        var circleSquad = new Galaga.Squadron.Circle(numEnemies);
        var waveSquad = new Galaga.Squadron.Wave(numEnemies);

        var squadronCircleFormation = circleSquad.CreateEnemies(Galaga.GalagaStates.GameRunning.enemyStridesBlue, Galaga.GalagaStates.GameRunning.enemyStridesRed, 0.00f);
        var squadronWaveFormation = waveSquad.CreateEnemies(Galaga.GalagaStates.GameRunning.enemyStridesBlue, Galaga.GalagaStates.GameRunning.enemyStridesRed, 0.00f);

        Assert.AreNotEqual(squadronCircleFormation, squadronWaveFormation);
    }
}