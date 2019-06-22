using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tabuleiro;

namespace XadrezLib
{
    public class PartidaDeXadrez
    {
        private List<Peca> _pecas;
        private List<Peca> _capituradas;
        public Tabuleiro.Tabuleiro Tabuleiro{ get; set; }
        public int Turno { get; set; }
        public Cor JogadorAtual { get; set; }
        public bool Terminada { get; set; }
        public bool Xeque { get; set; }
        public Peca VulneravelEnPassant { get; set; }
        
        
        public PartidaDeXadrez()
        {
            Tabuleiro=new Tabuleiro.Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            _pecas = new List<Peca>();
            _capituradas = new List<Peca>();
            ColocarPecas();
        }
        
        [Description("Função responsável por retornar as peças em jogo do jogador atual")]
        public List<Peca> PecasEmJogo() => _pecas.Where(x => x.cor == JogadorAtual).Except(PecasCapturadas(JogadorAtual)).ToList(); 

        [Description("Função responsável por retornar as peças capturadas de determinado jogador")]
        public List<Peca> PecasCapturadas(Cor cor) => _capituradas.Where(x=>x.cor == cor).ToList();
        
        
        //Lista das peças adiverçarias
        [Description("Função responsável por retornar as peças adversárias em jogo do jogador atual")]
        public List<Peca> Adversaria() => PecasEmJogo().Except(PecasEmJogo().Where(x=>x.cor!=JogadorAtual).ToList()).ToList();

        
        [Description("Método responsável por colocar novas peças no tabuleiro")]
        public void ColocarNovaPeca(char coluna, int linha, Peca peca) 
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }
        
        [Description("Função que executa a movimentação das peças, analisa e aplica jogadas especiais")]
        public Peca ExecutaMovimento(Posicao origem, Posicao destino) 
        {
            Peca p = Tabuleiro.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = Tabuleiro.retirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
            if (pecaCapturada != null) 
            {
                _capituradas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2) 
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = Tabuleiro.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2) 
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = Tabuleiro.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao) 
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null) 
                {
                    Posicao posP;
                    posP = p.cor == Cor.Branco ?
                        new Posicao(destino.linha + 1, destino.coluna):
                        new Posicao(destino.linha - 1, destino.coluna);
                        
                    pecaCapturada = Tabuleiro.retirarPeca(posP);
                    _capituradas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        [Description("Método responsável por desfazer a jogada apartir de uma " +
                     "posição de origem, uma posição de destino e, " +
                     "se for o caso, a peça capiturada")]
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) 
        {
            Peca p = Tabuleiro.retirarPeca(destino);
            
            p.decrementarQteMovimentos();
            if (pecaCapturada != null) 
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                _capituradas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(p, origem);
            
                // #jogadaespecial roque pequeno
                if (p is Rei && destino.coluna == origem.coluna + 2) 
                {
                    Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                    Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                    Peca T = Tabuleiro.retirarPeca(destinoT);
                    T.decrementarQteMovimentos();
                    Tabuleiro.ColocarPeca(T, origemT);
                }

                // #jogadaespecial roque grande
                if (p is Rei && destino.coluna == origem.coluna - 2) 
                {
                    Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                    Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                    Peca T = Tabuleiro.retirarPeca(destinoT);
                    T.decrementarQteMovimentos();
                    Tabuleiro.ColocarPeca(T, origemT);
                }

                // #jogadaespecial en passant
                if (p is Peao)
                {
                    if (origem.coluna != destino.coluna && pecaCapturada == VulneravelEnPassant)
                    {
                        Peca peao = Tabuleiro.retirarPeca(destino);
                        Posicao posP = p.cor == Cor.Branco ? 
                            new Posicao(3, destino.coluna): 
                            new Posicao(4, destino.coluna);
                        
                        Tabuleiro.ColocarPeca(peao, posP);
                    }
                }
        }
        [Description("Método que executa a mudança de jogador")]
        private void MudarJogador()
        {
            JogadorAtual = JogadorAtual == Cor.Branco ? 
                Cor.Preto :
                Cor.Branco;
        }
        
        [Description("Método responsável por realizar a jogada apartir de " +
                     "uma posição de origem e uma posição de destino" +
                     "acionando o método ExecutaMovimento, " +
                     "verifica de o jogador se colocará em xeque não permitindo tal movimentação," +
                     "promove peças peões e mostra os peões passiveis da jogada en passant." +
                     "Por fim troca o turno")]
        public void RealizaJogada(Posicao origem, Posicao destino) 
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            
            Func<Cor, List<Peca>, List<Peca>, bool> estaEmXeque = TestesPartida.EstaEmXeque;
            Func<Cor, List<Peca>, List<Peca>, Tabuleiro.Tabuleiro, PartidaDeXadrez, bool> testeXequemate = TestesPartida.TesteXequemate;
            
            if (estaEmXeque(JogadorAtual, Adversaria(), PecasEmJogo())) 
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            var peca = Tabuleiro.Peca(destino);

            
            if (peca is Peao) 
            {
                if ((peca.cor == Cor.Branco && destino.linha == 0) || (peca.cor == Cor.Preto && destino.linha == 7)) 
                {
                    peca = Tabuleiro.retirarPeca(destino);
                    _pecas.Remove(peca);
                    Peca dama = new Dama(peca.cor, Tabuleiro);
                    Tabuleiro.ColocarPeca(dama, destino);
                    _pecas.Add(dama);
                }
            }

            Xeque = estaEmXeque(JogadorAtual, Adversaria(), PecasEmJogo());

            if (testeXequemate.Invoke(JogadorAtual, Adversaria(), PecasEmJogo(),Tabuleiro, this)) 
            {
                Terminada = true;
            }
            else 
            {
                Turno++;
                MudarJogador();
            }

            // #jogadaespecial en passant
            VulneravelEnPassant =
                peca is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2) ? 
                    peca : 
                    null;
        }
        
        [Description("Método responsável por colocar as peças no tabuleiro")]
        private void ColocarPecas() {
            ColocarNovaPeca('a', 1, new Torre(Cor.Branco,Tabuleiro));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branco,Tabuleiro));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branco,Tabuleiro));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branco,Tabuleiro));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branco,Tabuleiro));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branco,Tabuleiro));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branco,Tabuleiro));
            
            ColocarNovaPeca('a', 2, new Peao(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branco,Tabuleiro,this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branco,Tabuleiro,this));
            
            ColocarNovaPeca('a', 8, new Torre(Cor.Preto,Tabuleiro));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preto,Tabuleiro));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preto,Tabuleiro));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preto,Tabuleiro));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preto,Tabuleiro));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preto,Tabuleiro));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preto,Tabuleiro));
            
            ColocarNovaPeca('a', 7, new Peao(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preto,Tabuleiro,this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preto,Tabuleiro,this));
        }
    }
}