using System;
using Tabuleiro;
using XadrezLib;

namespace CLIRefatoracaoDoJogoDeXadrez.InterfaceGrafica
{
    public class Jogo
    {
        public static void Init()
        {
            try 
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada) 
                {

                    try 
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        var origem = Tela.LerPosicaoXadrez().ToPosicao();
                        TestesPartida.ValidarPosicaoDeOrigem(origem,partida.Tabuleiro,partida.JogadorAtual);

                        bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        var destino = Tela.LerPosicaoXadrez().ToPosicao();
                        TestesPartida.ValidarPosicaoDeDestino(origem, destino, partida.Tabuleiro);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e) 
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(partida);
            }
            catch (TabuleiroException e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}