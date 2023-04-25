// using System;
// using Breakout;
// using DIKUArcade.Math;

// namespace BreakoutTests
// {

//     [TestFixture]
//     public class EntityHitTest
//     {

//         private string path;
//         private LevelManager level;
//         private Block block;
//         [SetUp]
//         public void Setup()
//         {
//             //adding enitites
//             block = new Block(new DynamicShape(new Vec2F(0.5, 0.95f - 0.5), new Vec2F(0.5, 0.5)), new Image(Path.Combine("Assets", "Images", "block.png")));       
            
//         }

//         [Test]
//         public void TestHit()
//         {
//             //Testing if block is hit
//             block.Hit();
//             Assert.AreEqual(block.Health, 2);
//         }
//     }
// }