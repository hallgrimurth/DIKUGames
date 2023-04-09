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

//Testing the enemy class

namespace galagaTests
{
    public class testEnemy
    {
  

        [Test]
        public void DecreaseHitpoint() {
            // Arrange
            var red = new Image(@"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
            var shape = new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
            var enemy = new Enemy(shape, red, red, 0);
            enemy.Hitpoints = 3;

            // Act
            enemy.DecreaseHitpoints();

            // Assert
            Assert.AreEqual(2, enemy.Hitpoints);
        }
    }
}
