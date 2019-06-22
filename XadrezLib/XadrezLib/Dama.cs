using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    public sealed class Dama:Peca
    {
        public Dama(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString() 
        {
            return "D";
        }
        [Description("Função responsável por retornar se a peça pode ou não se mover")]
        private bool podeMover(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }

        [Description("Função herdada da classe pai Peça, responsável por retornar os movimentos possiveis da subclasse Dama")]
        public override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha, pos.coluna - 1);
            }

            // direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha, pos.coluna + 1);
            }

            // acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna);
            }

            // abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna);
            }

            // NO
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna - 1);
            }

            // NE
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }

            // SE
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna + 1);
            }

            // SO
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna - 1);
            }

            return mat;
        }
    }
}