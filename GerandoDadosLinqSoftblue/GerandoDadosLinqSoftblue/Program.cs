using System;
using System.Linq;

namespace GerandoDadosLinqSoftblue
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //var c = Enumerable.Range(1, 10);
            //var c = Enumerable.Empty<string>();
            var c = Enumerable.Repeat("B",10);
                
            foreach (var s in c)
            {
                Console.WriteLine(s);
            }
        }
    }
}