using NUnit.Framework;
using DIKUArcade.Math;

namespace galagaTests {
    public class TestHealth {
        [Test]
        public void TestLoseHealth() {
            // Arrange
            var health = new Health(new Vec2F(0.75f, -0.2f), new Vec2F(0.4f, 0.4f));
            int expectedHealthPoints = health.HealthPoints - 1;
            
            // Act
            health.LoseHealth();
            int actualHealthPoints = health.HealthPoints;
            
            // Assert
            Assert.AreEqual(expectedHealthPoints, actualHealthPoints);
        }
    }
}
