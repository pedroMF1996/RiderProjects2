using System;

namespace OperadoresCasting
{
    class Program
    {
        static void Main()
        {
            //Curso da softblue de C# avançado - checked, unchecked, casting e sobrecarga de operadores
            //utilização das palavras checked, unchecked
            
            short s1 = 25000;
            short s2 = 20000;
            try
            {
                checked//A operação vai me avisar quando houver uma falha de overflow
                       //para que a operação não me retorne valores errados
                {
                    short s3 = (short)(s1 + s2);
                    Console.WriteLine(s3);
                }

                unchecked//A operação não vai me avisar quando houver uma falha de overflow
                         //e a operação me retornará possivelmente valores errados
                {
                    short s4 = (short)(s1 * s2);
                    Console.WriteLine(s4);
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}