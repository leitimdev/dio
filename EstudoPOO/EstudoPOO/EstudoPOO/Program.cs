using EstudoPOO.Models;

Pessoa pessoa1 = new Pessoa(nome: "Thiago", sobrenome: "Silva", idade: 42);
pessoa1.Apresentar();

Pessoa pessoa2 = new Pessoa(nome: "Thiago", sobrenome: "Teixeira", idade: 41);
pessoa2.Apresentar();

Pessoa pessoa3 = new Pessoa(nome: "Thiago", sobrenome: "Martins", idade: 40);
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
