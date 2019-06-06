namespace Tabuleiro
{
    public interface Ipeca
    {
        Posicao posicao { get; set; }
        Cor cor { get; set; }
        QuantidadeDeMovimento qteMovimentos { get; set; }
        Tabuleiro tab { get; set; }

        void incrementarQteMovimentos();
        void decrementarQteMovimentos();
        bool existeMovimentosPossiveis();
        bool movimentosPossiveis(Posicao pos);
        
    }
}