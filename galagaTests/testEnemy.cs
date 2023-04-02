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
        public void TestEnemy()
        {
            //Arrange
            // var enemy = new Galaga_Exercise_3.GalagaEntities.Enemy(new Galaga_Exercise_3.GalagaEntities.EnemyShape(new DIKUArcade.Math.Vec2F(0.1f, 0.1f), new DIKUArcade.Math.Vec2F(0.1f, 0.1f)), new Galaga_Exercise_3.GalagaEntities.MovementStrategy());
            //Act
            // enemy.Move();
            //Assert
            // Assert.AreEqual(enemy.Shape.Position.X, 0.1f);
            // Assert.AreEqual(enemy.Shape.Position.Y, 0.1f);
        }



        [Test]
        public void TestEnemyEnrage()
        {
            // var startSpeed = 0.01f;
            // var enemy = new Galaga.Enemy.Enemy(new DynamicShape(new DIKUArcade.Math.Vec2F(0.1f,0.1f),new DIKUArcade.Math.Vec2F(0.1f,0.1f)), Galaga.GalagaStates.GameRunning.enemyStridesBlue, Galaga.GalagaStates.GameRunning.enemyStridesRed, startSpeed);
            // enemy.LoseHealth;
            // enemy.LoseHealth;
            // var newSpeed = enemy.Enrage().speed;
            // Assert.AreNotEqual(startSpeed, newSpeed);
        }
    }
}
