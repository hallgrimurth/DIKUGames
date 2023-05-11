using System;
using Breakout;
using Breakout.BreakoutStates;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace BreakoutTests
{

    [TestFixture]
    public class BallTest
    {

        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void BallTest1() {
            Vec2F pos = new Vec2F(0.4f, 0.2f);
            var ballImage = new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"));
            var ball = new Ball(pos, ballImage);
            Assert.That(ball.Direction.X, Is.EqualTo(0.01f));
        }

        [Test]
        public void BallTest2() {
            Vec2F pos = new Vec2F(0.4f, 0.2f);
            var ballImage = new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"));
            var ball = new Ball(pos, ballImage);
            Assert.That(ball.Direction.Y, Is.EqualTo(0.005f));
        }

        [Test]
        public void BallTest3() {
            Vec2F pos = new Vec2F(0.4f, 0.2f);
            var ballImage = new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"));
            var ball = new Ball(pos, ballImage);
            Assert.That(ball.Extent.X, Is.EqualTo(0.015f));
        }

        [Test]
        public void BallTest4() {
            Vec2F pos = new Vec2F(0.4f, 0.2f);
            var ballImage = new Image(Path.Combine(Constants.MAIN_PATH, "Assets", "Images", "ball.png"));
            var ball = new Ball(pos, ballImage);
            Assert.That(ball.Extent.Y, Is.EqualTo(0.015f));
        }
    }
}