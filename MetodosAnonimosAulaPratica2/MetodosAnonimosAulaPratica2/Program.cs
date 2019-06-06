using System;
using System.Collections.Generic;

namespace MetodosAnonimosAulaPratica2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ItemHandler handler = Process;
            
            /*List<int> l = BuildList(1,100, delegate(int n)
                                                                {
                                                                    return n + 1;
                                                                });
            */
            
            //List<int> l = BuildList(1,100, handler);
            
            List<int> l = BuildList(1,100, x =>
            {
                x+=15;
                return x * 2;
            });
            foreach (var item in l)
            {
                Console.WriteLine(item);
            }
        }

        static int Process(int n)
        {
            return n + 1;
        }

        static List<int> BuildList(int start, int end, ItemHandler handler)
        {
            List<int> l = new List<int>();

            l.Add(start);
            int n = handler(start);

            while (n <= end)
            {
                l.Add(n);
                n = handler(n);
            }

            return l;
        }
    }

    delegate int ItemHandler(int n);
}