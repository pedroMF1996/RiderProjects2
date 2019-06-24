using System;
using static RenovaveisVeiculos.Tela.Tela;

namespace RenovaveisVeiculos
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                MostrarMenu();
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