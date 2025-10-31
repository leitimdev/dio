using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO_v2.Models
{
    public class Corrente : Conta
    {
        public override void Depositar(decimal valor)
        {
            Saldo += valor;
        }

        public override void Sacar(decimal valor)
        {
            Saldo -= valor;
        }
    }
}