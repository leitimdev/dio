using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO_v2.Models
{
    public class Professor : Pessoa
    {
        public Professor()
        {
        }
        public Professor(string nome) : base(nome)
        {
        }
        public double Salario { get; set; }

        public override void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome}, tenho {Idade} anos, meu email é {Email} e meu salário é {Salario}.");
        }
    }
}