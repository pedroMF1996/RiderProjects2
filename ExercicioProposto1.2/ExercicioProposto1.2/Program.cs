using System;
namespace ExercicioProposto1._2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
//            Vector v1 = new Vector(2, 3);
//            Vector v2 = new Vector(4, 5);
//            Vector v3 = v1 + v2;
//            Vector v4 = v3 * 3;
//            Console.WriteLine(v4);

            Vector v = new Vector();
            v['X'] = 5;
            v['Y'] = 7;

            int x = v['X'];
            int y = v['Y'];

            Console.WriteLine($"{x} - {y}");
        }
    }

    public struct Vector
    {
        private int x;
        private int y;
        
        public int this[char numero]
        {
            get
            {
                switch (numero)
                {
                    case 'X':
                        return x;
                    case 'Y':
                        return y;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (numero)
                {
                    case 'X':
                        x = value;
                        break;
                    case 'Y':
                        y = value;
                        break;
                }
            }
        }

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