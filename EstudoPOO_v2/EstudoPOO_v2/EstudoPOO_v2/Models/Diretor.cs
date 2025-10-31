using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO_v2.Models
{
    public class Diretor : Professor
    {
        public override void Apresentar()
        {
            Console.WriteLine($"Olá, eu sou o diretor {Nome}, tenho {Idade} anos, meu email é {Email} e meu salário é {Salario}.");
        }
    }
}