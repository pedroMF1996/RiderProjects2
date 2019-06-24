using System;
using System.Globalization;

namespace RenovaveisVeiculos.Modelo
{
    public class Veiculo
    {
        public int Id{ get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public Cor Cor { get; set; }
        public DateTime AnoDeFabricacao { get; set; }
        public Combustivel Combustivel { get; set; }

        public Veiculo(int id, string marca, string modelo, Cor cor, DateTime anoDeFabricacao, Combustivel combustivel)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Cor = cor;
            AnoDeFabricacao = anoDeFabricacao;
            Combustivel = combustivel;
        }

        public override string ToString()
        {
            return $@"\{Id.ToString()} {Marca} {Modelo} {Cor.ToString()} {AnoDeFabricacao.ToString(CultureInfo.InvariantCulture)} {Combustivel.ToString()}    ";
        }
    }

    
    public enum Cor
    {
        Preto,
        Branco,
        Amarelo,
        Vermelho,
        Cinza
    }

    public enum Combustivel
    {
        Gasolina,
        Alcool,
        Óleo_Disel
    } 
}