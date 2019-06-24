using System;
using System.Globalization;

namespace RenovaveisVeiculos.Modelo
{
    public class Transacao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        
        public Cliente Cliente { get; set; }
        public Veiculo Mercadoria { get; set; }
        public int Quantidade { get; set; }

        public Transacao(int id, DateTime data, Cliente cliente, Veiculo mercadoria, int quantidade)
        {
            Id = id;
            Data = data;
            Cliente = cliente;
            Mercadoria = mercadoria;
            Quantidade = quantidade;
        }

        public Transacao()
        {
            
        }

        public double PrecoTotal() => Quantidade * Mercadoria.Valor;

        public override string ToString()
        {
            return $@"{Id.ToString()} {Data.ToString(CultureInfo.InvariantCulture)} {Quantidade.ToString()} {PrecoTotal().ToString("f2",CultureInfo.InvariantCulture)}\" +
                   $"{Cliente}" +
                   $"{Mercadoria}";
        }
    }
}