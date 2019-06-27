using System;
using TreinarLogica2.Model;
namespace TreinarLogica2
{
    internal class Program
    {
        private static RsaClass _rsa;   
        public static void Main(string[] args)
        {

            try
            {
                _rsa = new RsaClass();

                Console.Write("Escreva a frase a ser criptografada: ");
                var mensagem = Console.ReadLine();
                
                string mensagemCriptografada = _rsa.Encrypt(mensagem);
                Console.WriteLine($"\nmensagemCriptografada: \n{mensagemCriptografada}\n\n");


                string mensagemDescriptografada = _rsa.Decrypt(mensagemCriptografada);
                Console.WriteLine($"mensagemDescriptografada: \n\t{mensagemDescriptografada}");
                
                _ = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }  
            finally
            {
                _rsa.Dispose();
            }
        }
    }
}