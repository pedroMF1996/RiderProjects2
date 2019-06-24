namespace RenovaveisVeiculos.Modelo
{
    public class Cliente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public ContaCorrente Conta { get; set; }

        public Cliente(string cpf, string nome, ContaCorrente conta)
        {
            CPF = cpf;
            Nome = nome;
            Conta = conta;
        }

        public Cliente()
        {
            
        }

        public override string ToString()
        {
            return $@"{CPF} {Nome} {Conta}\";
        }
    }
}