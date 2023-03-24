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
        public Column(int maxEnemies) {
            this.MaxEnemies = maxEnemies;
            this.Enemies = new EntityContainer<Enemy>();
        }
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride, 
            float speed){
            // enemies in two columns on the left and right side of the screen
            for (int i = 0; i < MaxEnemies/2; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f, (1.1f - (float)i*0.05f)), 
                        new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride), 
                        speed));
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.9f, (1.1f - (float)i*0.05f)), 
                        new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride), 
                        speed));
            }
        }

        public void SetEnemieSpeed(float speed) {
            foreach (Enemy enemy in Enemies) {
                enemy.speed = speed;
            }
        }
    }
}