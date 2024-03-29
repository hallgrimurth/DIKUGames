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
        public Wave(int maxEnemies) {
            this.MaxEnemies = maxEnemies;
            this.Enemies = new EntityContainer<Enemy>();
        }
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride, 
            float speed){
            // wave pattern
            for (int i = 0; i < MaxEnemies; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, (1.15f - (float)i*0.03f)), 
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