using System;

namespace Tabuleiro
{    
    public class Tabuleiro
    {
        
        public int Linhas { get; }
        public int Colunas { get;}

        private readonly Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            _pecas = new Peca[linhas,colunas];
            Linhas = linhas;
            Colunas = colunas;
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            return _pecas[pos.linha, pos.coluna];
        }

        public bool TestarPosicao(Posicao pos)
        {
            //Teste 3
            Func<Posicao, bool> posicaoInvalida = posicao3 => !(posicao3.linha    <  0      || 
                                                                  posicao3.linha  >= Linhas || 
                                                                  posicao3.coluna <  0      || 
                                                                  posicao3.coluna >= Colunas);
            
            //Teste 2
            Action<Posicao> validarPosicao = posicao2 =>
                {
                    if (posicaoInvalida(posicao2))
                    {
                        throw new TabuleiroException("Posição inválida!");
                    }
                };
            
            //Teste 1
            Func<Posicao, bool> existePeca = posicao1 =>
                {
                    validarPosicao(posicao1);
                    return Peca(posicao1) != null;
                };

            return existePeca(pos);
        }
        
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (TestarPosicao(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            
            _pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca retirarPeca(Posicao pos)
        {
            Peca aux = null;
            if (Peca(pos)!=null)
            {
                aux = Peca(pos);
                aux.posicao = null;
                _pecas[pos.linha, pos.coluna] = null;
            }
            return aux;
        }
    }
}