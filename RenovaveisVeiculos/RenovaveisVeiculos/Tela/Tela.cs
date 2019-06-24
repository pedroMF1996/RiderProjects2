using System;
using RenovaveisVeiculos.Controle;
using RenovaveisVeiculos.Modelo;

namespace RenovaveisVeiculos.Tela
{
    public static class Tela
    {
        private static Registrar _registrar;
        
        public static void MostrarMenu()
        {
            int opt = 0;
            while (opt!=6)
            {
                Menu();
                switch (opt)
                {
                    case 0:
                        RegistrarCaminho();
                        break;
                    case 1:
                        InsertMode();
                        break;
                    case 2:
                        ExcludeMode();
                        break;
                    case 3:
                        ListMode();
                        break;
                    case 4:
                        FilterMode();
                        break;
                    case 5:
                        UpdateMode();
                        break;
                }
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
                Console.WriteLine(apException);
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
                Console.WriteLine("Inserção de elementos de transação: \n\n");
                var transacao = Insert.Transacao();
                _registrar.Insert(transacao);
            }
            catch (ApplicationException apException)
            {
                Console.WriteLine(apException);
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
                Console.WriteLine(apException);
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
                Console.WriteLine(apException);
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
                Console.WriteLine(apException);
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
                Console.WriteLine(apException);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}