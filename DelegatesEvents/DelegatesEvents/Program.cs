/*
 * Um delegate é um objeto capaz de referenciar um método(ou "apontar para um método")
 * semelhante aos ponteiros de c e c++ porém garante segurança
 *
 * Criando delegates:
 *     Delegate void AvisoConsulta(DateTime horario, string obs)
 *     O delegate AvisoConsulta pode apontar para qualquer metodo que respeite a sua assinatura
 *
 * Como referenciar um método dentro daquele delegate
 *     AvisoConsulta callback = new AvisoConsulta(MostraConsulta);
 *     MostraConsulta -> método a ser referenciado é passado como parametro no construtor do delegate;
 *                    -> lembrando que: O delegate AvisoConsulta pode apontar para qualquer metodo que respeite a sua assinatura
 *
 * Podem ser usados métodos estáticos ou de instância
 *
 * O bojeto do delegate deve ser usado para fazer a chamada
 *     é como se a chamada fosse feita diretamente ao objeto
 *     o delegate se encarrega de encaminhar a chamada para o método referenciado
 *     callback(DateTime.Now, "Chegar com 10 min de anteceência");
 *
 * O delegate também é capaz de referenciar mais de um método
 *     Use o operador += para referencia mais de um método
 *     Use o operador -= para remover um método do delegate
 *
 * O objeto delegate pode ser substituido apenas pelo nome do método que ele referencia
 *
 *     AvisoConsulta callback = new AvisoConsulta(MostrarConsulta);
 *     RegistrarCallBack(callback);
 *             |
 *             V
 *     RegistrarCallBack(MostrarConsulta);
 *
 *  Um delegate costuma ser usado apenas em contexto especifico
 *     baixo reaproveitamento
 *     isso ocasiona uma série de delegates no código da aplicação
 *     Delegates genéricos podem ser usados para evitar esse problema
 *  Usando quando o nome do delegate não importa
 *     podem ser de dois tipos
 *         Action:
 *             podem referenciar métodos de até: 16 parametros;
 *             os métodos dever possuir retorno: void;
 *             ex:
 *                 Action<DateTime, string> callback = new Action<DateTime, string>(MostrarConsulta)
 *                 ou
 *                 Action<DateTime, string> callback = MostrarConsulta;
 *
 *         Func:
 *             pode referenciar métodos de até 16 parâmetros;
 *             os métodos podem ter retorno;
 * 
 *             Func<parametro, parametro, retorno>;
 *
 *             ex:
 *                 Func<int, string, bool> callback = new Func<int,string,bool>(MeuMetodo);
 *                 ou
 *                 Func<int, string, bool> callback = MeuMetodo;
 * ________________________________________________________________________________________________________________
 *
 * Events
 * representam eventos em C#
 *     quando um objeto avisa a respeito de eventos que acontecem com ele
 *     outros objetos vão registrar interesseno evento em questão, e serão avisados quando esse evento for disparado
 *     Os events precisam de delegates para funcionarem
 *     é possivel ter esse mesmo comportamento usando somente delegates
 *         no entanto seu código se tornará mais complexo
 *
 * Criando Events:
 *     Criação do delegate:
 *         delegate void SapoHendler(double distância);
 * 
 *     Criação da classe e do event:
 *
 *     O objeto interessado no evento faz o registro do callback
 *
 *     delegate void SapoHandler(double distancia);

        class Pesquisador
        {
            public Pesquisador(Sapo sapo)
            {
                sapo.Pulou += SapoPulou; //Registra interesse no event Pulou no objeto Sapo
                                        //O operador -=pode ser usado para remover o interesse em um event
            }

            public void SapoPulou(double distancia)
            {
                
            }
         }

        
        class Sapo
        {
            public event SapoHandler Pulou;

            public void Pular()
            {
                Random r = new Random();

                int distancia = r.Next(30);

                if (Pulou != null)
                {
                    Pulou(distancia);
                }
            }
        }
        
        Padrão par a definição do event
            A Microsoft recomenda que eventos chamem métodos que:
                recebamdois parametros:
                    object sender
                        Indica o objeto que disparou o evento
                    system.EventArgs args
                        Define as informaçoes do evento
                    
        Com a arquitetura padrão
        
        class PulouEventArgs : EventArgs
        {
            public readonly double distancia;

            public PulouEventArgs(double distancia)
            {
                this.distancia = distancia;
            }
        }

        delegate void SapoHandler(object sender, PulouEventArgs args);

        class Sapo
        {
            public event SapoHandler Pulou;

            public void Pular()
            {
                Random r = new Random();

                int distancia = r.Next(30);

                if (Pulou != null)
                {
                    Pulou(this, new PulouEventArgs(distancia));
                }
            }
        }
        
        class Pesquisador
        {
            public Pesquisador(Sapo sapo)
            {
                sapo.Pulou += SapoPulou; //Registra interesse no event Pulou no objeto Sapo
                                        //O operador -=pode ser usado para remover o interesse em um event
            }

            public void SapoPulou(object sender, PulouEventArgs args)
            {
                Sapo s = sender as Sapo;
                double d = args.distancia;
            }
        }
        
        Muitos events recebem como parametro um objeto e um EventArgs
        
        O delegate EventHandler<T> facilita o uso destes tipos de eventos
            public event EventHandler<PulouEventArgs> Pulou;
                
                enum Cor
    {
        VERDE,
        AMARELO,
        VERMELHO
    }

    delegate void SemaforoHandler(Cor cor);
    
    class Semaforo
    {
        private Cor cor = Cor.VERMELHO;
        private SemaforoHandler callbacks;
                
        public void Iniciar()
        {
            while (true)
        {
        cor = (cor == Cor.VERMELHO ) ? 
             Cor.VERDE : 
            (cor == Cor.VERDE)? 
                Cor.AMARELO : 
                Cor.VERMELHO;

            Console.WriteLine($"Semáforo mudou para: {cor}");
            callbacks(cor);
            Thread.Sleep(2000);
        }
        }

        public void AdicionarCallback(SemaforoHandler semaforoHandler)
        {
        callbacks += semaforoHandler;
        }
        }

        class Carro
        {
        private int id;

        public Carro(int id)
        {
        this.id = id;
        }

        public void SemaforoAlterado(Cor cor)
        {
        Console.WriteLine($"Carro {id.ToString()} notificado: cor {cor}");
        }
    }
        
        
    internal class Program
    {
        public static void Main(string[] args)
        {
            Semaforo s = new Semaforo(); 
                        
            //            Carro c = new Carro(1);
            //            
            //            s.AdicionarCallback(c.SemaforoAlterado);

            int i = 1;
            while (i<=3)
            {
            Carro c = new Carro(i++);
            s.AdicionarCallback(c.SemaforoAlterado);
            }
                        
            s.Iniciar();
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            Temperature t = new Temperature();

            Func<double, double> converterToCelsius = t.ToCelsius;
            Func<double, double> converterToFahrenheit = t.ToFahrenheit;

            Action<double> printToCelsius = t.PrintToCelsius;
            Action<double> printToFahrenheit = t.PrintToFahrenheit;

//            double celsius = t.ToCelsius(90);
//            double fahrenheit = t.ToFahrenheit(25);

            double celsius = converterToCelsius(90);
            double fahrenheit = converterToFahrenheit(25);
            
            Console.WriteLine($"{celsius.ToString()}\n{fahrenheit.ToString()}");

            printToCelsius(fahrenheit);
            printToFahrenheit(celsius);
        }

    }

    public delegate double TemperatureConverter(double temp); 
    class Temperature
    {
        public double ToFahrenheit(double celsius)
        {
            return (celsius*9/5)+32;
        }

        public double ToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }
        
        public void PrintToFahrenheit(double celsius)
        {
            Console.WriteLine($"{(celsius*9/5)+32}");
            
        }

        public void PrintToCelsius(double fahrenheit)
        {
            Console.WriteLine($"{(fahrenheit - 32) * 5 / 9}");
        }
    }
 */

using System;
using System.Threading;

namespace DelegatesEvents
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            NumberGenerator g = new NumberGenerator();
            g.OnGenerated += NumberGenerated;
            g.Start();

        }

        public static void NumberGenerated(object sender, NumberEventArgs args)
        {
            Console.WriteLine($"Número gerado: {args.Number}");
        }
    }

    public delegate void NumberHandler(object sender, NumberEventArgs args);

    class NumberGenerator
    {

        public event NumberHandler OnGenerated;
        
        Random r = new Random();
        

        public void Start()
        {
            while (true)
            {
                int n = r.Next(100);
                
                OnGenerated?.Invoke(this, new NumberEventArgs(n));
                
                Thread.Sleep(1000);
            }
        }

    }

    public class NumberEventArgs:EventArgs
    {
        public int Number { get; private set; }

        public NumberEventArgs(int number)
        {
            Number = number;
        }
    }
}