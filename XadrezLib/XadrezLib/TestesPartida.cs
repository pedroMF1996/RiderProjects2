using System.Collections.Generic;
using System.Linq;
using Tabuleiro;

namespace XadrezLib
{
    public class TestesPartida
    {
        private delegate Peca ExecutaMovimento(Posicao origem, Posicao destino);
        private delegate void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapiturada);
        
        public void ValidarPosicaoDeOrigem(Posicao pos, Tabuleiro.Tabuleiro tab, Cor jogadorAtual) {
            if (tab.Peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.Peca(pos).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.Peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino, Tabuleiro.Tabuleiro tab) {
            if (!tab.Peca(origem).movimentosPossiveis(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        
        public static bool EstaEmXeque(Cor cor, List<Peca> pecasEmJogo, List<Peca> adversaria)
        {
            Peca r = pecasEmJogo.FirstOrDefault(x => x is Rei);
            
            if (r == null) {
                throw new TabuleiroException((cor == Cor.Branco)?"Não tem rei da cor branco no tabuleiro!":"Não tem rei da cor preto no tabuleiro!");
            }
            
            foreach (Peca x in adversaria) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[r.posicao.linha, r.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }
        
        public static bool TesteXequemate(Cor cor, List<Peca> pecasEmJogo,List<Peca> adversaria, Tabuleiro.Tabuleiro tab)
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            
            if (!EstaEmXeque(cor, pecasEmJogo, adversaria)) {
                return false;
            }
            foreach (Peca x in pecasEmJogo) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i=0; i<tab.Linhas; i++) {
                    for (int j=0; j<tab.Colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = partida.ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor, pecasEmJogo, adversaria);
                            partida.DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        
    }
}