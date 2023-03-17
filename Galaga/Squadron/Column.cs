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
    public class Column : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}
         public  Random rand = new Random(); // For randomizing enemy speed
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
            // enemies in columns on the left and right
            for (int i = 0; i < MaxEnemies/2; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride), new ImageStride(40, alternativeEnemyStride)));
            }
        }
    }
}