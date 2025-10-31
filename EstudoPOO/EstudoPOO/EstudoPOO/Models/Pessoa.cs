using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO.Models
{
    public class Pessoa
    {
        public Pessoa()
        { 

        }
        public Pessoa(string nome, string sobrenome, int idade)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Idade = idade;

        }

        public void Deconstruct(out string nome, out string sobrenome, out int idade)
        {
            nome = Nome;
            sobrenome = Sobrenome;
            idade = Idade;
        }

        private string _nome;
        private int _idade;

        public string Nome
        {
            get => _nome.ToUpper();
            
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("O nome não pode ser vazio.");
                }
                _nome = value;
            }
        }
        
        public string Sobrenome { get; set; }
        public string NomeCompleto => $"{Nome} {Sobrenome}".ToUpper();

        public int Idade
        {
            get => _idade;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("A idade não pode ser negativa ou zero.");
                }
                _idade = value;
            }
        }

        public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {NomeCompleto} e tenho {Idade} anos.");
        }



    }
}