using System;
using DIKUArcade.Entities;
using Galaga.MovementStrategy;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public float s = 0.0003; 
        public float p = 0.045; 
        public float a = 0.05;  
        public void MoveEnemy (Enemy enemy){
        
        

        }

        public void MoveEnemies (EntityContainer<Enemy> enemies){
            foreach(Enemy enemy in EntityContainer<Enemy> enemies){
                MoveEnemy(enemy);
            }
        }
    }
}