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
        public void MovementStrategy() {
            // Arrange
            var red = new Image("Assets/Images/RedMonster.png");
            var shape = new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
            Enemy enemy = new Enemy(shape, red, red, 0);
            var zigZagDown = new ZigZagDown();

            // Act
            zigZagDown.MoveEnemy(enemy);

            // Assert
            Assert.IsNotNull(enemy.shape);
        }

        [Test]
        public void MovementStrategy2() {
            // Arrange
            var red = new Image("Assets/Images/RedMonster.png");
            var shape = new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
            Enemy enemy = new Enemy(shape, red, red, 0);
            var zigZagDown = new ZigZagDown();

            // Act
            zigZagDown.MoveEnemy(enemy);

            // Assert
            Assert.IsNotNull(enemy.shape.Position);
        }

        [Test]
        public void MovementStrategy3() {
            // Arrange
            var red = new Image(@"C:\Users\Nynne\OneDrive\Dokumenter\KU\SU23\DIKUGames\Galaga\Assets\Images\RedMonster.png");
            var shape = new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));
            Enemy enemy = new Enemy(shape, red, red, 0);
            var zigZagDown = new ZigZagDown();

            // Act
            zigZagDown.MoveEnemy(enemy);

            // Assert
            Assert.IsNotNull(enemy.shape.Position.Y > 0.5);
        }
    }
