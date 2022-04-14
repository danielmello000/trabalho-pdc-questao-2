using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Questao2
{
    class Program
    {
        static void Main(string[] args)
        {
            FileUtil.GerarArquivosCursos();
            ExecutarSequencial();
            Console.WriteLine();
            ExecutarConcorrente();
        }

        /// <summary>
        /// Executa a questão de forma concorrente e exibe a tempo gasto
        /// </summary>
        private static void ExecutarConcorrente()
        {
            var inicio = DateTime.Now;
            Console.WriteLine($"{inicio.ToLongTimeString()} - Início execução de forma concorrente.");

            var tasks = new List<Task>();
            var formandos = new ConcurrentBag<string>();
            var files = FileUtil.GetArquivosCursos();

            // Dispara uma thread pra cada arquivo
            foreach (var filename in files)
            {
                tasks.Add(Task.Run(() => FileUtil.LerArquivo(filename, formandos)));
            }

            // Aguarda todas as threads terminarem
            Task.WaitAll(tasks.ToArray());

            var fileFormandos = $"formandos-concorrente";
            FileUtil.GravarArquivo(fileFormandos, formandos);

            var fim = DateTime.Now;
            Console.WriteLine($"{fim.ToLongTimeString()} - Fim execução de forma concorrente.");

            var tempo = fim - inicio;
            Console.WriteLine($"Tempo de execução: {tempo.TotalSeconds} segundos.");
        }

        /// <summary>
        /// Executa a questão de forma sequencial e exibe a tempo gasto
        /// </summary>
        private static void ExecutarSequencial()
        {
            var inicio = DateTime.Now;
            Console.WriteLine($"{inicio.ToLongTimeString()} - Início execução de forma sequencial.");

            var formandos = new ConcurrentBag<string>();
            var files = FileUtil.GetArquivosCursos();

            // Lê os arquivos de forma sequencial
            foreach (var filename in files)
            {
                FileUtil.LerArquivo(filename, formandos);
            }

            var fileFormandos = $"formandos-sequencial";
            FileUtil.GravarArquivo(fileFormandos, formandos);

            var fim = DateTime.Now;
            Console.WriteLine($"{fim.ToLongTimeString()} - Fim execução de forma sequencial.");

            var tempo = fim - inicio;
            Console.WriteLine($"Tempo de execução: {tempo.TotalSeconds} segundos.");
        }
    }
}
