using StringUtils = SoftblueLib2.StringsUtils;
using System;

namespace ExemploAssemblyPublicoSoftbluecli
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string s = StringUtils.Capitalize("c#");
            Console.WriteLine(s);
        }
    }
}