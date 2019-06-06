/*
 * O que são Assemblies?
 *     Uma aplicação .net é composta por uma sériede assemblies
 *     O assembly é como se fosse uma unidade de compilação
 *     Os assemblies podem ser de dois tipos:
 *         exe: pode ser executado pelo CLR
 *         dll: é feito para ser usado por outros assemblies
 *     O assembly mscorlib.dll é onde estão definidos os tipos principais do c#
 *     Um assemble pode referenciar um ou mais outros assemblies
 *     assembly.exe -> asmbly.dll
 *     assembly.exe -> asmbly.dll -> asmbly.dll -> asmbly.dll
 * 
 * Por que usar assemblies?
 *    Divisão da aplicação em partes menores:
 *         Facilita a manutenção do código
 *         Permite que equipes de desenvolvimento diferentes possam trabalhar em assemblies distintos
 *
 *     Reutilização do código:
 *         O mesmo assembly pode ser reutilizado em outras aplicações
 *         Conceito de biblioteca de classes
 *         
 * Estrutura de um Assembly?
 *     Windows Header:     Informações usadas pelo windows
 * 
 *     CLR Header:         Informações usadas pelo CLR
 *
 *     IL Code:            Código do assembly compilado em IL
 *                         (Intermediate Language)
 * 
 *     Type Metadata:      Metadados dos tipos contidos no assembly
 * 
 *     Assembly Manifest:  Versão do assembly, quem ele referencia
 * 
 *     Embedded Resources: (Opcional) arquivos de imagem, som, video, etc.
 *
 * O assembly armazena o código em forma de IL
 *    independente de linguagem de programação
 *    Códigos escritos em linguagem de programação distintas do assembly podem usá-lo
 * 
 * Tipos de assemblies
 *     privado
 *         Usados apenas por uma aplicação
 *         O arquivo do assembly fica localizado dentro do diretório da aplicação
 *     compartilhado
 *         usados por várias aplicações
 *         O arquivo do assembly fica em um local conhecido, denominado de Global Assembly Cache (GAC)
 * 
 * GAC (Global Assembly Cache)
 *     Repositório de assemblies
 *     Fica em dois locais
 *         C:\Windows\assembly
 *             Assemblies compilados em versão do .NET anteriores à 4.0
 *         C:\Windows\Microsoft.NET\assembly\GAC_MSIL
 *             Assemblies compilados na versão 4.0 do .NET ou superior
 *     Um asembly só pode ser adicionado ao GAC se ele possuir um strongName
 *         - Nome + Versão + Chave Pública + Assinatura Digital
 *         O uso de strong name evita a substituição do assembly original por outro contendo códigos maliciosos 
 */

using System;
using Math = SoftBlueLib.Math;

namespace AssembliesSoftBlue
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int x1 = 1, x2 = x1;
            
            Action mostraSoma = () => Console.WriteLine(Math.Sum(x1, x2));
            Action mostraSubtracao = () => Console.WriteLine(Math.Subtract(x1,x2));

            mostraSoma();
            mostraSubtracao();
        }
    }
}