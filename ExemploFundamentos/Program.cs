using ExemploFundamentos.Models;

Calculadora calc = new Calculadora();
int resultado = calc.Somar(10, 20);
Console.WriteLine($"Resultado da soma: {resultado}");

resultado = calc.Subtrair(10, 20);
Console.WriteLine($"Resultado da subtração: {resultado}");

resultado = calc.Multiplicar(10, 20);
Console.WriteLine($"Resultado da multiplicação: {resultado}");  

resultado = calc.Dividir(10, 20);
Console.WriteLine($"Resultado da divisão: {resultado}");

resultado = calc.Potencia(10, 3);
Console.WriteLine($"Resultado da potência: {resultado}");

resultado = (int)calc.Seno(30);
Console.WriteLine($"Resultado do seno: {resultado}");

resultado = (int)calc.Coseno(60);
Console.WriteLine($"Resultado do cosseno: {resultado}");

resultado = (int)calc.Tangente(45);
Console.WriteLine($"Resultado da tangente: {resultado}");

resultado = (int)calc.RaizQuadrada(9);
Console.WriteLine($"Resultado da raiz quadrada: {resultado}");

for(int contador = 0; contador < 10; contador++)
{
    if (contador == 5)
    {
        continue;
    }   
    Console.WriteLine($"Contador = {contador}");

    if (contador == 8)
    {
        break;
    }
}

/* Console.WriteLine("Hello, World!");

int quantidadeEmEstoque = 10;
int quantidadeCompra = 1;
bool possivelVenda = quantidadeCompra > 0 && quantidadeEmEstoque > quantidadeCompra;

Console.WriteLine($"Quantidade em estoque: {quantidadeEmEstoque}");
Console.WriteLine($"Quantidade compra: {quantidadeCompra}");
Console.WriteLine($"É possível realizar a venda? {possivelVenda}");

if (quantidadeCompra == 0)
{
    Console.WriteLine("Não houve venda.");
}
else if (possivelVenda)
{
    Console.WriteLine("Venda realizada.");
}
else
{
    Console.WriteLine("Desculpe. Não temos a quantidade desejada em estoque.");
}


Console.WriteLine("Digite seu nome:");
string nome = Console.ReadLine() ?? "";
Console.WriteLine("Digite sua idade:");
int idade = int.Parse(Console.ReadLine() ?? "0");

switch (idade)
{
    case < 0:
        Console.WriteLine("Idade inválida.");
        break;

    default:
        Pessoa pessoa = new Pessoa();
        pessoa.Nome = nome;
        pessoa.Idade = idade;
        pessoa.Apresentar();
        break;
}

if (idade < 18 && idade >= 0)
{
    Console.WriteLine("De menor.");
}
else if (idade >= 18)
{
    Console.WriteLine("De maior.");
}
 */
