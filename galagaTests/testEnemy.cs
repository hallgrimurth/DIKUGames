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
            // IBaseImage enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            // IBaseImage enemyStridesBlue = ImageStride.CreateStride(4, Path.Combine("Assets", "Images", "BlueMonster.png"));

            // var startSpeed = 0.01f;

            // var enemy = new Enemy(new DynamicShape(new DIKUArcade.Math.Vec2F(0.1f,0.1f),new DIKUArcade.Math.Vec2F(0.1f,0.1f)), enemyStridesBlue, enemyStridesRed, startSpeed);
            // enemy.LoseHealth;
            // enemy.LoseHealth;
            // var newSpeed = enemy.Enrage().speed;
            // Assert.AreNotEqual(startSpeed, newSpeed);
        }
    }
}
