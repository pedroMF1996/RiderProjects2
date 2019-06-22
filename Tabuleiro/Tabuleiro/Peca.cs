using System;

namespace Tabuleiro
{
    public abstract class Peca:Ipeca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; set; }
        public QuantidadeDeMovimento qteMovimentos { get; set; }
        public Tabuleiro tab { get; set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            posicao = null;
            this.cor = cor;
            qteMovimentos = new QuantidadeDeMovimento(0);
            this.tab = tab;
        }


        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public void decrementarQteMovimentos()
        {
            qteMovimentos--;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            var i = 0;
            var j = 0;
            while (i < tab.Linhas)
            {
                while (j < tab.Colunas)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                    j++;
                }
                j = 0;
                i++;
            }
            return false;
        }

        public bool movimentosPossiveis(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha,pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();
    }
}