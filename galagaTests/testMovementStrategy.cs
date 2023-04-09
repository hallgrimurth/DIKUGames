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
using Galaga.MovementStrategy;
using Galaga;

namespace galagaTests;

public class testMovementStrategy {

    [Test]
    public void TestMovementStrategy()
    {
        int numEnemies = 8;
        var rowSquad = new Galaga.Squadron.Row(numEnemies);
        // rowSquad.CreateEnemies(GalagaStates.GameRunning.enemyStridesBlue, Galaga.GalagaStates.GameRunning.enemyStridesRed, 0.01f);
        var rowZigZag = new ZigZagDown();
        var rowNoMove = new NoMove();
        // rowZigZag.MoveEnemies(rowSquad);
        // rowSquad.ZigZagDown();

        // Assert.AreNotEqual(rowZigZag, rowNoMove);
    }

    [Test]
    public void TestMovementStrategy2()
    {
        // List<Image> enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
        // List<Image> enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        // int numEnemies = 8;
        // var rowSquad = new Galaga.Squadron.Row(numEnemies);
        // var waveSquad = new Galaga.Squadron.Wave(numEnemies);
        // rowSquad.CreateEnemies(enemyStridesBlue, enemyStridesRed, 0.00f);
        //waveSquad.CreateEnemies(enemyStridesBlue, enemyStridesRed, 0.00f);
        
        // var rowZigZag = new Galaga.MovementStrategy.ZigZagDown.MoveEnemies(rowSquad);
        // var waveZigZag = new Galaga.MovementStrategy.ZigZagDown.MoveEnemies(waveSquad);

        // Assert.AreNotEqual(rowZigZag, waveZigZag);
    }
}