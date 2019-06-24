using System;

namespace RenovaveisVeiculos
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                
            }
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}