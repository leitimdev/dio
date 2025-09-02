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
            foreach (var aluno in Alunos)
            {
                Console.WriteLine(aluno.NomeCompleto);
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