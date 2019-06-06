using System;
using System.Collections.Generic;
using System.Linq;

namespace ExercicioProposto2SoftBlue
{
    internal class Program
    {
        public delegate bool Filter(int num);
        
        public static void Main(string[] args)
        {
            Filter greatedThan5 = FilterGreatedThan5;
            
            List<int> n = new List<int>(){0,1,2,3,4,5,6,7,8,9,10};

            List<int> lista = FilterList(n, greatedThan5);

            Console.WriteLine("numeros maiores que 5: ");
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }

            Filter numbersOdd = FilterOdd;
            
            lista.Clear();
            lista = FilterList(n, numbersOdd);
            
            Console.WriteLine("numeros impares: ");
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
            
        }

        public static List<int> FilterList(List<int> nums, Filter filter)
        {
            return nums.Where(x => filter(x)).ToList();
        }

        public static bool FilterGreatedThan5(int num)
        {
            return num > 5;
        }

        public static bool FilterOdd(int num)
        {
            return num % 2 != 0;
        }
    }
}