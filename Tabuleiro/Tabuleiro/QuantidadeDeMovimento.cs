namespace Tabuleiro
{
    public class QuantidadeDeMovimento
    {
        public int Movimento { get; set; }
        

        public QuantidadeDeMovimento(int movimento)
        {
            Movimento = movimento;
        }

        public static QuantidadeDeMovimento operator ++(QuantidadeDeMovimento num)
        {
            num.Movimento += 1;
            return num;
        }

        public static QuantidadeDeMovimento operator --(QuantidadeDeMovimento num)
        {
            num.Movimento -= 1;
            return num;
        }
    }
}