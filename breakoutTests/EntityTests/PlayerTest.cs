// using System;
// using Breakout;
// using DIKUArcade.Graphics;

// using DIKUArcade.GUI;
// using DIKUArcade.Events;
// using System.Collections.Generic;

// namespace BreakoutTests
// {

//     [TestFixture]
//     public class PlayerTest
//     {
//         private Player player;

//        [SetUp]
//         public void Setup()
//         {
//             // Window.CreateOpenGLContext();
//             //adding enitites
//             player = new Player(new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.05f)), new Image("Assets/Images/paddle.png"));      
            
//         }

//         [Test]
//         public void TestPlayerMovement()
//         {
//             player.Move();
//             Assert.That(player.Shape.Position.Y, Is.EqualTo(0.05f));
//         }

//         [Test]
//         public void TestPlayerMovesRight() {
//             player.SetMoveRight(true);
//             player.Move();
//             Assert.That(player.Shape.Position.X, Is.EqualTo((0.41f)));
//         }


//         [Test]
//         public void TestPlayerMovesLeft() {
//             player.SetMoveLeft(true);
//             player.Move();
//             Assert.That(player.Shape.Position.Y, Is.EqualTo((0.05f)));
//         }
//     }

// }