using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstudoPOO_v2.Interfaces;

namespace EstudoPOO_v2.Models
{
    public class Calculadora : ICalculadora
    {
        public int Somar(int a, int b)
        {
            return a + b;
        }
        public int Subtrair(int a, int b)
        {
            return a - b;
        }
        public int Multiplicar(int a, int b)
        {
            return a * b;
        }
        public double Dividir(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Divisor não pode ser zero.");
            }
            return (double)a / b;
        }
    }
}