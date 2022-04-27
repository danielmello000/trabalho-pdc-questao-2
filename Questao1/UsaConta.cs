namespace Questao1
{
    public class UsaConta
    {
        public int Intervalo { get; set; }
        public decimal ValorRetirada { get; set; }
        public Conta Conta { get; set; }
        public bool Suspenso { get; set; }

        public UsaConta(int intervalo, decimal valorRetirada, Conta conta)
        {
            Intervalo = intervalo;
            ValorRetirada = valorRetirada;
            Conta = conta;
        }
    }
}
