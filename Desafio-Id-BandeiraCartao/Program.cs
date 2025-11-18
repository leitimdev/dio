using System;
using System.Linq;

namespace IdentificadorCartao
{
    // Classe responsável por identificar a bandeira do cartão
    class IdentificadorBandeira
    {
        private string numeroCartao;

        public IdentificadorBandeira(string numeroCartao)
        {
            // Remove espaços e hífens do número do cartão
            this.numeroCartao = numeroCartao.Replace(" ", "").Replace("-", "");
        }

        // Método que identifica a bandeira baseado no número do cartão
        public string IdentificarBandeira()
        {
            // Valida se o número contém apenas dígitos
            if (!numeroCartao.All(char.IsDigit))
            {
                return "Inválido";
            }

            // Valida o comprimento do número do cartão
            int tamanho = numeroCartao.Length;
            if (tamanho < 13 || tamanho > 19)
            {
                return "Inválido";
            }

            // Identifica a bandeira baseado nos primeiros dígitos
            
            // Visa: começa com 4
            if (numeroCartao.StartsWith("4"))
            {
                return "Visa";
            }

            // MasterCard: começa com 51-55 ou 2221-2720
            if (numeroCartao.Length >= 2)
            {
                int primeirosDois = int.Parse(numeroCartao.Substring(0, 2));
                if (primeirosDois >= 51 && primeirosDois <= 55)
                {
                    return "MasterCard";
                }
            }
            
            if (numeroCartao.Length >= 4)
            {
                int primeirosQuatro = int.Parse(numeroCartao.Substring(0, 4));
                if (primeirosQuatro >= 2221 && primeirosQuatro <= 2720)
                {
                    return "MasterCard";
                }
            }

            // American Express: começa com 34 ou 37
            if (numeroCartao.StartsWith("34") || numeroCartao.StartsWith("37"))
            {
                return "American Express";
            }

            // Discover: começa com 6011, 622126-622925, 644-649, ou 65
            if (numeroCartao.StartsWith("6011") || numeroCartao.StartsWith("65"))
            {
                return "Discover";
            }
            
            if (numeroCartao.Length >= 3)
            {
                int primeirosTres = int.Parse(numeroCartao.Substring(0, 3));
                if (primeirosTres >= 644 && primeirosTres <= 649)
                {
                    return "Discover";
                }
            }

            if (numeroCartao.Length >= 6)
            {
                int primeirosSeis = int.Parse(numeroCartao.Substring(0, 6));
                if (primeirosSeis >= 622126 && primeirosSeis <= 622925)
                {
                    return "Discover";
                }
            }

            // Elo: alguns dos BINs mais comuns
            if (numeroCartao.Length >= 6)
            {
                string primeirosSeis = numeroCartao.Substring(0, 6);
                string[] binsElo = { "401178", "401179", "438935", "457631", "457632", 
                                     "431274", "451416", "457393", "504175", "627780", 
                                     "636297", "636368", "650031", "650032", "650033", 
                                     "650035", "650036", "650037", "650038", "650039" };
                
                if (binsElo.Contains(primeirosSeis))
                {
                    return "Elo";
                }
            }

            // Hipercard: começa com 606282 ou 384100
            if (numeroCartao.StartsWith("606282") || numeroCartao.StartsWith("384100"))
            {
                return "Hipercard";
            }

            // Diners Club: começa com 300-305, 36, ou 38
            if (numeroCartao.Length >= 3)
            {
                int primeirosTres = int.Parse(numeroCartao.Substring(0, 3));
                if (primeirosTres >= 300 && primeirosTres <= 305)
                {
                    return "Diners Club";
                }
            }
            
            if (numeroCartao.StartsWith("36") || numeroCartao.StartsWith("38"))
            {
                return "Diners Club";
            }

            // JCB: começa com 3528-3589
            if (numeroCartao.Length >= 4)
            {
                int primeirosQuatro = int.Parse(numeroCartao.Substring(0, 4));
                if (primeirosQuatro >= 3528 && primeirosQuatro <= 3589)
                {
                    return "JCB";
                }
            }

            return "Desconhecida";
        }

        // Método para validar o cartão usando o algoritmo de Luhn
        public bool ValidarCartao()
        {
            if (!numeroCartao.All(char.IsDigit))
            {
                return false;
            }

            int soma = 0;
            bool alternar = false;

            // Percorre o número do cartão de trás para frente
            for (int i = numeroCartao.Length - 1; i >= 0; i--)
            {
                int digito = int.Parse(numeroCartao[i].ToString());

                if (alternar)
                {
                    digito *= 2;
                    if (digito > 9)
                    {
                        digito -= 9;
                    }
                }

                soma += digito;
                alternar = !alternar;
            }

            return (soma % 10) == 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Identificador de Bandeira de Cartão de Crédito ===\n");
            
            while (true)
            {
                Console.Write("Digite o número do cartão (ou 'sair' para encerrar): ");
                string entrada = Console.ReadLine();

                if (entrada?.ToLower() == "sair")
                {
                    Console.WriteLine("\nEncerrando o programa...");
                    break;
                }

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Número inválido. Tente novamente.\n");
                    continue;
                }

                IdentificadorBandeira identificador = new IdentificadorBandeira(entrada);
                string bandeira = identificador.IdentificarBandeira();
                bool valido = identificador.ValidarCartao();

                Console.WriteLine($"\nBandeira identificada: {bandeira}");
                Console.WriteLine($"Validação (Luhn): {(valido ? "Válido" : "Inválido")}\n");
                Console.WriteLine(new string('-', 50) + "\n");
            }
        }
    }
}