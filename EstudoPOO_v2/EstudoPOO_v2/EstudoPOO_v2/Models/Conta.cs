using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO_v2.Models
{
    public abstract class Conta
    {
        public int Numero { get; set; }
        public int Agencia { get; set; }
        protected decimal Saldo;
        public abstract void Depositar(decimal valor);
        public abstract void Sacar(decimal valor);
        public void ExibirSaldo()
        {
            Console.WriteLine($"Saldo atual: {Saldo}");
        }
    }
}