using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Depositar (decimal valor)
        {
            if (valor <= 0)
                return false;

            _saldo += valor;
            return true;
        }

        public bool Sacar(decimal valor)
        {
            if (valor <= 0 || valor > _saldo)
                return false;

            _saldo -= valor;
            return true;
        }
    }
}
