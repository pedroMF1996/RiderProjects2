using Tabuleiro;

namespace XadrezLib
{
    public class Bispo:Peca
    {
        public Bispo(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public sealed override string ToString() 
        {
            return "B";
        }

        private bool podeMover(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }
        
        public sealed override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // NO
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tab.TestarPosicao(pos) && podeMover(pos)) 
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
            while (tab.TestarPosicao(pos) && podeMover(pos)) 
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
            while (tab.TestarPosicao(pos) && podeMover(pos)) 
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
            while (tab.TestarPosicao(pos) && podeMover(pos)) 
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