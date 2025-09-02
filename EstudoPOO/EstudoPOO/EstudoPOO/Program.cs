using EstudoPOO.Models;

Pessoa pessoa1 = new Pessoa();
pessoa1.Nome = "Thiago";
pessoa1.Sobrenome = "Silva";
pessoa1.Idade = 42;
pessoa1.Apresentar();

Pessoa pessoa2 = new Pessoa();
pessoa2.Nome = "Thiago";
pessoa2.Sobrenome = "Teixeira";
pessoa2.Idade = 41;
pessoa2.Apresentar();

Pessoa pessoa3 = new Pessoa();
pessoa3.Nome = "Thiago";
pessoa3.Sobrenome = "Martins";
pessoa3.Idade = 40;
pessoa3.Apresentar();

Curso curso = new Curso();
curso.Nome = "Curso de C#";
curso.Alunos = new List<Pessoa>();

curso.AdicionarAluno(pessoa1);
curso.AdicionarAluno(pessoa2);
curso.AdicionarAluno(pessoa3);
Console.WriteLine($"Quantidade de alunos no curso {curso.Nome}: {curso.ObterQuantidadeAlunos()}");
Console.WriteLine("Lista de alunos:");
curso.ListarAlunos();
