﻿using System;
using System.Reflection;

namespace DefinindoAtributes
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                //MessageAttribute attr = (MessageAttribute)type.GetCustomAttribute(typeof(MessageAttribute));
                MessageAttribute attr = type.GetCustomAttribute<MessageAttribute>();
                
                if (attr != null)
                {
                    Console.WriteLine($"Classe {type.FullName}: {attr.Value}");
                }
                
            }
        }
    }

    [Message(Value = "ABC")]
    class A
    {
        
    }
    
    [Message(Value = "DEF")]
    class B
    {
        
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessageAttribute:Attribute
    {
        public string Value { get; set; }
    }
}