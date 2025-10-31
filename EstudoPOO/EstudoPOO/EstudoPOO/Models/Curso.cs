using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoPOO.Models
{
    public class Curso
    {
        public string Nome { get; set; }
        public List<Pessoa> Alunos { get; set; }

        public void AdicionarAluno(Pessoa aluno)
        {
            Alunos.Add(aluno);
        }

        public void ListarAlunos()
        {
            Console.WriteLine($"Alunos do curso de: {Nome}: ");
            for (int i = 0; i < Alunos.Count; i++)
            {
                //string texto = "Nº" + i + " - " + Alunos[i].NomeCompleto;
                string texto = $"Nº {i + 1} - {Alunos[i].NomeCompleto}";
                Console.WriteLine(texto);
            }

        }

        public void RemoverAluno(Pessoa aluno)
        {
            Alunos.Remove(aluno);
        }

        public int ObterQuantidadeAlunos()
        {
            return Alunos.Count;
        }
        



    }
}