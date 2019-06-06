using System;
using System.Security.Cryptography;

namespace testes
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*string frase = "R S A";
            foreach (char letra in frase)
            {
                int n = letra;
                if (n!=32)
                {
                    Console.Write($" {n.ToString()} ");
                }
                else
                {
                    Console.Write(" _ ");
                }
            }*/

//            int n = 200;
//            char c = Convert.ToChar(n);
//
//            Console.WriteLine(c);

//            for (int i = 0; i < 100; i++)
//            {
//                Console.WriteLine($"{i}: {(35*i).ToString()}");
//            }

//            Console.WriteLine((5832%35).ToString());
            string kPub;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = true;
                var x = rsa.ExportParameters(true);
                kPub = Convert.ToString(x.DP);

            }

            Console.WriteLine(kPub);
        }
    }
}