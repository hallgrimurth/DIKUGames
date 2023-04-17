using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;

namespace Breakout{ 
    class LevelManager{
        private char[,] map;
        public LevelManager(string filePath){
            LoadMap(filePath);
        } 

        private void LoadMap(string filePath) {
            if (!File.Exists(filePath)) {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            string[] lines = File.ReadAllLines(filePath);

            int rows = lines.Length;
            int cols = lines[0].Length;

            map = new char[rows, cols];

            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    map[row, col] = lines[row][col];
                }
            }
        }

        public void PrintMap() {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    Console.Write(map[row, col]);
                }
                Console.WriteLine();
            }
        }

    }
}

