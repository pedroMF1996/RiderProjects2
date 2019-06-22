using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace trabalhoProgLin
{
    class Program
    {
        static void Main(string[] args)
        {
            var mat = new List<int>();
            
            var pc1 = new List<int>();
            var pc3 = new List<int>();
            var pc4 = new List<int>();
            var pc11 = new List<int>();
            var pc16 = new List<int>();
            var pc18 = new List<int>();
            var pc20 = new List<int>();
            
            
            try
            {
                checked
                {
                    using (var sr = new StreamReader(@"C:\Users\PEDROMARTINSFALLEIRO\Downloads\dados.txt"))
                    {
                        List<int> i = new List<int>();
                        while (!sr.EndOfStream)
                        {
                            string s = sr.ReadLine();
                            foreach (var v in s.Split(' '))
                            {
                                i.Add(int.Parse(v));
                            }
                            mat.AddRange(i);
                            i.Clear();
                        }
                    }

                    int cont = 0;
                    foreach (var i in mat)
                    {
                        cont = cont >= 20 ? 0 : cont;
                        Console.Write(cont%20==0 ? $"\n {i.ToString()} ":$" {i.ToString()} ");
                        cont++;
                    }

                    var q = mat.Count;
                    int j = 0;
                    for (int i = 0; i < q; i++)
                    {
                        j = j % 20 == 0 ? 0 : j;
                        var x = j == 0? mat[i] * 4 : j == 1 ? mat[i] * 2 : j == 2 ? mat[i] * 7 : j == 3 ? mat[i] * 6 : j == 4 ? mat[i] * 4 : j == 5 ? mat[i] * 7 : j == 6 ? mat[i] * 4 : j == 7 ? mat[i] * 10 : j == 8 ? mat[i] * 4 : j == 9 ? mat[i] * 5 : j == 10 ? mat[i] * 9 : j == 11 ? mat[i] * 8 : j == 12 ? mat[i] * 2 : j == 13 ? mat[i] * 4 : j == 14 ? mat[i] * 7 : j == 15 ? mat[i] * 8 : j == 16 ? mat[i] * 3 : j == 17 ? mat[i] * 6 : j == 18 ? mat[i] * 5 : mat[i] * 9;
                        
                        if (i>= 20 && i<40)//caso 1
                        {
                            pc1.Add(x);
                        }
                        else 
                        if (i>= 60 && i<80)//caso 3
                        {
                            pc3.Add(x);
                        }
                        else 
                        if (i>= 80 && i<100)//caso 4
                        {
                            pc4.Add(x);
                        }
                        else 
                        if (i>= 220 && i<240)//caso 11
                        {
                            pc11.Add(x);
                        }
                        else 
                        if (i>= 320 && i<340)//caso 16
                        {
                            pc16.Add(x);
                        }
                        else 
                        if (i>= 360 && i<380)//caso 18
                        {
                            pc18.Add(x);
                        }
                        else 
                        if (i>= 400 && i<420)//caso 20
                        {
                            pc20.Add(x);
                        }

                        j++;
                    }

                    Action c1 = () =>
                    {
                        Console.WriteLine("\n\n Caso 1:");
                        foreach (var i in pc1)
                        {
                            Console.Write($"{i.ToString()} ");
                        }
                    };
                    c1();
                    
                    Action c3 = () =>
                    {
                        Console.WriteLine("\n\n Caso 3:");
                        foreach (var i in pc3)
                        {
                            Console.Write($"{i.ToString()} ");
                        }
                    };
                    c3();

                    Action c4 = () =>
                    {
                        Console.WriteLine("\n\n Caso 4:");
                        foreach (var i in pc4)
                        {
                            Console.Write($"{i.ToString()} ");
                        }
                    };
                    c4();
                    
                    Console.WriteLine("\n\n Caso 11:");
                    foreach (var i in pc11)
                    {
                        Console.Write($"{i.ToString()} ");
                    }

                    Action c16 = () =>
                    {
                        Console.WriteLine("\n\n Caso 16:");
                        foreach (var i in pc16)
                        {
                            Console.Write($"{i.ToString()} ");
                        }
                    };
                    c16();
                    
                    Console.WriteLine("\n\n Caso 18:");
                    foreach (var i in pc18)
                    {
                        Console.Write($"{i.ToString()} ");
                    }

                    Action c20 = () =>
                    {
                        Console.WriteLine("\n\n Caso 20:");
                        foreach (var i in pc20)
                        {
                            Console.Write($"{i.ToString()} ");
                        }
                    };
                    Console.WriteLine("\n\n");


                    var maiorC1 = pc1.Max();
                    var indcMaiorC1 = pc1.FindIndex(x=>x==maiorC1);
                    Console.WriteLine($"caso 1 : value: {maiorC1.ToString()} - indice: {indcMaiorC1.ToString()}");
                    
                    
                    var maiorC3 = pc3.Max();
                    var indcMaiorC3 = pc3.FindIndex(x=>x==maiorC3);
                    Console.WriteLine($"caso 3 : value: {maiorC3.ToString()} - indice: {indcMaiorC3.ToString()}");
                    
                    
                    
                    var maiorC4 = pc4.Max();
                    var indcMaiorC4 = pc4.FindIndex(x=>x==maiorC4);
                    Console.WriteLine($"caso 4 : value: {maiorC4.ToString()} - indice: {indcMaiorC4.ToString()}");
                    
                    
                    var maiorC11 = pc11.Max();
                    var indcMaiorC11 = pc11.FindIndex(x=>x==maiorC11);
                    Console.WriteLine($"caso 11 : value: {maiorC11.ToString()} - indice: {indcMaiorC11.ToString()}");
                        
                    var maiorC16 = pc16.Max();
                    var indcMaiorC16 = pc16.FindIndex(x=>x==maiorC16);
                    Console.WriteLine($"caso 16 : value: {maiorC16.ToString()} - indice: {indcMaiorC16.ToString()}");
                    
                    var maiorC18 = pc18.Max();
                    var indcMaiorC18 = pc18.FindIndex(x=>x==maiorC18);
                    Console.WriteLine($"caso 18 : value: {maiorC18.ToString()} - indice: {indcMaiorC18.ToString()}");
                    
                    var maiorC20 = pc20.Max();
                    var indcMaiorC20 = pc20.FindIndex(x=>x==maiorC20);
                    Console.WriteLine($"caso 20 : value: {maiorC20.ToString()} - indice: {indcMaiorC20.ToString()}\n\n");
                    //tenho que de alguma forma montar uma forma dinamica de comparar todos os valores de cada lista
                    //e retornar qual tme os menores valores


                    Console.WriteLine("cidades centro de distribuição: 4, 16, 20");
                    c4();
                    c16();
                    c20();

                    List<int> clientesDaC4 = new List<int>();
                    List<int> clientesDaC16 = new List<int>();
                    List<int> clientesDaC20 = new List<int>();
                    
                    for (int i = 0; i < 20; i++)
                    {
                        int x1 = pc1[i], 
                            x2 = pc3[i], 
                            x3 = pc4[i];
                        
                        var tc1 = clientesDaC4.Sum();
                        var tc3 = clientesDaC16.Sum();
                        var tc4 = clientesDaC20.Sum();
                        
                        if ((x1 < x2 || x1 < x3) && (tc1 <= tc3 || tc1 <= tc4))
                        {
                            if (x1 < x2 && tc1 <= tc3)
                            {
                                clientesDaC4.Add(i+1);
                            }
                            else if (x1 < x3 && tc1 <= tc4)
                            {
                                clientesDaC4.Add(i+1);
                            }
                        }
                        else if (x2 <= x3 || tc3 <= tc4) 
                        {
                            clientesDaC16.Add(i+1);
                        }
                        else
                        {
                            clientesDaC20.Add(i+1);
                        }
                    }

                    Console.WriteLine("\n\nClientes da c4: ");
                    foreach (int i in clientesDaC4)
                    {
                        Console.Write($" {i.ToString()} ");
                    }

                    Console.WriteLine("\n\nClientes da c16: ");
                    foreach (int i in clientesDaC16)
                    {
                        Console.Write($" {i.ToString()} ");
                    }

                    Console.WriteLine("\n\nClientes da c20: ");
                    foreach (int i in clientesDaC20)
                    {
                        Console.Write($" {i.ToString()} ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }
    }
}