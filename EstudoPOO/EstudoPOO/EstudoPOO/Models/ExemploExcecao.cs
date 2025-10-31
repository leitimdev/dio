using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO.Models
{
    public class ExemploExcecao
    {
        public void Metodo1()
        {
            Console.WriteLine("Início do método 1");
            Metodo2();
            Console.WriteLine("Fim do método 1");
        }
        public void Metodo2()
        {
            Console.WriteLine("Início do método 2");
            Metodo3();
            Console.WriteLine("Fim do método 2");
        }
        public void Metodo3()
        {
            Console.WriteLine("Início do método 3");
            // Lançando uma exceção propositalmente
            throw new Exception("Ocorreu um erro no método 3");
            //Console.WriteLine("Fim do método 3");
        }
        public void Metodo4()
        {
            throw new Exception("Ocorreu um erro.");

        }
    }
}