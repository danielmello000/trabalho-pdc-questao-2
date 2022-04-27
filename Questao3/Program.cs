using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        // Fila de clientes do cinema
        var fila = new List<string>
        {
            "Angela",
            "Bruno",
            "Celeste",
            "Duda",
            "Edu",
            "Fabio",
            "Geovana",
            "Hercules"
        };

        // Simulando o caixa do cinema recebendo os pedidos dos clientes, iniciando e finalizando
        foreach (var cliente in fila)
        {
            Console.WriteLine($"------Pedido de {cliente} iniciado!------");
            FazerPedido(cliente);
            Console.WriteLine($"------Pedido de {cliente} finalizado!------\n\n");

        }
    }

    /// <summary>
    /// Resumo:
    ///     Função onde recebe o nome do cliente, faz a chamada da preparação do pedido e
    ///     aguarda até que os 2 fiquem prontos
    /// </summary>
    public static void FazerPedido(string nome)
    {
        IniciarPreparacao();
        Console.WriteLine($"Pedido de {nome} está pronto!!");
    }

    /// <summary>
    /// Resumo: 
    ///     Função usada para chamar a preparação da pipoca e refrigerante.    
    /// </summary>
    public static void IniciarPreparacao()
    {
        // Cria 2 novas tarefas, uma com o metodo getPipoca, outra com o metodo getRefrigerante.
        var taskPipoca = Task.Run(() => GetPipoca());
        var taskRefri = Task.Run(() => GetRefigerante());

        // Realiza a espera das 2 tarefas terminarem para continuar a execução
        Task.WaitAll(taskPipoca, taskRefri);
    }


    /// <summary>
    /// Resumo:
    ///     Escreve no console o passo a passo da preparação da pipoca,
    ///     desde o recebimento do pedido até ser finalizado. 
    /// </summary>
    public static void GetPipoca()
    {
        Console.WriteLine($"Pedido de pipoca recebido!");
        Console.WriteLine($"Preparando pipoca em 6 segundos...");

        // Colocando a thread em espera por 6s
        Thread.Sleep(6000);

        Console.WriteLine($"Pipoca pronta!");
    }

    /// <summary>
    /// Resumo:
    ///     Escreve no console o passo a passo da preparação do refrigerante,
    ///     desde o recebimento do pedido até ser finalizado.
    /// </summary>
    public static void GetRefigerante()
    {
        Console.WriteLine($"Pedido de refrigerante recebido!");
        Console.WriteLine($"Preparando refrigerante em 2.5 segundos...");

        // Colocando a thread em espera por 2.5s
        Thread.Sleep(2500);

        Console.WriteLine($"Refrigerante pronto!");
    }
}