using System;
using System.Globalization;

namespace RenovaveisVeiculos.Modelo
{
    public class Transacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public Cliente Cliente { get; set; }
        public Veiculo Mercadoria { get; set; }

        public Transacao(int id, DateTime data, double valor, Cliente cliente, Veiculo mercadoria)
        {
            Id = id;
            Data = data;
            Valor = valor;
            Cliente = cliente;
            Mercadoria = mercadoria;
        }

        public override string ToString()
        {
            return $@"{Id.ToString()} {Data.ToString(CultureInfo.InvariantCulture)} {Valor.ToString("f2",CultureInfo.InvariantCulture)}\" +
                   $"{Cliente}" +
                   $"{Mercadoria}";
        }
    }
}