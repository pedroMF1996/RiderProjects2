using System;
using System.IO;
using RenovaveisVeiculos.ExcecaoPersonalizada;
using static RenovaveisVeiculos.Tela.Tela;

namespace RenovaveisVeiculos
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            checked
            {
                Console.Clear();
                try
                {
                    MostrarMenu();
                }
                catch (IOException e)
                {
                    Console.WriteLine($"{e.Message}\n{e.StackTrace}");   
                }
                catch (ApplicationException e)
                {
                    Console.WriteLine($"{e.Message}\n{e.StackTrace}");
                }
                catch (SystemException exception)
                {
                    Console.WriteLine($"{exception.Message}\n {exception.StackTrace}");
                }
                
            }
        }
    }
}