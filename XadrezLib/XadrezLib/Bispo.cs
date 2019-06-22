using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    public sealed class Bispo:Peca
    {
        public Bispo(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString() 
        {
            return "B";
        }
        
        [Description("Função responsável por retornar se a peça pode ou não se mover")]
        private bool podeMover(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }
        
        [Description("Função herdada da classe pai Peça, responsável por retornar os movimentos possiveis da subclasse Bispo")]
        public override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // NO
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab)  && podeMover(pos)) 
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