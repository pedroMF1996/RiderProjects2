using System;
using RenovaveisVeiculos.Modelo;

namespace RenovaveisVeiculos.Tela
{
    public static class Insert
    {
        public static Transacao Transacao()
        {
            Console.WriteLine("Transação :");
            Console.Write("Id: ");
            var id = int.Parse(RecebeString("O id não pode ser nullo"));
                
            Console.Write("Data: ");
            var data = DateTime.Parse(RecebeString("O campo Data não pode ser nullo"));
                
            Console.Write("Quantidade: ");
            var quantidade = int.Parse(RecebeString("O campo quantidade não pode ser nullo"));
                
            var cliente = Cliente();
            var veiculo = Veiculo();
            
            return new Transacao(id, data, cliente, veiculo, quantidade);
        }

        public static Cliente Cliente()
        {
            Console.WriteLine("Cliente: ");
            Console.Write("CPF: ");
            var cpf = RecebeString("O campo CPF não pode ser nullo");
            
            Console.Write("Nome: ");
            var nome = RecebeString("O campo Nome não pode ser nullo");

            Console.WriteLine("Qualo tipo de conta do cliente? \nComum - C \nEspecial - E");
            var conta = RecebeOpcaoDeConta() == 'N' ?
                Conta(): RecebeOpcaoDeConta() == 'E' ?
                    ContaEspecial() :
                    throw new ApplicationException("Campo preenchido irregularmente");
            
            return new Cliente(cpf, nome, conta);
        }

        private static char RecebeOpcaoDeConta() => Char.ToUpper(Convert.ToChar(
            RecebeString(
                "O campo tipo de conta deve ser escolhido entre C ou E para que haja o andamento regular do programa")));

        public static ContaCorrente Conta()
        {
            Console.WriteLine("Conta corrente: ");
            Console.Write("Titular: ");
            var titular = RecebeString("O campo titular não pode ser nullo");
            
            Console.Write("Saldo: ");
            var saldo = double.Parse(RecebeString("O campo saldo não pode ser nullo"));
            
            return new ContaCorrente(titular, saldo);
        }

        public static ContaCorrenteEspecial ContaEspecial()
        {
            Console.WriteLine("Conta corrente: ");
            
            Console.Write("Titular: ");
            var titular = RecebeString("O campo titular não pode ser nullo");
            
            Console.Write("Saldo: ");
            var saldo = double.Parse(RecebeString("O campo saldo não pode ser nullo"));
            
            Console.Write("Limite: ");
            var limite = double.Parse(RecebeString("O campo Limite não pode ser nullo"));
            
            return new ContaCorrenteEspecial(titular, saldo, limite);
        }

        public static Veiculo Veiculo()
        {
            Console.WriteLine("Veículo: ");
            
            Console.Write("Id: ");
            var id = int.Parse(RecebeString("O id não pode ser nullo"));
            
            Console.Write("Marca: ");
            var marca = RecebeString("Marca não pode ser nulla");
            
            Console.Write("Modelo: ");
            var modelo = RecebeString("Modelo não pode ser nullo");
            
            Console.Write("Cor: ");
            var cor = (Cor) Enum.Parse(typeof(Cor), RecebeString("A cor não pode ser nulla"));

            Console.Write("Ano de fabricação: ");
            var anoDeFabricacao = DateTime.Parse(RecebeString("Data de fabricação não pode ser nulla"));
            
            Console.Write("Combustivel: ");
            var combustivel =
                (Combustivel) Enum.Parse(typeof(Combustivel), RecebeString("O combustível não pode ser nullo"));
            
            Console.Write("Valor: ");
            var valor = double.Parse(RecebeString("O valor não pode ser nullo"));
            
            
            return new Veiculo(id, marca, modelo, cor, anoDeFabricacao, combustivel, valor);
        }
        
        public static string RecebeString(string msgCasoHouverExcecao) => 
            Console.ReadLine()??throw new ApplicationException(msgCasoHouverExcecao);

    }
}