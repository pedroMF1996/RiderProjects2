using System;
using System.Security.Cryptography;

namespace RSAlib
{
    public class RSA
    {
        
        public Tuple<RSAParameters,RSAParameters> GerarChaves(RSAParameters chavePublica, RSAParameters chavePrivada)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                chavePublica = rsa.ExportParameters(false);//public virtual byte[] ExportRSAPrivateKey ();
                chavePrivada = rsa.ExportParameters(true);
            }
            Tuple<RSAParameters,RSAParameters> chaves = new Tuple<RSAParameters, RSAParameters>(chavePublica,chavePrivada);
            
            return chaves;
        }

        public byte[] Criptografar(byte[] fraseEmBytes,RSAParameters chavePublica)
        {
            byte[] criptografar;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(chavePublica);
                criptografar = rsa.Encrypt(fraseEmBytes, true);
            }
            
            return criptografar;
        }

        public byte[] Descriptografar(byte[] fraseEmBytes,RSAParameters chavePrivada)
        {
            byte[] descriptografar;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(chavePrivada);
                descriptografar = rsa.Decrypt(fraseEmBytes, true);
            }

            return descriptografar;    
        }
    }
}