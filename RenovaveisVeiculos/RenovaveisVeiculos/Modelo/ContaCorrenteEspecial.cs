using System;
using System.Globalization;

namespace RenovaveisVeiculos.Modelo
{
    public sealed class ContaCorrenteEspecial : ContaCorrente
    {
        public double Limite { get; set; }
        
        public ContaCorrenteEspecial(string titular, double saldo, double limite) : base(titular, saldo)
        {
            Limite = limite;
        }

        public ContaCorrenteEspecial()
        {
        }


        public void SaqueContaEspecial(double value)
        {
            if (value <= Saldo+Limite)
            {
                Saldo -= value;
            }
            else
            {
                throw new ApplicationException("Impossivel fazer o saque"); 
            }
        }

        public override string Extrato()
        {
            return base.Extrato() + $" {Limite.ToString("f2",CultureInfo.InvariantCulture)}";
        }
    }
}