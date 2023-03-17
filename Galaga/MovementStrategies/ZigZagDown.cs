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
        public float s = 0.0003f; 
        public float p = 0.045f; 
        public float a = 0.05f; 
        public DynamicShape shape;

        
        public void MoveEnemy (Enemy enemy){
            float startPosY = enemy.startPos.Y;
            float startPosX = enemy.startPos.X;
            float y = startPosY + s;
            float x = startPosX + a*(float)Math.Sin(shape.Position.Y-startPosY)/p;
            enemy.shape.SetPosition(new Vec2F(x,y));
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach(Enemy enemy in enemies){
                MoveEnemy(enemy);
            }
        }
    }
}