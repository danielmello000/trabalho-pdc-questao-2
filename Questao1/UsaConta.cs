using System;
using System.Globalization;
using System.Threading;

namespace Questao1
{
    public class UsaConta
    {
        public string Nome { get; set; }
        public int Intervalo { get; set; }
        public decimal ValorRetirada { get; set; }
        public Conta Conta { get; set; }
        public int QuantidadeSaques { get; set; }

        public UsaConta(int intervalo, decimal valorRetirada, string nome, Conta conta)
        {
            Intervalo = intervalo;
            ValorRetirada = valorRetirada;
            Nome = nome;
            Conta = conta;
        }

        /// <summary>
        /// Enquanto o saldo da conta for maior que 0, saca uma quantidade a cada x segundos
        /// Após o saldo zerar, imprime quantas vezes e o valor total de saque
        /// </summary>
        public void Sacar()
        {
            while (Conta.Saldo > 0)
            {
                // Sincroniza o acesso a conta
                Monitor.Enter(Conta);
                Console.WriteLine();
                Console.WriteLine("========== Thread " + Nome + " - " + DateTime.Now.ToLongTimeString() + " ==========");
                Console.WriteLine("Sacando " + ValorRetirada + " de " + Conta.Saldo.ToString("C", CultureInfo.CurrentCulture));

                // Tenta sacar o valor da conta
                if (Conta.Sacar(ValorRetirada))
                {
                    QuantidadeSaques++;
                    Console.WriteLine("Operação bem sucedida. Saldo atual: " + Conta.Saldo.ToString("C", CultureInfo.CurrentCulture));
                }
                else
                {
                    Console.WriteLine("Saldo insuficiente.");
                }
                Console.WriteLine("===================================================");
                Monitor.Exit(Conta);
                Thread.Sleep(Intervalo);
            }

            // Saldo zerado, imprime a quantidade e o valor sacado
            Console.WriteLine();
            Console.WriteLine("========== Thread " + Nome + " ==========");
            Console.WriteLine("Quantidade de saques efetuados: " + QuantidadeSaques);
            Console.WriteLine("Valor retirado da conta: " + (QuantidadeSaques * ValorRetirada).ToString("C", CultureInfo.CurrentCulture));
        }

        public override string ToString()
        {
            return $"{Nome} - executa a cada {Intervalo}ms, retira {ValorRetirada.ToString("C", CultureInfo.CurrentCulture)}";
        }
    }
}
