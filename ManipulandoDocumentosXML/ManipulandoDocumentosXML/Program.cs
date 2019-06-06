/*
 * O FORMATO XML
 *     System.Xml.XDocument
 *     Extencible Markup Language
 *     Padronizado pela W3C
 *     É um formato para representação de dados
 *         Hierarquico
 *         Padrão aberto e portavel
 *     Muito usado atualmente
 *         Integrassões de dados entre sistemas
 *         Web services
 *         Arquivos de configuração
 *         Representação dos mais diversos tipos de informações
 * 
 * API DOM
 *     Document Object Model
 *     API especificada pelo W3C
 *     Permite manipular a estrutura do XML
 *     Esta API cria uma arvore em memoria que representa a estrutura do documento XML
 *         é preciso ficar atento quanto ao consumo de memoria
 *     Diversas linguagens de programação suportam o uso de DOM
 *        No c# o dom é suportado atravéz do namespace  system.xml
 *         Atualmente, o LINQ para XML é uma alternativa mais interessante
 * 
 * LINQ para XML
 * Está disponivel atravez do namespace System.XML.Linq
 * É possivel gerar documentos xml de forma mais simples do que usando o DOM
 * A extração de dados do XML pode ser feita atravez do uso de LINQ
 * 
 *     Principais classes
 *         XDocument    DocumentoXML
 *         XElement     Elemento(tag) dentro do XML
 *         XAttribute   Atributo em um elemento
 *         XDeclaration Declaração do inicio do XML
 *         XComment     Comentario
 *         XCData       Seção CData de uma tab
 *         Xnamespace   Namespace
 * 
 *     Criando um documento XML
 *         XDocument doc = new XDocument(
 *             new XElement("linguagens",
 *                 new XElement("linguagem", "C#"),
 *                 new XElement("linguagem", "Java"),
 *                 new XElement("linguagem", "C++")
 *             )
 *         );
 *_______________________________________________________________________________________________________________
 
 *         XDocument doc = new XDocument(
 *             new XElement("linguagens",
 *                 new XElement("linguagem", new XAttribute("ano",2001), "C#"),
 *                 new XElement("linguagens",
     *                 new XElement("linguagem", "Java"),
     *                 new XElement("Author", "James Gosling")
 *                 ),
 *                 new XElement(
     *                 new XElement("linguagem", "C++"),
     *                 new XElement("descricao", new XCData("Linguagem orientada a objetos"))                  
 *                 )
 *             )
 *         );
 *
 * _________________________________________________________________________________________________________________
 *
 *         XDocument doc = new XDocument(
 *             new XDeclaration("1.0","iso-8859-1","yes"),
 *             new XComment("Linguagens de programação"),
 *             new XElement("linguagens",
 *                 new XElement("linguagem", "C#"),
 *                 new XElement("linguagem", "Java"),
 *                 new XElement("linguagem", "C++")
 *             )
 *         );
 * _________________________________________________________________________________________________________________
 *
 *         List<Linguagem> linguagem = new List<Linguagem>
 *             {
 *                 new Linguagem {Nome = "C#",  Ano = 2001},
 *                 new Linguagem {Nome = "Java",Ano = 1995},
 *                 new Linguagem {Nome = "C++", Ano = 1985}
 *             }
 *
 *         XElement doc = new XElement("Linguagens",
 *             from l in linguagens
 *                 select new XElement("linguagem", new XAttribute("ano",l.Ano), new XElement("nome",l.Nome)
 *             )
 *         )
 *         
 *     Lendo e gravando documentos XML
 *         As classes XDocument e XElement possuem os métodos Load(), Parse(), Save()
 *             doc.Save(@"C:\Temp\arquivo1.xml");
 *
 *             XElement e = XElement.Load(@"C:\Temp\arquivo1.xml");
 *
 *             StringWriter w = new StringWriter();
 *             doc.Save(w);
 * 
 *             string s = ...;
 *             XElement e = new XElement.Parse(s);
 * 
 *         Inserir elementos em documentos xml
 *             A insersão é feita através dos métodos Add(), AddFirst() de XElement
 *             Add(): Adiciona no final
 *             AddFirst(): Adiciona no inicio;
 * 
 *         Remover elementos de documentos xml
 *             A remoção é feita através do método Remove() de XElement
 *             Pode ser usado para remover um elemento ou um fragmento completo do XML(arvore)
 *
 *     Acessando Elementos
 *         Linq para XML possui alguns métodos para acessar elementos XML
 *         Definidos em Extensions Methods como extension methods de IEnumerable<T>
 *             Attributes()    Atributos do elemento
 *             Attribute()     Atributo do elemento com determinado nome
 *             Descendants()   Elementos dentro de um elemento(descendentes em qualquer nível)
 *             Elements()      Elementos dentro de um elemento(descendentes diretos)
 *             Element()       Primeiro elemento descendente
 *             Ancestors()     Pais de um elemento
 * 
 *         Os elementos da tabela anterior podem ser usados em expressões linq para extrair dados do xml
 *             var r = from e in doc.Element("linguagem")
 *                     where (int)e.Attribute("ano)>1990
 *                     select new
 *                     {
 *                         Nome = e.Element("nome").Value,
 *                         Ano = e.Attribute("ano").Value
 *                     };
 *         Existem situações onde a geração da árvore do XML feita pelo DOM não é necessaria ou não é viável
 *             Documentos XML muito extensos
 * 
 *         Uma alternativa é usar a classe XMLReader
 *             Considerado um pull parser
 *     
 *         O XML pode ser processado enquanto ele é lido
 *
 *         Não existe a criação da arvore em memória
 *
 *         A navegação no XML não é tão flexível quanto no DOM
 *
 *         A classe faz parte do namespace System.XML
 *             XMLReader reader = XMLReader.Create(@"C:\arquivo1.xml");
 *
 *             while(reader.Read())
 *             {
 *                 if(reader.NodeType == XmlNodeType.Elemet && reader.Name == "linguagem")
 *                 {
 *                     int ano = int.Parse(reader.GetAttribute("ano"));
 *                 }
 *                 else if(reader.NodeType == XmlNodeType.Element && reader.Name == "nome")
 *                 {
 *                     String nome = reader.ReadElementContentAsString();
 *                 }
 *             }
 */

using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace ManipulandoDocumentosXML
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Funcionario f = CriarFuncionario();
            
            XElement xml = new XElement("Funcionario",
                new XElement("Id",f.Id),
                new XElement("Nome",f.Nome),
                new XElement("Telefone",new XAttribute("tipo","residencial"), f.TelefoneResidencial),
                new XElement("Telefone",new XAttribute("tipo","celular"), f.TelefoneCelular),
                new XElement("Endereço", 
                    new XElement("Rua", f.Endereco.Rua),
                    new XElement("Numeo",f.Endereco.Numero),
                    new XElement("Cidade", f.Endereco.Cidade),
                    new XElement("Estado",f.Endereco.Estado))
                );

            Console.WriteLine(xml);
        }

        private static Funcionario CriarFuncionario()
        {
            Funcionario f = new Funcionario
            {
                Id = 1,
                Nome = "José da Silva",
                TelefoneCelular = "3333-5555",
                TelefoneResidencial ="9999-5555" 
            };
            
            Endereco endereco = new Endereco
            {
                Rua="R. dos Limões",
                Numero = 100,
                Cidade = "São Paulo",
                Estado = "SP"
            };
            f.Endereco = endereco;

            return f;
        }
    }

    class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TelefoneResidencial { get; set; }
        public string TelefoneCelular { get; set; }
        public Endereco Endereco { get; set; }
    }

    class Endereco
    {
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}