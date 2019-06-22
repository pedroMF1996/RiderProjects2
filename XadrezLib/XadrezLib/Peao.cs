using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    public sealed class Peao:Peca
    {
        private PartidaDeXadrez _partidaDeXadrez;
        public Peao(Cor cor, Tabuleiro.Tabuleiro tab, PartidaDeXadrez partidaDeXadrez) : base(cor, tab)
        {
            _partidaDeXadrez = partidaDeXadrez;
        }
        
        public override string ToString() 
        {
            return "P";
        }

        [Description("Função responsável por retornar se há inimigo em uma determinada posição")]
        private bool ExisteInimigo(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p != null && p.cor != cor;
        }

        [Description("Função responsável por retornar se a posição analisada está ou não sendo ocupada por uma peça")]
        private bool Livre(Posicao pos) 
        {
            return tab.Peca(pos) == null;
        }

        [Description("Função herdada da classe pai Peça, responsável por retornar os movimentos possiveis da subclasse Peão")]
        public override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branco) 
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (TestesTabuleiro.PosicaoValida(pos,tab)  && Livre(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha - 1, posicao.coluna);
                if (TestesTabuleiro.PosicaoValida(pos,tab) && Livre(p2) && !TestesTabuleiro.ExistePeca(pos,tab)  && Livre(pos) && qteMovimentos.Movimento == 0) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (TestesTabuleiro.PosicaoValida(pos,tab) && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (TestesTabuleiro.PosicaoValida(pos,tab)  && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.linha == 3) 
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (TestesTabuleiro.PosicaoValida(pos,tab) && ExisteInimigo(esquerda) && tab.Peca(esquerda) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (TestesTabuleiro.PosicaoValida(pos,tab) && ExisteInimigo(direita) && tab.Peca(direita) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }
            }
            else 
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (TestesTabuleiro.PosicaoValida(pos,tab) && Livre(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha + 1, posicao.coluna);
                if (TestesTabuleiro.PosicaoValida(pos,tab) && Livre(p2) && !TestesTabuleiro.ExistePeca(pos,tab) && Livre(pos) && qteMovimentos.Movimento == 0) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (TestesTabuleiro.PosicaoValida(pos,tab) && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (TestesTabuleiro.PosicaoValida(pos,tab) && ExisteInimigo(pos)) 
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.linha == 4) 
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (TestesTabuleiro.PosicaoValida(pos,tab) && ExisteInimigo(esquerda) && tab.Peca(esquerda) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (TestesTabuleiro.PosicaoValida(pos,tab) && ExisteInimigo(direita) && tab.Peca(direita) == _partidaDeXadrez.VulneravelEnPassant) 
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}