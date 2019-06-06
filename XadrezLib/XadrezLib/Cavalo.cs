using Tabuleiro;

namespace XadrezLib
{
    public class Cavalo:Peca
    {
        public Cavalo(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public sealed override string ToString() 
        {
            return "C";
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

            pos.definirValores(posicao.linha - 1, posicao.coluna - 2);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha - 2, posicao.coluna + 1);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha - 1, posicao.coluna + 2);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 1, posicao.coluna + 2);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 2, posicao.coluna - 1);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 1, posicao.coluna - 2);
            if (tab.TestarPosicao(pos) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}