using System;
using DIKUArcade.Entities;
using Galaga.MovementStrategy;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public float s = 0.0003; 
        public float p = 0.045; 
        public float a = 0.05; 
        public DynamicShape shape;

        
        public void MoveEnemy (Enemy enemy){
            shape.Position.Y = Galaga.Enemy.startPos[1] + s;
            shape.Position.X = Galaga.Enemy.startPos[0] + a*(Math.Sin(shape.Position.Y-startPos[1])/p);
        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach(Enemy enemy in EntityContainer<Enemy> enemies){
                MoveEnemy(enemy);
            }
        }
    }
}