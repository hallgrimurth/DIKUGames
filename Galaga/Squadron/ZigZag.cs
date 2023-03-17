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
    public class ZigZag : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}
         public  Random rand = new Random(); // For randomizing enemy speed
        public void CreateEnemies (List<Image> enemyStride,List<Image> alternativeEnemyStride){       
            // zigzag pattern
            for (int i = 0; i < MaxEnemies; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - ((float)i*0.1f%0.2f))), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            }

        }
    }
}
