using System;

namespace DefinindoIndexadores
{
    class Program
    {
        public static void Main()
        {
            Temperaturas t = new Temperaturas();

            Console.WriteLine(t[2]);
            Console.WriteLine($"mes 12: {t[12]}ºC");
            t[12] = 30;
            Console.WriteLine($"mes 12: {t[12]}ºC");

            Console.WriteLine($"Março: {t["mar"]}ºC");
        }
    }    

    class Temperaturas
    { 
        int[] temperaturas = new int[] {30, 31, 29, 27, 22, 15, 16, 19, 23, 26, 27, 28};

        public int this[int mes]
        {
            get { return temperaturas[mes - 1]; }
            set { temperaturas[mes - 1] = value; }
        }

        public int this[string mes]
        {
            get
            {
                switch (mes)
                {
                    case "jan": return temperaturas[0];
                    case "fev": return temperaturas[1];
                    case "mar": return temperaturas[2];
                    case "abr": return temperaturas[3];
                    case "mai": return temperaturas[4];
                    case "jun": return temperaturas[5];
                    case "jul": return temperaturas[6];
                    case "ago": return temperaturas[7];
                    case "set": return temperaturas[8];
                    case "out": return temperaturas[9];
                    case "nov": return temperaturas[10];
                    case "dez": return temperaturas[11];
                    default: return -1;
                }
            }
        }
    }
}

