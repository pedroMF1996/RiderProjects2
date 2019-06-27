using System;
using System.Security.Cryptography;
using System.Text;
using static TreinarLogica2.Test.TestClass;

namespace TreinarLogica2.Model
{
    public class RsaClass
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;
        private RSACryptoServiceProvider _rsa;

        public RsaClass()
        {
            _rsa = new RSACryptoServiceProvider(2048);
            _privateKey = _rsa.ExportParameters(true);
            _publicKey = _rsa.ExportParameters(false);
        }

        //discarte do provedor
        public void Dispose() => _rsa.Dispose();

        //converte a string, que está em utf8, de recebimento em um array de bytes
        private static byte[] TextUtf8Bytes(string msg) =>
            Encoding.UTF8.GetBytes(VerificaString(msg, "A string, utf8, aqui é nulla"));
            
        //converte a string, que está em base64, de recebimento em um array de bytes
        private static byte[] TextBase64Bytes(string msg) =>
            Convert.FromBase64String(VerificaString(msg, "A string, base64, aqui é nulla"));

        //converte o array de bytes de recebimento em uma string de base64
        private static string TextStringBase64(byte[] msg) =>
            Convert.ToBase64String(VerificaByteArray(msg, "O array de bytes aqui é nullo"));

        //converte o array de bytes de recebimento em uma string utf8
        public string TextStringUtf8(byte[] msg) =>
            Encoding.UTF8.GetString(VerificaByteArray(msg, "O array de bytes aqui é nullo"));

        public string Encrypt(string msg)
        {
            //converte a string de recebimento em um array de bytes
            byte[] textBytes = TextUtf8Bytes(msg);

            //criptografa a string
            _rsa.ImportParameters(_publicKey);
            byte[] result = _rsa.Encrypt(textBytes, false);

            //retorna a string ja criptografada
            return TextStringBase64(result);
        }

        public string Decrypt(string msg)
        {
            //converte a string de recebimento em um array de bytes
            byte[] textBytes = TextBase64Bytes(msg);

            //descriptografa a string
            _rsa.ImportParameters(_privateKey);
            byte[] result = _rsa.Decrypt(textBytes, false);

            //retorna a string ja descriptografada
            return TextStringUtf8(result);
        }
    }
}
