using System;
using System.Threading;

namespace ExercicioProposto2._3SoftBlue
{
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            Clock cronometro = new Clock();    
            cronometro.AddCallback(OnSecondRegressive);
            
            cronometro.Start();
        }
        
        public static void OnSecond(object sender, ClockEventArgs args)
        {
            Console.WriteLine(args.Second);
        }

        public static void OnSecondRegressive(object sender, ClockEventArgs args)
        {
            Console.WriteLine(60-args.Second);
        }
    }

    public delegate void SecondsHandler(object sender, ClockEventArgs args);

    public class Clock
    {
        private SecondsHandler callback;
        public void Start()
        {
            while (true)
            {
                callback?.Invoke(this,new ClockEventArgs(DateTime.Now.Second));       
                Thread.Sleep(1000);
            }
        }

        public void AddCallback(SecondsHandler handler)
        {
            callback += handler;
        }

    }

    public class ClockEventArgs:EventArgs
    {
        public long Second { get; private set; }

        public ClockEventArgs(long second)
        {
            Second = second;
        }
    }
}