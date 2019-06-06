using System;

namespace MetodosAnonimosAulaPratica3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //closure é a capacidade da expressão lambda de acessar variaveis externas a ela

//            int x = 10;
//
//            Action a = () => Console.WriteLine(x);
//
//            x = 5;
//            
//            a(); 
            
            Action[] actions = new Action[5];
            

            for (int i = 0; i < 5; i++)
            {
                int j = i;
                actions[i] = () => Console.WriteLine(j);
            }

            foreach (Action a in actions)
            {
                a();
            }

            
        }
    }
}