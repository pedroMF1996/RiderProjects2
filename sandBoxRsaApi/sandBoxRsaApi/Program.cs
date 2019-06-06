using System;
using RSAlib.Biblioteca;
using RSA.Domain;

namespace sandBoxRsaApi
{
    internal class Program
    {
        private static string ChaveToUser(Chave key)
        {
            string result = "Chave: ";
            result += $"Exponent:{key.Exponent.ToString()} /";
            result += $"Modulus:{key.Modulus.ToString()} /";
            result += $"P:{key.P.ToString()} /";
            result += $"Q:{key.Q.ToString()} /";
            result += $"DP:{key.DP.ToString()} /";
            result += $"DQ:{key.DQ.ToString()} /";
            result += $"InverseQ:{key.InverseQ.ToString()} /";
            return result;
        }
        
        public static void Main(string[] args)
        {

            Chave key = RSACLASSLIB.GerarChave();
            string[] partesDaChave = ChaveTo

        }
    }
}