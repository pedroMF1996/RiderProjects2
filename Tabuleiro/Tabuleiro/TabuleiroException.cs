using System;
using System.Runtime.Serialization;

namespace Tabuleiro
{
    public sealed class TabuleiroException:ApplicationException
    {
        public TabuleiroException(string message) : base(message)
        {
        }
    }
}