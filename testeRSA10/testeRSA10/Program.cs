using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace testeRSA10
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1;
            
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                s1 = GetKeyString(rsa.ExportParameters(false));
                Console.WriteLine(s1);
                rsa.FromXmlString(FromJsonString(s1));
                Console.WriteLine("hello word");
                Console.WriteLine(rsa.Encrypt(Convert.FromBase64String("hello word"), false));
                
                
            }

            Console.WriteLine(s1);
        }
        public static string GetKeyString(RSAParameters publicKey)
        {
            var sw = new StringWriter();
                sw.Write(JsonConvert.SerializeObject(publicKey));
            return sw.ToString();
        }
        
        public static string FromJsonString(string jsonString)
        {
            var paramsJson = JsonConvert.DeserializeObject<RSAParameters>(jsonString);
            StringWriter sw = new StringWriter();
            XmlSerializer xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, paramsJson);

            return sw.ToString();
        }
    }
}