using System;
using System.Runtime.Serialization;

namespace Tabuleiro
{
    public class TabuleiroException:ApplicationException
    {
        private string msg;

        public TabuleiroException(string message) : base(message)
        {
        }
    }
}