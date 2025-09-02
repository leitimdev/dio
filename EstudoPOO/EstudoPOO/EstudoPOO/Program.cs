using EstudoPOO.Models;

Dictionary<string, string> estados = new Dictionary<string, string>();
estados.Add("SP", "São Paulo");
estados.Add("RJ", "Rio de Janeiro");
estados.Add("MG", "Minas Gerais");

foreach (var item in estados)
{
    Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
}
estados.Remove("MG");
estados["SP"] = "São Paulo - Atualizado";

Console.WriteLine("Após remover o estado de MG:");

foreach (var item in estados)
{
    Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
}

string chave = "SP";
if (estados.ContainsKey(chave))
{
    Console.WriteLine($"Valor da chave {chave}: {estados[chave]}");
}
else
{
    Console.WriteLine($"Chave {chave} não encontrada.");
}

Console.WriteLine($"Total de estados no dicionário: {estados.Count}");
Console.WriteLine($"Estado: {estados["SP"]}");

// ########### PILHA #############  

/* Stack<int> pilha = new Stack<int>();
pilha.Push(1);
pilha.Push(2);
pilha.Push(3);

foreach (var item in pilha)
{
    Console.WriteLine(item);
}

Console.WriteLine($"Removendo o topo da pilha: {pilha.Pop()}");

foreach (var item in pilha)
{
    Console.WriteLine(item);
} */

// ############# FILA #############

/* Queue<int> fila = new Queue<int>();
fila.Enqueue(1);
fila.Enqueue(2);
fila.Enqueue(3);

foreach (var item in fila)
{
    Console.WriteLine(item);
}

Console.WriteLine($"Removendo o primeiro elemento da fila: {fila.Dequeue()}");

foreach (var item in fila)
{
    Console.WriteLine(item);
}
 */

/* try
{
    string[] linhas = File.ReadAllLines("Arquivos/arquivoLeitura.txt");
    foreach (var linha in linhas)
    {
        Console.WriteLine(linha);
    }
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}
catch (DirectoryNotFoundException ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}
finally
{
    Console.WriteLine("Chegou no bloco finally.");
}
 */


/* Pessoa pessoa1 = new Pessoa(nome: "Thiago", sobrenome: "Silva", idade: 42);
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

decimal valorDecimal = 1.5m;
Console.WriteLine($"Valor monetário: {valorDecimal:C}");

double porcentagem = 0.3421;
Console.WriteLine($"Porcentagem: {porcentagem:P}");

DateTime data = DateTime.Now;
Console.WriteLine($"Data atual: {data:dd/MM/yyyy}");
Console.WriteLine($"Data atual: {data:dd/MM/yyyy HH:mm}");
Console.WriteLine($"Data atual: {data.ToShortDateString()}");
Console.WriteLine($"Data atual: {data.ToLongDateString()}");

string dataString = "2024-06-25 15:30";
DateTime dataConvertida = DateTime.Parse(dataString);
Console.WriteLine($"Data convertida: {dataConvertida:dd/MM/yyyy HH:mm}");
 */