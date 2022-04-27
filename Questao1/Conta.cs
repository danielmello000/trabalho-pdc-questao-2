using System.Globalization;

namespace Questao1
{
    public class Conta
    {
        private decimal _saldo;
        public decimal Saldo => _saldo;
        public string Numero { get; set; }
        public string Titular { get; set; }

        public Conta(string numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            _saldo = 0;
        }

        /// <summary>
        /// Deposita o valor passado no saldo
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Sucesso da operação</returns>
        public bool Depositar (decimal valor)
        {
            if (valor <= 0)
                return false;

            _saldo += valor;
            return true;
        }

        /// <summary>
        /// Saca o valor passado do saldo
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Sucesso da operação</returns>
        public bool Sacar(decimal valor)
        {
            if (valor <= 0 || valor > _saldo)
                return false;

            _saldo -= valor;
            return true;
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular {Titular}, Saldo {Saldo.ToString("C", CultureInfo.CurrentCulture)}";
        }
    }
}
