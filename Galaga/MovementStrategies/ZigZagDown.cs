using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public float s; 
        public float p = 0.045f; 
        public float a = 0.05f;
        public void MoveEnemy (Enemy enemy){
            s = enemy.speed - 0.002f;
            Vec2F startPos = enemy.startPos;
            Vec2F enemyPos = enemy.shape.Position;
            float y = enemyPos.Y + s;
            float x = startPos.X + a*(float)Math.Sin(((2*Math.PI)*(enemyPos.Y-startPos.Y))/p);
            enemy.shape.SetPosition(new Vec2F(x,y));
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach(Enemy enemy in enemies){
                MoveEnemy(enemy);
            }
        }
    }
}