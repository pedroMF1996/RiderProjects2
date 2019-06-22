using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    public sealed class Rei:Peca
    {
        private PartidaDeXadrez _partidaDeXadrez;
        public Rei(Cor cor, Tabuleiro.Tabuleiro tab, PartidaDeXadrez partidaDeXadrez) : base(cor, tab)
        {
            _partidaDeXadrez = partidaDeXadrez;
        }
        
        public override string ToString() 
        {
            return "R";
        }
        
        [Description("Função responsável por retornar se a peça pode ou não se mover")]
        private bool PodeMover(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }

        private bool TesteTorreParaRoque(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos.Movimento == 0;
        }
    
        [Description("Função herdada da classe pai Peça, responsável por retornar os movimentos possiveis da subclasse Rei")]
        public override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (TestesTabuleiro.PosicaoValida(pos,tab)  && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // ne
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // se
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // so
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // no
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && PodeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // #jogadaespecial roque
            if (qteMovimentos.Movimento==0 && !_partidaDeXadrez.Xeque) 
            {
                // #jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (TesteTorreParaRoque(posT1)) 
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.Peca(p1)==null && tab.Peca(p2)==null) 
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (TesteTorreParaRoque(posT2)) 
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.Peca(p1) == null && tab.Peca(p2) == null && tab.Peca(p3) == null) 
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            } 


            return mat;
        }
    }
}