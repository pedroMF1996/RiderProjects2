using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TrabalhoFinalProgramacaoLinear
{
    internal class Program
    {
        static List<int> NumeroDeEntregasParaCadaCidade = new List<int>();
        static int[,] MatrizDaDistanciasEntreAsCidades = new int[20,20];
        static IEnumerable<int> TodasAsCidades = Enumerable.Range(0, 20);
        static List<int> PossiveisCentros = new List<int>{0,2,3,10,15,17,19};
        
        public static void Main(string[] args)
        {
            string s;
            int i = 0;
            int j = 0;
            bool primeiraLinha = true;
            
            
            using (StreamReader sr = new StreamReader(@"..\..\..\TrabalhoFinal.txt"))
            {
                while (!sr.EndOfStream)
                {
                    if (primeiraLinha)
                    {
                        s = sr.ReadLine();
                        var str = s.Split(' ');
                        
                        foreach (var item in str)
                        {
                            int qtd = Int32.Parse(item);
                            NumeroDeEntregasParaCadaCidade.Add(qtd);
                        }

                        primeiraLinha = false;
                    }
                    else
                    {
                        s = sr.ReadLine();
                        string[] linhas = s?.Split(' ');

                        if (linhas != null)
                            foreach (string item in linhas)
                            {
                                int qtd = Int32.Parse(item);
                                MatrizDaDistanciasEntreAsCidades[i, j] = qtd;
                                j++;
                            }

                        j = 0;
                        i++;
                    }
                }
                
                //tentei deixar o mais simples e o mais representativo possivel
                for (int k = 0; k < 20; k++)//linha
                {
                    for (int l = 0; l < 20; l++)//coluna
                    {
                        Console.Write($"{MatrizDaDistanciasEntreAsCidades[k, l].ToString()} ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
