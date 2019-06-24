using System;
using System.Globalization;

namespace RenovaveisVeiculos.Modelo
{
    public class ContaCorrente
    {
        public string Titular { get; set; }
        public double Saldo { get; set; }

        public ContaCorrente(string titular, double saldo)
        {
            Titular = titular;
            Saldo = saldo;
        }

        public ContaCorrente()
        {
            
        }
        //Desposito
        public void Deposito(double value)
        {
            Saldo += value;
        }
        
        //Saque
        public void Saque(double value)
        {
            if (Saldo<=0.00)
            {
                throw new ApplicationException("A conta em questão está sem saldo");
            }
            else
            {
                Saldo += value;
            }
        }

        public virtual string Extrato()
        {
            return $@"\{Titular} {Saldo.ToString("f2",CultureInfo.InvariantCulture)}";
        }
    }
}