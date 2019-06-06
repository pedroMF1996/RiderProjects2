using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionAttributes
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Nome da classe: ");
            string className = Console.ReadLine();

            Type t = Type.GetType(className);

            PropertyInfo[] props = t.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                Console.WriteLine($"{prop.Name} => {prop.PropertyType}");
            }

            MethodInfo method = t.GetMethod("Init");

            if (method == null)
            {
                Console.WriteLine("Método Init() não existe");
            }
            else
            {
                object obj = Activator.CreateInstance(t);
                method.Invoke(obj,null);
            }
        }
    }

    class MyClass
    {
        public int x { get; set; }

        public void Init()
        {
            Console.WriteLine("Método Init() invocado!");
        }
    }
    
}