using System;
using System.Security.Cryptography;
using System.Text;

namespace TreinarLogica2
{
    internal class Program
    {
        private static RSAParameters _publicKey;
        private static RSAParameters _privateKey;
        private static RSACryptoServiceProvider _rsa;

        private static void Set()
        {
            _rsa = new RSACryptoServiceProvider(2048);   
            _privateKey = _rsa.ExportParameters(true);
            _publicKey = _rsa.ExportParameters(false);
        }
        
        public static void Main(string[] args)
        {
            try
            {
                Set();
                
                var mensagemCriptografada = Encrypt("hello word");
                Console.WriteLine(mensagemCriptografada);
                
                Console.WriteLine();
                Console.WriteLine();
                var mensagemDescriptografada = Decrypt(mensagemCriptografada);
                Console.WriteLine(mensagemDescriptografada);
                
                //Fecha provedor
                _rsa.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        //converte a string, que está em utf8, de recebimento em um array de bytes
        private static byte[] TextUtf8Bytes(string msg) => 
            Encoding.UTF8.GetBytes(msg ?? throw new ApplicationException("A string, utf8, aqui é nulla"));
        
        //converte a string, que está em base64, de recebimento em um array de bytes
        private static byte[] TextBase64Bytes(string msg) => 
            Convert.FromBase64String(msg ?? throw new ApplicationException("A string, base64, aqui é nulla"));
        
        //converte o array de bytes de recebimento em uma string de base64
        private static string TextStringBase64(byte[] msg) => 
            Convert.ToBase64String(msg ?? throw new ApplicationException("O array de bytes aqui é nullo"));

        //converte o array de bytes de recebimento em uma string utf8
        private static string TextStringUtf8(byte[] msg) =>
            Encoding.UTF8.GetString(msg??throw new ApplicationException("O array de bytes aqui é nullo"));
        
        private static string Encrypt(string msg)
        {
            //converte a string de recebimento em um array de bytes
            var textBytes = TextUtf8Bytes(msg);
            
            //criptografa a string
                _rsa.ImportParameters(_publicKey);
                var result = _rsa.Encrypt(textBytes, false);
            
            //retorna a string ja criptografada
            return TextStringBase64(result);
        }

        private static string Decrypt(string msg)
        {
            //converte a string de recebimento em um array de bytes
            var textBytes = TextBase64Bytes(msg);
            
            //descriptografa a string
                _rsa.ImportParameters(_privateKey);
                var result = _rsa.Decrypt(textBytes, false);
            
            //retorna a string ja descriptografada
            return TextStringUtf8(result);
        }
    }
}