using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    [Description("Classe responsável pelos testes que ocorrerão ao longo da partida")]
    public static class TestesPartida
    {   
        [Description("Método responsável por validar a posição de origem " +
                     "verificando antes de que haja uma movimentação de alguma peça")]
        public static void ValidarPosicaoDeOrigem(Posicao pos, Tabuleiro.Tabuleiro tab, Cor jogadorAtual) {
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

        [Description("Método responsável po validar a posição de destino" +
                     "antes que haja a movimentação de alguma peça")]
        public static void ValidarPosicaoDeDestino(Posicao origem, Posicao destino, Tabuleiro.Tabuleiro tab) {
            if (!tab.Peca(origem).movimentosPossiveis(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        
        [Description("Função responsável por verificar se o rei do jogador adversário está em xeque")]
        public static bool EstaEmXeque(Cor cor, List<Peca> pecasAdversariasEmJogo, List<Peca> pecasEmJogo)
        {
            Peca rei = pecasAdversariasEmJogo.Find(x => x is Rei);

            if (rei == null) {
                throw new TabuleiroException((cor == Cor.Branco)?
                    "Não tem rei da cor branco no tabuleiro!"
                    :"Não tem rei da cor preto no tabuleiro!");
            }
            
            foreach (Peca x in pecasEmJogo) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[rei.posicao.linha, rei.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }
        
        [Description("Função que em colaboração à função EstaEmXeque verifica " +
                     "se a situação do rei adversário é de xequemate " +
                     "retornando o possível fim da partida")]
        public static bool TesteXequemate(Cor cor, List<Peca> pecasAdversariasEmJogo, List<Peca> pecasEmJogo,Tabuleiro.Tabuleiro tab, PartidaDeXadrez partida)
        {
            if (!EstaEmXeque(cor, pecasAdversariasEmJogo, pecasEmJogo)) {
                return false;
            }
            foreach (Peca x in pecasAdversariasEmJogo) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i=0; i<tab.Linhas; i++) {
                    for (int j=0; j<tab.Colunas; j++) {
                        if (mat[i, j]) {
                            var origem = x.posicao;
                            var destino = new Posicao(i, j);
                            var pecaCapturada = partida.ExecutaMovimento(origem, destino);
                            var testeXeque = EstaEmXeque(cor, pecasAdversariasEmJogo, pecasEmJogo);
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