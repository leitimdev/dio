using System;

namespace ExemploFundamentos.Common.Models;

public class Calculadora
{
    public int Somar(int numero1, int numero2)
    {
        return numero1 + numero2;
    }

    public int Subtrair(int numero1, int numero2)
    {
        return numero1 - numero2;
    }

    public int Multiplicar(int numero1, int numero2)
    {
        return numero1 * numero2;
    }

    public int Dividir(int numero1, int numero2)
    {
        return numero1 / numero2;
    }

    public int Potencia(int baseNumero, int expoente)
    {
        return (int)Math.Pow(baseNumero, expoente);
    }

    public double Seno(double angulo)
    {
        double radiano = (Math.PI / 180) * angulo;
        double seno = Math.Sin(radiano);
        return Math.Sin(seno);
    }

    public double Coseno(double angulo)
    {
        double radiano = (Math.PI / 180) * angulo;
        double coseno = Math.Cos(radiano);
        return Math.Cos(coseno);
    }

    public double Tangente(double angulo)
    {
        double radiano = (Math.PI / 180) * angulo;
        double tangente = Math.Tan(radiano);
        return Math.Tan(tangente);
    }

    public double RaizQuadrada(double numero)
    {
        return Math.Sqrt(numero);
    }
}
