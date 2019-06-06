/*
 * Metodos criados para serem chamados em resposta aos eventos dificilmentesão usados em outros contextos
 *     o método existe apenas para tratar aquele evento especifico
 * Uma alternativa para a criação destes metodos são os métodos anonimos
 *     são metodos que não possuem nomes definidos
 * Podem ser usados tanto como events quanto delegates
 *
 * sem uso do metodo anonimo
 *
 * static void main()
 * {
 *     Pessoa p = new Pessoa();
 *     p.Acordou += PessoaAcordou;
 * }
 *
 * static void PessoaAcordou(object sender, AcordouEvetArgs args)
 * {
 *     ...
 * }
 *
 * com uso de metodos anonimos
 *
 * static void main()
 * {
 *     Pessoa p = new Pessoa();
 *     p.Acordou += delegate(object sender, AcordouEvetArgs args) 
 *                     {
 *                         ... método não tem nome e é definido de forma inline
 *                     };
 * }
 *
 * Ao definir um metodo anonimo não é necessario declarar os paramentros se eles não forem usados pelo metodo;
 * static void main()
 * {
 *     Pessoa p = new Pessoa();
 *     p.Acordou += delegate 
 *                     {
 *                         ... método não tem nome e é definido de forma inline
 *                     };
 * }
 *
 * um metodo anonimo pode definir variaveis como qualquer outro metodo;
 *     ele pode acessar variaveis locais definidas pelo metodo que o define(outer method)
 *
 * static void main()
 * {
 *     int diaDoMes = 5;
 *     Pessoa p = new Pessoa();
 *     p.Acordou += delegate(object sender, AcordouEvetArgs args) 
 *                     {
 *                         Console.WriteLine(diaDoMes);
 *                         ... método não tem nome e é definido de forma inline
 *                     };
 * }
 *
 * Expressoes lambda
 *
 * forma de simplificar o uso de metodos anonimos
 * qualquer metodo que recebe um delegate como parametro pode ser chamado usando uma expressão lambda
 *
 *     Parametros => processamento
 *     => : operador lambda
 * 
 */

using System;

namespace MetodosAnonimosExpressoesLambda
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }
}