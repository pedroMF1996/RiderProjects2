using System;

namespace RenovaveisVeiculos.ExcecaoPersonalizada
{
    public class TelaException:ApplicationException
    {
        public TelaException(string message) : base(message)
        {
        }
    }
}