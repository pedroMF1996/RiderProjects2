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
                MostrarMenu();
            }
        }
    }
}