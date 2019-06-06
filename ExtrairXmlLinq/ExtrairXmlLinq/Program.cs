using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ExtrairXmlLinq
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            XElement xml = XElement.Load(@"..\..\colaboradores.xml");

            List<Colaborador> q = (from e in xml.Elements("colaborador")
                orderby int.Parse(e.Attribute("codigo").Value)
                select new Colaborador()
                {
                    Codigo = (int)e.Attribute("codigo"),
                    Type = (Tipo)Enum.Parse(typeof(Tipo),e.Attribute("tipo").Value),
                    Nome = e.Element("nome").Value,
                    Idade = int.Parse(e.Element("idade").Value)
                }).ToList();
            

            foreach (Colaborador colaborador in q)
            {
                Console.WriteLine(colaborador);
            }
        }
    }

    enum Tipo
    {
        Funcionario,
        Terceirizado
    }

    class Colaborador
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public Tipo Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}, {2} anos, {3}", Codigo,Nome,Idade,Type);
        }
    }
}