using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($@"{double.MaxValue.ToString("f2",CultureInfo.InvariantCulture)}");
        }
    }
}