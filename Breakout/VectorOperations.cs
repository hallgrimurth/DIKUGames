using System;
using DIKUArcade.Math;

namespace Breakout
{
    /// <summary>
    /// The VectorOperations class provides utility methods for vector operations.
    /// </summary>
    public class VectorOperations
    {
        /// <summary>
        /// Calculates the length of a vector.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <returns>The length of the vector.</returns>
        static public float Length(Vec2F vec)
        {
            float length = (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
            return length;
        }

        /// <summary>
        /// Normalizes a vector.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <returns>The normalized vector.</returns>
        static public Vec2F Normalize(Vec2F vec)
        {
            float length = Length(vec);
            vec.X = vec.X / length;
            vec.Y = vec.Y / length;
            return vec;
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="vec1">The first vector.</param>
        /// <param name="vec2">The second vector.</param>
        /// <returns>The dot product of the two vectors.</returns>
        static public float DotProduct(Vec2F vec1, Vec2F vec2)
        {
            float dotProduct = vec1.X * vec2.X + vec1.Y * vec2.Y;
            return dotProduct;
        }

        /// <summary>
        /// Calculates the perpendicular vector of a given vector.
        /// </summary>
        /// <param name="vec">The vector.</param>
        /// <returns>The perpendicular vector.</returns>
        static public Vec2F Perpendicular(Vec2F vec)
        {
            float temp = vec.X;
            vec.X = -vec.Y;
            vec.Y = temp;
            return vec;
        }

        /// <summary>
        /// Calculates the projection of a vector onto another vector.
        /// </summary>
        /// <param name="vec1">The vector to project.</param>
        /// <param name="vec2">The vector onto which to project.</param>
        /// <returns>The projection of vec1 onto vec2.</returns>
        static public Vec2F Projection(Vec2F vec1, Vec2F vec2)
        {
            float dotProduct = DotProduct(vec1, vec2);
            float length = Length(vec2);
            Vec2F projection = new Vec2F(dotProduct * vec2.X / length, dotProduct * vec2.Y / length);
            return projection;
        }

        /// <summary>
        /// Calculates the reflection of a vector given a surface normal.
        /// </summary>
        /// <param name="vec">The vector to reflect.</param>
        /// <param name="normal">The surface normal.</param>
        /// <returns>The reflected vector.</returns>
        static public Vec2F Reflection(Vec2F vec, Vec2F normal)
        {
            Vec2F projection = Projection(vec, normal);
            Vec2F reflection = new Vec2F(vec.X - 2 * projection.X, vec.Y - 2 * projection.Y);
            return reflection;
        }
    }
}
