using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga.Squadron {
    public class Wave : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
            // wave pattern
            for (int i = 0; i < numEnemies; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (0.9f - (float)i*0.03f)), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, BlueMonster), new ImageStride(80, enemyStridesRed)));
            }
        }
    }
}