using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    public sealed class Cavalo:Peca
    {
        public Cavalo(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString() 
        {
            return "C";
        }
        
        [Description("Função responsável por retornar se a peça pode ou não se mover")]
        private bool podeMover(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }

        [Description("Função herdada da classe pai Peça, responsável por retornar os movimentos possiveis da subclasse Cavalo")]
        public override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            pos.definirValores(posicao.linha - 1, posicao.coluna - 2);
            if (TestesTabuleiro.PosicaoValida(pos,tab)  && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha - 2, posicao.coluna + 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab)  && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha - 1, posicao.coluna + 2);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 1, posicao.coluna + 2);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 2, posicao.coluna - 1);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha + 1, posicao.coluna - 2);
            if (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}