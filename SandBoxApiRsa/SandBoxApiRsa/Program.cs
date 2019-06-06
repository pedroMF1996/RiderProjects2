using System;
using System.Security.Cryptography;
using RSA.Domain;
using RSAlib.Biblioteca;

namespace SandBoxApiRsa
{
    class Program
    {
        private static string ChaveToUser(Chave key)
        {
            string result = "Chave: ";
            result += $"Exponent:{key.Exponent} /";
            result += $"Modulus:{key.Modulus} /";
            result += $"P:{key.P} /";
            result += $"Q:{key.Q} /";
            result += $"DP:{key.DP} /";
            result += $"DQ:{key.DQ} /";
            result += $"InverseQ:{key.InverseQ}";
            return result;
        }

        public static void Main(string[] args)
        {
            Chave key = RSACLASSLIB.GerarChave();
            string[] partesDasChaves = ChaveToUser(key).Split(' ');
            foreach (string partesDasChave in partesDasChaves)
            {
                Console.WriteLine(partesDasChave);
            }

            Console.WriteLine();
            Console.WriteLine();

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                Console.WriteLine(RSACLASSLIB.ChaveString(rsa.ExportParameters(true)));
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            string text = "Hello word";
            Console.WriteLine($"Texto normal: {text}");
            string textCriptografado = RSACLASSLIB.Criptografar(text, key);
            Console.WriteLine(textCriptografado);
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("A");
            string textDescript = RSACLASSLIB.Descriptografar(text, key);
            Console.WriteLine(textDescript);
            
        }
    }
}