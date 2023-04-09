using NUnit.Framework;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;
using Galaga.GalagaStates;
using Galaga;

namespace galagaTests;

public class testSquadron {

    [Test]
    public void TestSquadron()
    {   
        int numEnemies = 8;
        List<Image> Red = ImageStride.CreateStrides(2, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        List<Image> Blue = ImageStride.CreateStrides(4, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        var circleSquad = new Galaga.Squadron.Circle(numEnemies);
        var waveSquad = new Galaga.Squadron.Wave(numEnemies);

        // var squadronCircleFormation = circleSquad.CreateEnemies(Blue, Red, 0.00f);
        // var squadronWaveFormation = waveSquad.CreateEnemies(Blue, Red, 0.00f);

        // Assert.AreNotEqual(squadronCircleFormation, squadronWaveFormation);
    }
}