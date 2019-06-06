using System;
using System.Threading;

namespace ExercicioProposto2._2SoftBlue
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Clock cronometro = new Clock();
            
            cronometro.Start();
        }
    }

    public delegate void SecondsHandler(long numeroEmSegundos);
    
    public class Clock
    {
        private SecondsHandler callback = OnSecondRegressive;
        public void Start()
        {
            while (true)
            {
                callback(DateTime.Now.Second);                
                Thread.Sleep(1000);
            }
        }

        public static void OnSecond(long segundos)
        {
            Console.WriteLine(segundos);
        }
        
        public static void OnSecondRegressive(long segundos)
        {
            Console.WriteLine(60-segundos);
        }
    }
}