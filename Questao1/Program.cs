using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Questao1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Instancia a conta e deposita o valor inicial
            var conta = new Conta("123456789", "Alex");
            conta.Depositar(1000);

            // Instancia os objetos que irão sacar da conta
            var gastadora = new UsaConta(3000, 10, "A Gastadora", conta);
            var esperta = new UsaConta(6000, 50, "A Esperta", conta);
            var economica = new UsaConta(12000, 5, "A Econômica", conta);

            var tasks = new List<Task>();

            // Dispara as 3 tasks consumidoras, aguarda elas terminarem e dispara a task patrocinadora, reiniciando o processo
            while(true)
            {
                tasks.Add(Task.Run(() => gastadora.Sacar()));
                tasks.Add(Task.Run(() => esperta.Sacar()));
                tasks.Add(Task.Run(() => economica.Sacar()));

                Task.WaitAll(tasks.ToArray());
                await Task.Run(() => Patrocinadora(conta));
                tasks.Clear();
            }
        }

        /// <summary>
        /// Deposita um valor na conta
        /// </summary>
        /// <param name="conta">Conta a depositar</param>
        public static void Patrocinadora(Conta conta)
        {
            var valorDeposito = 100;

            Console.WriteLine();
            Console.WriteLine("========== Thread A Patrocinadora ==========");
            Console.WriteLine("Depositando " + valorDeposito + " na conta.");
            conta.Depositar(valorDeposito);
            Console.WriteLine("============================================");
        }
    }
}
