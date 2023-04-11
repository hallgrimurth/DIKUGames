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
    public void TestSquadron1()
    {
        int numEnemies = 8;
        float enemySpeed = 0.01f;
        List<Image> Red = ImageStride.CreateStrides(2, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        List<Image> Blue = ImageStride.CreateStrides(4, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        var row = new Galaga.Squadron.Row(numEnemies);

    
        row.SetEnemieSpeed(0.01f);
        row.CreateEnemies(Blue, Red, 0.01f);

        foreach(Enemy enemies in row.Enemies) {
            Assert.That(enemySpeed, Is.EqualTo(enemies.speed));
        }
    }

    [Test]
    public void TestSquadron2()
    {
        int numEnemies = 8;
        List<Image> Red = ImageStride.CreateStrides(2, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        List<Image> Blue = ImageStride.CreateStrides(4, @"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
        var row = new Galaga.Squadron.Row(numEnemies);
        var wave = new Galaga.Squadron.Wave(numEnemies);

    
        row.SetEnemieSpeed(0.01f);
        row.CreateEnemies(Blue, Red, 0.01f);
        wave.SetEnemieSpeed(0.02f);
        wave.CreateEnemies(Blue, Red, 0.02f);
        

        foreach(Enemy rowenemies in row.Enemies) {
            foreach(Enemy waveenemies in wave.Enemies) {
                Assert.That(waveenemies.speed, Is.Not.EqualTo(rowenemies.speed));
            }
        }

    }
}