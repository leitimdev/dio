using System;

namespace ExemploFundamentos.Models;

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

    public void Seno(double angulo)
    {
        double radiano = (Math.PI / 180) * angulo;
        double seno = Math.Sin(radiano);
        return Math.Sin(seno);
    }

    public void Coseno(double angulo)
    {
        double radiano = (Math.PI / 180) * angulo;
        double coseno = Math.Cos(radiano);
        return Math.Cos(coseno);
    }

    public void Tangente(double angulo)
    {
        double radiano = (Math.PI / 180) * angulo;
        double tangente = Math.Tan(radiano);
        return Math.Tan(tangente);
    }

    public void RaizQuadrada(double numero)
    {
        return Math.Sqrt(numero);
    }
}
