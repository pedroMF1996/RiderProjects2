using System;

namespace RenovaveisVeiculos.ExcecaoPersonalizada
{
    public class ControllerException:ApplicationException
    {
        public ControllerException(string message) : base(message)
        {
        }
    }
}