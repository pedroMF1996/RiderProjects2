namespace Tabuleiro
{
    public static class TestesTabuleiro
    {
        public static bool PosicaoValida(Posicao pos, Tabuleiro tab)
        {
            return !(pos.linha <  0 || pos.linha  >= tab.Linhas || pos.coluna <  0 || pos.coluna >= tab.Colunas);
        }

        public static void ValidarPosicao(Posicao pos, Tabuleiro tab)
        {
            if (!PosicaoValida(pos,tab))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

        public static bool ExistePeca(Posicao pos, Tabuleiro tab)
        {
            ValidarPosicao(pos,tab);
            return tab.Peca(pos) != null;
        }
    }
}