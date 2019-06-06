using Tabuleiro;

namespace XadrezLib
{
    public class Peao:Peca
    {
        private PartidaDeXadrez _partidaDeXadrez;
        public Peao(Cor cor, Tabuleiro.Tabuleiro tab, PartidaDeXadrez partidaDeXadrez) : base(cor, tab)
        {
            _partidaDeXadrez = partidaDeXadrez;
        }
        
        public sealed override string ToString() 
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p != null && p.cor != cor;
        }

        private bool Livre(Posicao pos) 
        {
            return tab.Peca(pos) == null;
        }

        public sealed override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branco) 
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (!tab.TestarPosicao(pos) && Livre(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha - 1, posicao.coluna);
                if (!tab.TestarPosicao(p2) && Livre(p2) && !tab.TestarPosicao(pos) && Livre(pos) && qteMovimentos.Movimento == 0) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (!tab.TestarPosicao(pos) && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (!tab.TestarPosicao(pos) && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.linha == 3) 
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (!tab.TestarPosicao(esquerda) && ExisteInimigo(esquerda) && tab.Peca(esquerda) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (!tab.TestarPosicao(direita) && ExisteInimigo(direita) && tab.Peca(direita) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }
            }
            else 
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (!tab.TestarPosicao(pos) && Livre(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha + 1, posicao.coluna);
                if (!tab.TestarPosicao(p2) && Livre(p2) && !tab.TestarPosicao(pos) && Livre(pos) && qteMovimentos.Movimento == 0) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (!tab.TestarPosicao(pos) && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (!tab.TestarPosicao(pos) && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.linha == 4) 
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (!tab.TestarPosicao(esquerda) && ExisteInimigo(esquerda) && tab.Peca(esquerda) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (!tab.TestarPosicao(direita) && ExisteInimigo(direita) && tab.Peca(direita) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}