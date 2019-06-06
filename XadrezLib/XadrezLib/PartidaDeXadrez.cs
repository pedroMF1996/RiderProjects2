using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public List<Peca> PecasEmJogo(Cor cor)
        {
            return _pecas.Where(x => x.cor == cor).Except(PecasCapituradas(cor)).ToList();
        }

        public List<Peca> PecasCapituradas(Cor cor)
        {
            return _capituradas.Where(x=>x.cor == cor).ToList();
        }
        
        public void ColocarNovaPeca(char coluna, int linha, Peca peca) 
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }
        
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

        private void MudarJogador()
        {
            JogadorAtual = JogadorAtual == Cor.Branco ? 
                Cor.Preto :
                Cor.Branco;
        }
        
        public void RealizaJogada(Posicao origem, Posicao destino) 
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            
            Func<Cor, List<Peca>, List<Peca>, bool> estaEmXeque = TestesPartida.EstaEmXeque;
            Func<Cor, List<Peca>, List<Peca>, Tabuleiro.Tabuleiro, bool> testeXequemate = TestesPartida.TesteXequemate;
           
            //Lista das peças adiverçarias
            List<Peca> adversaria = PecasEmJogo(JogadorAtual).
                                        Except(PecasEmJogo(JogadorAtual).
                                            Where(x=>x.cor!=JogadorAtual).ToList()).ToList(); 
            
            if (estaEmXeque(JogadorAtual, PecasEmJogo(JogadorAtual), adversaria)) 
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca peca = Tabuleiro.Peca(destino);

            
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

            Xeque = estaEmXeque(JogadorAtual, PecasEmJogo(JogadorAtual), adversaria);

            if (testeXequemate(JogadorAtual, PecasEmJogo(JogadorAtual), adversaria, Tabuleiro)) 
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