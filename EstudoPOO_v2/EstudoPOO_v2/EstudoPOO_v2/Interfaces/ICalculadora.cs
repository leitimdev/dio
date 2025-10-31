using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO_v2.Interfaces
{
    public interface ICalculadora
    {
        int Somar(int a, int b);
        int Subtrair(int a, int b);
        int Multiplicar(int a, int b);
        double Dividir(int a, int b);
    }
}