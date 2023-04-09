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
        List<Image> Red = ImageStride.CreateStrides(2, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        List<Image> Blue = ImageStride.CreateStrides(4, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        var row = new Galaga.Squadron.Row(numEnemies);
        var rowZigZag = new ZigZagDown();
        var rowNoMove = new NoMove();

        row.CreateEnemies(Blue, Red, 0.01f);

        // var rowZigZagMove = rowZigZag.MoveEnemies(row.Enemies);
        // var rowNoMove = rowNoMove.MoveEnemies(row.Enemies);

        // Assert.AreNotEqual(rowZigZagMove, rowNoMove);
    }

    [Test]
    public void TestMoveEnemies() {
        // Arrange
        int numEnemies = 8; 
        List<Image> Red = ImageStride.CreateStrides(2, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        List<Image> Blue = ImageStride.CreateStrides(4, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        var wave = new Galaga.Squadron.Wave(numEnemies);
        var row = new Galaga.Squadron.Row(numEnemies);
        var movementStrategy = new Down();


        // Act
        row.CreateEnemies(Blue, Red, -0.002f);
        wave.CreateEnemies(Blue, Red, -0.002f);

        // var rowMove = movementStrategy.MoveEnemies(row.Enemies);
        // var waveMove = movementStrategy.MoveEnemies(wave.Enemies);

        // Assert
        // Assert.AreNotEqual(rowMove, waveMove);
        }
    }
