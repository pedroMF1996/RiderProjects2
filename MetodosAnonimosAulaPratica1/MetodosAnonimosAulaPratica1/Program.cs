using System;
using System.Threading;

namespace MetodosAnonimosAulaPratica1
{
    internal class Program
    {
        public static void Main()
        {
            NumberGenerator g = new NumberGenerator();
            g.OnGenerated += delegate(object sender, NumberEventArgs args) {
                                Console.WriteLine($"Número gerado: {args.Number}");
                             };
            g.Start();
        }

//        static void g_OnGenerated(object sender, NumberEventArgs args)
//        {
//            Console.WriteLine($"Número gerado: {args.Number}");
//        }
    }

    public delegate void NumberHandler(object sender,NumberEventArgs args);

    public class NumberEventArgs
    {
        public int Number;
    }

    public class NumberGenerator
    {
        public event NumberHandler OnGenerated;
        Random r = new Random();

        public void Start()
        {
            while (true)
            {
                int n = r.Next(100);
                if (OnGenerated != null)
                {
                    NumberEventArgs args = new NumberEventArgs(){Number= n};
                    OnGenerated(this, args);
                }
                Thread.Sleep(1000);    
            }
        }
    }
}