using System;
using CSharp_easy_RSA_PEM;

namespace testeRSA10
{
    class Program
    {
        static void Main(string[] args)
        {
            string privateKeyStr;
            Console.WriteLine("Hello World!");
            using (var rsa = Crypto.CreateRsaKeys(2048))
            {
                privateKeyStr = Crypto.ExportPrivateKeyToRSAPEM(rsa);
            }

            Console.WriteLine(privateKeyStr);
        }
    }
}