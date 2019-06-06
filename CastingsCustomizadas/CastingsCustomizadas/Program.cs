using System;

namespace CastingsCustomizadas
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LetraAlfabeto la = new LetraAlfabeto('b');

            char c = (char)la;
            Console.WriteLine(c);

            LetraAlfabeto la1 = (LetraAlfabeto) c;
            Console.WriteLine(la1);
            
        }
    }

    class LetraAlfabeto
    {
        private char _caractere;

        public LetraAlfabeto(char caractere)
        {
            _caractere = char.ToUpper(caractere);
        }

//        public static implicit operator char(LetraAlfabeto la)
//        {
//            return la._caractere;
//        }

        public static explicit operator char(LetraAlfabeto la)
        {
            return la._caractere;
        }

//        public static implicit operator LetraAlfabeto(char c)
//        {
//            return new LetraAlfabeto(c);
//        }

        public static explicit operator LetraAlfabeto(char c)
        {
            return new LetraAlfabeto(c);
        }
        
    }
}