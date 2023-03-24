using NUnit.Framework;
<<<<<<< HEAD
namespace galagaTests;

public class TestEnemy {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
=======

//Testing the enemy class

namespace galagaTests
{
    public class testEnemy
    {
        [Test]
        public void TestEnemy()
        {
            //Arrange
            var enemy = new Galaga_Exercise_3.GalagaEntities.Enemy(new Galaga_Exercise_3.GalagaEntities.EnemyShape(new DIKUArcade.Math.Vec2F(0.1f, 0.1f), new DIKUArcade.Math.Vec2F(0.1f, 0.1f)), new Galaga_Exercise_3.GalagaEntities.MovementStrategy());
            //Act
            enemy.Move();
            //Assert
            Assert.AreEqual(enemy.Shape.Position.X, 0.1f);
            Assert.AreEqual(enemy.Shape.Position.Y, 0.1f);
        }
>>>>>>> states
    }
}