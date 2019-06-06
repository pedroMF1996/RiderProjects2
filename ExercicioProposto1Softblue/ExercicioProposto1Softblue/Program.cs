using System;
namespace ExercicioProposto1Softblue
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Vector v1 = new Vector(2, 3);
            Vector v2 = new Vector(4, 5);
            Vector v3 = v1 + v2;
            Vector v4 = v3 * 3;
            Console.WriteLine(v4);
        }
    }

    public struct Vector
    {
        private int x;
        private int y;

        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(
                            x:v1.x + v2.x, 
                            y:v1.y + v2.y);
        }
        
        public static  Vector operator *(Vector v, int m)
        {
            return new Vector(
                                v.x*m, 
                                y:v.y*m);
        }

        public override string ToString()
        {
            return $"({x.ToString()}, {y.ToString()})";
        }
    }
}