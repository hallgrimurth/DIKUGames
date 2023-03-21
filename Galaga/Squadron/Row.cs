using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using System;

namespace Galaga.Squadron {
    public class Row : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}

        public Row(int maxEnemies) {
            this.MaxEnemies = maxEnemies;
            this.Enemies = new EntityContainer<Enemy>();
        }

           public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            // aligned enemies in a row
            DynamicShape shape = new DynamicShape(new Vec2F(-0.5f, 0.8f), new Vec2F(0.1f, 0.1f));
            IBaseImage image = new ImageStride(80, enemyStride);
            IBaseImage redImage = new ImageStride(40, alternativeEnemyStride);

            for (int i = 0; i < MaxEnemies; i++) {
                Console.WriteLine("Enemy added");
                this.Enemies.AddEntity(new Enemy(shape, image, redImage));
                shape.Position.X += 0.1f;
                // shape.Position.Y += 0.1f;
            }
            // normal enemies in a row
            //   for (int i = 0; i < MaxEnemies; i++) {
            //         this.Enemies.AddEntity(new Enemy(
            //             new DynamicShape(new Vec2F(0.1f, 0.1f), 
            //                 new Vec2F(0.1f, 0.1f)),
            //             new ImageStride(80, enemyStride), new ImageStride(40, alternativeEnemyStride)));
            //     }
        }    
    }
}



