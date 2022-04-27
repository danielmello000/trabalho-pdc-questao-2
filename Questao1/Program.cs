using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Questao1
{
    class Program
    {
        public static List<Thread> Threads { get; set; }
        public static List<UsaConta> Models { get; set; }

        static void Main(string[] args)
        {

            var conta = new Conta("123456789", "Alex");
            conta.Depositar(150);

            var gastadora = new UsaConta(3000, 10, conta);
            var esperta = new UsaConta(6000, 50, conta);
            var economica = new UsaConta(12000, 5, conta);

            //Thread thrGastadora = new Thread(gastadora.Start);
            //Thread thrEsperta = new Thread(esperta.Start);
            //Thread thrEconomica = new Thread(economica.Start);

            Thread thrGastadora = new Thread(() => { UsarConta(gastadora); });
            Thread thrEsperta = new Thread(() => { UsarConta(esperta); });
            Thread thrEconomica = new Thread(() => { UsarConta(economica); });

            thrGastadora.Name = "A Gastadora";
            thrEsperta.Name = "A Esperta";
            thrEconomica.Name = "A Econômica";

            Threads = new List<Thread>(){ thrGastadora, thrEsperta, thrEconomica };
            Models = new List<UsaConta>(){ gastadora, esperta, economica };

            thrGastadora.Start();
            thrEsperta.Start();
            thrEconomica.Start();

            //thrGastadora.Join();
            //thrEsperta.Join();
            //thrEconomica.Join();

            //tasks.Add(Task.Run(() => gastadora.Start()));
            //tasks.Add(Task.Run(() => esperta.Start()));
            //tasks.Add(Task.Run(() => economica.Start()));

            //Task.WaitAll(tasks.ToArray());

            //Console.WriteLine("teste");

            //Console.ReadLine();
        }

        public static void UsarConta(UsaConta model)
        {
            var conta = model.Conta;
            var valorRetirada = model.ValorRetirada;
            var intervalo = model.Intervalo;

            while (true)
            {
                Monitor.Enter(conta);
                Thread thr = Thread.CurrentThread;
                Console.WriteLine();
                Console.WriteLine("========== Thread " + thr.Name + " - " + DateTime.Now.ToLongTimeString() + " ==========");
                Console.WriteLine("Sacando " + valorRetirada + " de " + conta.Saldo.ToString("C", CultureInfo.CurrentCulture));

                if (conta.Sacar(valorRetirada))
                {
                    Console.WriteLine("Operação bem sucedida. Saldo atual: " + conta.Saldo.ToString("C", CultureInfo.CurrentCulture));
                }
                else
                {
                    Console.WriteLine("Saldo insuficiente.");
                }
                if (conta.Saldo == 0)
                {
                    //_timer.Stop();
                    Console.WriteLine("parando");

                    //Console.WriteLine("teste");
                    //Task.Delay(int.MaxValue);
                    //Monitor.Wait(conta);
                    model.Suspenso = true;
                    Teste();
                    Monitor.Wait(conta);
                }
                Console.WriteLine("===================================================");
                Monitor.Exit(conta);
                Thread.Sleep(intervalo);
            }
        }

        public static void Teste() {

            var allSleeping = Models.Select(x => x.Suspenso).All(x => x);

            if (allSleeping)
            {
                Console.WriteLine("aaaaaaaa");
            } else
            {
                Console.WriteLine("bbbbbbbb");
            }
        }
    }
}
