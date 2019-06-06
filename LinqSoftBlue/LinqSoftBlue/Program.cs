/*
 * O LINQ e uma linguagem que permite a extração de dados
 * Similaridades com sql
 * Extração de dados de varias fontes
 *     Objetos(coleções e arrays) <- foco
 *     XML
 *     DataSet
 *     Entities
 * 
 * Conceitos de c# usados pelo Linq
 *    Expressoes Lambda
 *    Extention Methods
 *    Tipos de dados var
 *         variaveis locais em c# podem ser declaradas usando o tipo var
 *         tipo de dado da variavel sera definido de forma implicita pelo compilador
 *         depois que o tipo da variavel é definido ele não pode ser trocado
 *         Uso restrito a variaveis locais
 *         A variavel deve receber um valor no momento da declaração
 *         A primeira atribuição não pode ser null;
 * 
 *    Tipos anonimos
 *         um tipo anonimo é uma classe criada sem nome
 *         o nome é gerado apenas no momento da compilação
 *         bastante uteis nas situações onde você deseja criar classes que existem apenas para encapsular dados
 *             e serão usadas apenas em locais especificos do código
 * 
 *         Definindo tipos anonimos:
 *             A variavel que ira receber o objeto do tipo anonimo deve ser do tipo var
 *             var cachorro = new {Nome = "Tótó", Idade = 5};
 *
 *         Multiplas Instâcias de tipos anonimos
 *             se tipos anonimos forem definidos mais de uma vez usando as mesmas properties,
 *             o compilador gera apenas uma classe
 * 
 *     Para comparar objetos, use o método Equals();
 * 
 * Funcionamento do LINQ
 *     O LINQ trabalha com extrações de dados;
 *     O retorno de uma expressão LINQ é um objeto IEnumerable<T>
 *     Iteração sobre os elementos é feita normalmente com o uso da estrutura foreach 
 *     Para usar o LINQ para objetos, o assembly System.Core.dll deve ser referenciado
 *     Todos os arquivos que usam expressões LINQ devem importar o namespace System.Linq;
 *     
 * Deferred e immediate execution
 *    Uma expressao LINQ só é executada quando a iteração sobre os elementos é realizada
 *         Deferred execution(execução postergada)
 *    É possivel forçar a execução imediata da expressão através de alguns métodos
 *         ToArray(),ToList(),ToDictionary("sempre mapea uma chave para um valor");
 * 
 * A classe System.Linq.Enumerable
 *     Tem papel fundamental na estrutura de funcionamento do LINQ;
 *     ela adiciona uma série de extension methods a interface IEnumerable<T>
 *     Quando uma expressão LINQ é compilada, ela é transformada em chamadas a estes extension methods;
 *     Os métodos como Where(), OrderByDescending(), Select(), etc. são definidos em Enumerable como sendo
 *         extension methods de IEnumerable<T>;
 *
 * Método OfType<T>();
 *     O  LINQ não consegue trabalhar com coleções que não usa generics;
 *     A classe System.Linq.Enumerble define um método chamado OfType<T>()
 *     Este método também pode ser usado para filtrar elementos de determinado tipo
 *
 * Projetando tipos de dados anonimos
 *     LINQ permite a criação de novos tipos de dados para o retorno
 *         Tipos anonimos aplicados ao select
 *     var s = from p in pessoas
 *             where p.sexo == SexoPessoa.Feminino
 *             Select new {Nome = p.Nome, Idade = p.Idade};
 *     foreach(var p in s)
 *     {
*           Console.WriteLine($"{p.Nome} -> {p.Idade}");
 *     }
 *
 * Definir variaveis 
 *    Linq permite a criação de variáveis, a fim de evitar a repetição de código
 *         usando a intrução let
 *    var s =  from p in pessoas
 *             let i = p.Idade;
 *             where i > 20
 *             Select new {Nome = p.Nome, Idade = i};
 * 
 * Operações com conjuntos
 *     Operações de agregação
 *         Operações que agregam elementos da coleção
 *     Só podem ser chamados como métodos
 *         Extension methods de IEnumerable<T>
 *     União, Intersecção, Subtração e concatenação
 *
 *    
 * Agrupamento de dados
 *    Clausula From composta
 *         O uso de from composto é útil quando é preciso acessar mais de um nível de objeto
 *
 *     Agrupamento de dados com base em uma chave
 *         O linq permite o agrupamento de dados com base em uma chave
 *         var s = from p in pessoas
 *                 group p by p.Idade into g
 *                 select new {Idade = g.Key, Num = g.Cout()};
 *
 *     É possivel agrupar outros critérios
 *         var s = from p in pessoas
 *                 group p by p.Idade into g
 *                 select new {Idade = g.key, NumBrq = g.Avg(p=>p.NumIrmaos)};
 * 
 * Operador join
 *     permite combinar múltiplas fontes de dados para extrair irformações
 *     var s = from p in pessoas
 *             join a in alunos on p.Nome equals a.Nome
 *             select new {Nome  = p.Nome,
 *                         Idade = p.Idade,
 *                         Nota  = a.Nota};
 *     O método DefaultIfEmpty() pode ser usado para trazer dados que existem apenas ema das coleções
 *     var s = from p in pessoas
 *             join a in alunos on p.Nome equals a.Nome into pa
 *             from a in pa.DefaultIfEmpty()
 *             select new {
 *                         Nome = p.Nome,
 *                         Idade = p.Idade,
 *                         Nota = a == null ?
 *                                     0 :
 *                                     a.Nota
 *                        };
 * Geração de dados
 *     Gera um intervalo numérico
 *     Apenas números inteiros
 *     var v = Enumerable.Range(1,5);
 *     O método select pode ser usado para modificaro comportamento do intervalo
 *     var v = Enumerable.Range(1,5).Select(x=>x*2);
 *     Empty() - gera uma coleção vazia
 *         útil em situações onde é necessario fornecer como parametro ou retornar uma coleção vazia
 *         var v = Enumerable.Empty<int>();
 *     Repeat() - gera coleção com o mesmo elemento repetidas vezes
 *         var v = Enumerable.Repeat("A", 4);
 *     
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LinqSoftBlue
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Aluno> alunos = CriarAlunos();
            List<Familia> familias = CriarFamilias();

            var q = from a in alunos
                where a.Turma.Serie == 3 && a.Turma.Letra == 'A'
                orderby a.Nota descending
                select a;
            foreach (Aluno aluno in q)
            {
                Console.WriteLine(aluno);
            }

            Console.WriteLine("\n\n");

//            var q2 = from a in alunos
//                where a.AtividadeExtras.Exists(x => x.Nome == "Xadrez") 
//                select a;

            var q2 = from a in alunos
                from e in a.AtividadeExtras
                where e.Nome == "Xadrez"
                select a;


            foreach (Aluno aluno in q2)
            {
                Console.WriteLine(aluno);
            }

            Console.WriteLine("\n\n");

            var q3 = from a in alunos
                from e in a.AtividadeExtras
                group e by e.Nome
                into g
                select new { Atividade = g.Key, Quantidade = g.Count()};
            
            foreach (var x1 in q3)
            {
                Console.WriteLine(x1);
            }

            Console.WriteLine("\n\n");

            var q4 = from a in alunos
                join f in familias on a.Nome equals f.Filho
                select new {Pai = f.Pai, Mae = f.Mae, Filho = a.Nome, Nota = a.Nota };
            
            foreach (var x1 in q4)
            {
                Console.WriteLine(x1);
            }

            Console.WriteLine("\n\n");
            
            var q5 = from a in alunos
                    join f in familias on a.Nome equals f.Filho into af 
                    from f in af.DefaultIfEmpty()
                    select new {Pai = f == null? " - " : f.Pai, Mae = f == null? " - " : f.Mae, Filho = a.Nome, Nota = a.Nota };
            
            foreach (var x1 in q5)
            {
                Console.WriteLine(x1);
            }

            Console.WriteLine("\n\n");

            var q6 = from a in alunos
                where a.Turma.Serie == 3 && a.Turma.Letra == 'A'
                select a.Nota;
            
            Console.WriteLine("Média: " + q6.Average().ToString("f2",CultureInfo.InvariantCulture));
            Console.WriteLine($"Minima: {q6.Min().ToString("f2",CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Maxima: {q6.Max().ToString("f2",CultureInfo.InvariantCulture)}");
            
            
//            foreach (double d in q6)
//            {
//                Console.WriteLine(d.ToString("f2",CultureInfo.InvariantCulture));
//            }



        }

        private static List<Familia> CriarFamilias()
        {
            return new List<Familia>
            {
                new Familia("João","Augusto","Mariana"),
                new Familia("Pedro","José","Bianca"),
                new Familia("Maria","Sérgio","Rita"),
                new Familia("Joana","Joaquim","Rose"),
            };
        }
        
        private static List<Aluno> CriarAlunos()
        {
            Turma t1 = new Turma(2,'B');
            Turma t2 = new Turma(3,'A');
            
            AtividadeExtra a1 = new AtividadeExtra{Nome = "Judô"};
            AtividadeExtra a2 = new AtividadeExtra{Nome = "Balé"};
            AtividadeExtra a3 = new AtividadeExtra{Nome = "Xadrez"};
            
            return new List<Aluno>
            {
                new Aluno(a1){Nome = "João", Nota = 9.5, Turma = t1},
                new Aluno(a1, a3){Nome="Pedro",Nota = 4,Turma = t1},
                new Aluno(a2, a3){Nome = "Maria",Nota = 5,Turma = t2},
                new Aluno(a2){Nome = "Joana",Nota = 6.5,Turma = t2},
                new Aluno(a2){Nome = "Julia",Nota = 7,Turma = t2}
            };
        }
    }

    class Aluno
    {
        public string Nome { get; set; }
        public double Nota { get; set; }
        public Turma Turma { get; set; }
        public List<AtividadeExtra> AtividadeExtras { get; set; }
        
        public Aluno(params AtividadeExtra[] atividadeExtras)
        {
            AtividadeExtras = atividadeExtras.ToList();
        }
        
        public override string ToString()
        {
            string a = " -> ";
            AtividadeExtras.ForEach(atividade => a += atividade + " ");
            a += "\n";
            return $"{Nome} -> {Nota.ToString("f2",CultureInfo.InvariantCulture)}"+a;
        }
    }

    class AtividadeExtra
    {
        public String Nome { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }

    class Turma
    {
        public int Serie { get; set; }
        public char Letra { get; set; }

        public Turma(int serie, char letra)
        {
            Serie = serie;
            Letra = letra;
        }

        public override string ToString()
        {
            return $"{Serie}{Letra}";
        }
    }

    class Familia
    {
        public string Filho { get; set; }
        public string Pai { get; set; }
        public string Mae { get; set; }

        public Familia(string filho, string pai, string mae)
        {
            Filho = filho;
            Pai = pai;
            Mae = mae;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}: {3}");
        }
    }
}