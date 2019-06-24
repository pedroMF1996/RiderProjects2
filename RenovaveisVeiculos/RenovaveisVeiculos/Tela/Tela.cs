using System;
using System.IO;
using RenovaveisVeiculos.Controle;

namespace RenovaveisVeiculos.Tela
{
    public static class Tela
    {
        private static Registrar _registrar;
        
        public static void MostrarMenu()
        {
            try
            {
                int opt = 0;
                while (opt!=6)
                {
                    Console.Clear();    
                    Menu();
                    opt = int.Parse(Insert.RecebeString("Você deve escolher uma opção"));
                    if (opt == 0)
                    {
                        RegistrarCaminho();
                    }
                    else 
                    if (opt == 1)
                    {
                        InsertMode();
                    }
                    else 
                    if (opt == 2)
                    {
                        ExcludeMode();
                    }
                    else 
                    if (opt == 3)
                    {
                        ListMode();
                    }
                    else 
                    if (opt == 4)
                    {
                        FilterMode();
                    }
                    else if (opt == 5)
                    {
                        UpdateMode();
                    }
                }   
            }
            catch (IOException e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");   
            }
            catch (ApplicationException e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
            }
            catch (SystemException exception)
            {
                Console.WriteLine($"{exception.Message}\n {exception.StackTrace}");
            }
        }

        private static void Menu()
        {
            Console.WriteLine("Seja bem vindo a Renováveis Veículos");
            Console.WriteLine("0 - registrar o caminho do arquivo");
            Console.WriteLine("1 - Inserir transacao");
            Console.WriteLine("2 - Excluir transacao");
            Console.WriteLine("3 - Listar transacao");
            Console.WriteLine("4 - Filtrar transação");
            Console.WriteLine("5 - Alterar dados da transação");
            Console.WriteLine("6 - Sair");
        }

        private static void RegistrarCaminho()
        {
            try
            {
                Console.WriteLine("Indique o caminho completo do arquivo de registro: ");
                
                Console.Write("Source path: ");
                var sourcePath = Insert.RecebeString("Caminho do arquivo não pode ser nullo");
                
                _registrar = new Registrar(sourcePath);
            }
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 

        private static void InsertMode()
        {
            try
            {
                Console.WriteLine("Inserção de elementos de transação:\n");
                var transacao = Insert.Transacao();
                _registrar.Insert(transacao);
            }
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        
        private static void ExcludeMode()
        {
            try
            {

            }
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void ListMode()
        {
            try
            {

            }
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void FilterMode()
        {
            try
            {

            }
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void UpdateMode()
        {
            try
            {

            }
            
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}