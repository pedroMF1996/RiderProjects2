using System;
using System.Collections.Generic;
using System.Threading;
using AutoRun;

namespace CriandoUmFrameWork
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Result> results = AutoRunner.Run();
            foreach (Result result in results)
            {
                Console.WriteLine(result);
                Activator.
            }
        }
    }

    [RunClass]
    public class A
    {
        [RunMethod]
        public static void Execute()
        {
            Console.WriteLine("A.Execute()");
            Thread.Sleep(2000);
        }
    }

    [RunClass]
    public class B
    {
        [RunMethod]
        public static void Start()
        {
            Console.WriteLine("B.Start()");
            Thread.Sleep(1000);
        }
    }
}