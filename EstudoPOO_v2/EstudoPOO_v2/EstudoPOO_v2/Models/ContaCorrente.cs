using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO_v2.Models
{
    public class ContaCorrente
    {
        public ContaCorrente(int agencia, int conta, decimal saldo)
        {
            Agencia = agencia;
            Conta = conta;
            Saldo = saldo;
        }
        public int Agencia { get; set; }
        public int Conta { get; set; }
        private decimal Saldo;

        public void Depositar(decimal valor)
        {
            Saldo += valor;
            Console.WriteLine($"Depósito de {valor} realizado com sucesso. Novo saldo: {Saldo}");
        }

        public void Sacar(decimal valor)
        {
            if (Saldo < valor)
            {
                Console.WriteLine("Saldo insuficiente");
                return;
            }
            Saldo -= valor;
            Console.WriteLine($"Saque de {valor} realizado com sucesso. Novo saldo: {Saldo}");
        }
        public void ExibirSaldo()
        {
            Console.WriteLine($"Saldo atual: {Saldo}");
        }
    }
}