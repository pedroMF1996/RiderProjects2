using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    public sealed class Torre:Peca
    {
        
        public Torre(Cor cor, Tabuleiro.Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString() 
        {
            return "T";
        }
    
        [Description("Função responsável por retornar se a peça pode ou não se mover")]
        private bool podeMover(Posicao pos) 
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }

        [Description("Função herdada da classe pai Peça, responsável por retornar os movimentos possiveis da subclasse Torre")]
        public override bool[,] movimentosPossiveis() 
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.linha = pos.linha - 1;
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
                pos.linha = pos.linha + 1;
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
                pos.coluna = pos.coluna + 1;
            }

            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (TestesTabuleiro.PosicaoValida(pos,tab) && podeMover(pos)) 
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor) 
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }

            return mat;
        }
    }
}