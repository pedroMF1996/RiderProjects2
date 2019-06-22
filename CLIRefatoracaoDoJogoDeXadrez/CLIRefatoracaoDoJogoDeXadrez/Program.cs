using System;
using CLIRefatoracaoDoJogoDeXadrez.InterfaceGrafica;

namespace CLIRefatoracaoDoJogoDeXadrez
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Jogo.Init();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }
    }
}