using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace rsateste
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //1ª forma
            /*const string chavePublica = "<RSAKeyValue><Modulus>0T+cB1Xbnz3GhopJhmopF0UBStuREv6fQ6OQQVnietwlXIMjSVQLrrRRNK/fPw9dtn" +
                                        "U15641copbWG7CC1ggCxpT8W4evi0iyjQva7hjS8ZCG2a/zABlnbu9t3Oa+FFfpjRB0G2qZDpbpU+PS99rRVtSUj2slcN" +
                                        "RTZWI1qSTaCk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var texto = Encoding.UTF8.GetBytes("usuariosenha");
            
                using (var rsa = new RSACryptoServiceProvider())
                {
                    try
                    {
                        rsa.FromXmlString(chavePublica);
                        var encryptedData = rsa.Encrypt(texto, false);
                        //Base64
                        //var base64Encrypted = Convert.ToBase64String(encryptedData);
                        //Console.WriteLine(base64Encrypted);
                        
                        //Hex
                        var sb = new StringBuilder();
                        foreach (var b in encryptedData)
                            sb.Append(b.ToString("X2"));
                        
                        Console.WriteLine(sb.ToString());
                    }
                    finally
                    {
                        rsa.PersistKeyInCsp = false;
                    }
                }
            
                //2ª forma
                var rsaPublic = new RSACryptoServiceProvider();
            
                rsaPublic.FromXmlString(chavePublica);
                
                var encryptedRsa = rsaPublic.Encrypt(texto, false);
                
         
                
                //Base64
                
                //Console.WriteLine(Convert.ToBase64String(encryptedRSA));
                
        
                
                //Hex
                
                var sb2 = new StringBuilder();

                foreach (var b in encryptedRsa)
                {

                    sb2.Append(b.ToString("X2"));
                }

                Console.WriteLine(sb2.ToString());
                
                Console.ReadKey();*/




//            string chavePublica = Rsa.GerarChavePublica();
//            Console.WriteLine(chavePublica);
//
////            string textoEnciptografado = Rsa.Criptografar("Ola mundo", chavePublica);
////            Console.WriteLine(textoEnciptografado);
//
//            string chavePrivada = Rsa.GerarChavePrivada();
//            Console.WriteLine($"chave privada: {chavePrivada}");


            Chave key = null;
            RSAParameters keyRsa;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                RSAParameters X = rsa.ExportParameters(true);
                key = Rsa.ChaveBitToChave(X);
                keyRsa = Rsa.ChaveChaveToRSAParameter(key);
                Console.WriteLine(Rsa.ChaveBitToChave(keyRsa));
                
            }

            Console.WriteLine(key);



        }

        public class Chave
        {
            public string Exponent { get; set; }
            public string Modulus { get; set; }
            public string P { get; set; }
            public string Q { get; set; }
            public string DP { get; set; }
            public string DQ { get; set; }
            public string InverseQ { get; set; }

            public override string ToString()
            {
                return string.Format("Exponent: {0} \nModulus: {1} \nP: {2} \nQ: {3}\nDP: {4}\nDQ: {5}\nInvert Q: {6}",
                    Exponent,Modulus,P,Q,DP,DQ,InverseQ);
            }
        }

        public class Rsa
        {
            private static string ChaveString(RSAParameters chave)
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(typeof(RSAParameters));
                
                xs.Serialize(sw, chave);

                return sw.ToString();
            }

            public static Chave ChaveBitToChave(RSAParameters chave)
            {
                Chave chaveChave = new Chave();
                chaveChave.Exponent = Encoding.Unicode.GetString(chave.Exponent);
                chaveChave.Modulus = Encoding.Unicode.GetString(chave.Modulus);
                chaveChave.P = Encoding.Unicode.GetString(chave.P);
                chaveChave.Q = Encoding.Unicode.GetString(chave.Q);
                chaveChave.DQ = Encoding.Unicode.GetString(chave.DQ);
                chaveChave.DP = Encoding.Unicode.GetString(chave.DQ);
                chaveChave.InverseQ = Encoding.Unicode.GetString(chave.InverseQ);
                return chaveChave;
            }
            
            public static RSAParameters ChaveChaveToRSAParameter(Chave chave)
            {
                RSAParameters key = new RSAParameters();
                key.Exponent = Encoding.Unicode.GetBytes(chave.Exponent);
                key.Modulus = Encoding.Unicode.GetBytes(chave.Modulus);
                key.P = Encoding.Unicode.GetBytes(chave.P);
                key.Q = Encoding.Unicode.GetBytes(chave.Q);
                key.DQ = Encoding.Unicode.GetBytes(chave.DQ);
                key.DP = Encoding.Unicode.GetBytes(chave.DQ);
                key.InverseQ = Encoding.Unicode.GetBytes(chave.InverseQ);
                return key;
            }
            
            public static string GerarChavePublica()
            {
                RSAParameters chavePublica;

                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;
                    chavePublica = rsa.ExportParameters(false);
                }

                string strChavePublica = ChaveString(chavePublica);

                return strChavePublica;
            }

            public static string GerarChavePrivada()
            {
                RSAParameters chavePrivada;

                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;
                    chavePrivada = rsa.ExportParameters(true);
                }

                string strChavePrivada = ChaveString(chavePrivada);

                return strChavePrivada;
            }

            public static string Criptografar(string texto, Chave chavePublica)
            {
                byte[] criptografar;
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;

                    rsa.ImportParameters(ChaveChaveToRSAParameter(chavePublica));
                    criptografar = rsa.Encrypt(Encoding.UTF8.GetBytes(texto), false);
                }

                return Convert.ToBase64String(criptografar);
            }

            public static string Descriptografar(byte[] fraseEmBytes, Chave chavePrivada)
            {
                byte[] descriptografar;
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.ImportParameters(ChaveChaveToRSAParameter(chavePrivada));
                    descriptografar = rsa.Decrypt(fraseEmBytes, true);
                }

                return Encoding.UTF8.GetString(descriptografar);
            }    
        }
    }
}