using System;

namespace Tabuleiro
{    
    public class Tabuleiro
    {
        
        public int Linhas { get; }
        public int Colunas { get;}

        private readonly Ipeca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            _pecas = new Peca[linhas,colunas];
            Linhas = linhas;
            Colunas = colunas;
        }

        public Peca Peca(int linha, int coluna)
        {
            return (Peca) _pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            return (Peca) _pecas[pos.linha, pos.coluna];
        }

        
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (!TestesTabuleiro.PosicaoValida(pos,this))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            
            _pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca retirarPeca(Posicao pos)
        {
            Peca aux = null;
            if (Peca(pos)!=null)
            {
                aux = Peca(pos);
                aux.posicao = null;
                _pecas[pos.linha, pos.coluna] = null;
            }
            return aux;
        }
    }
}