using System;
using DIKUArcade.Math;

namespace Breakout {
    class VectorOperations { 

        static public float Length(Vec2F vec) {
            float length = (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            return length;
        }

        static public Vec2F Normalize(Vec2F vec) {
            float length = Length(vec);
            vec.X = vec.X / length;
            vec.Y = vec.Y / length;
            return vec;
        }

        static public float DotProduct(Vec2F vec1, Vec2F vec2) {
            float dotProduct = vec1.X * vec2.X + vec1.Y * vec2.Y;
            return dotProduct;
        }

        static public Vec2F Perpendicular(Vec2F vec) {
            float temp = vec.X;
            vec.X = -vec.Y;
            vec.Y = temp;
            return vec;
        }

        static public Vec2F Projection(Vec2F vec1, Vec2F vec2) {
            float dotProduct = DotProduct(vec1, vec2);
            float length = Length(vec2);
            Vec2F projection = new Vec2F(dotProduct * vec2.X / length, dotProduct * vec2.Y / length);
            return projection;
        }

        //Reflection of a vector calculated with the formula V = v - 2 * projection(v, n)
        static public Vec2F Reflection(Vec2F vec, Vec2F normal) {
            Vec2F projection = Projection(vec, normal);
            Vec2F reflection = new Vec2F(vec.X - 2 * projection.X, vec.Y - 2 * projection.Y);
            return reflection;
        }
    }
}