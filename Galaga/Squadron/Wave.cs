using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;

namespace Galaga.Squadron {
    public class Wave : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}
         public  Random rand = new Random(); // For randomizing enemy speed
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
            List<Image> enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            List<Image> enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            // wave pattern
            for (int i = 0; i < MaxEnemies; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - (float)i*0.03f)), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStridesBlue), new ImageStride(80, enemyStridesRed)));
            }
        }
    }
}