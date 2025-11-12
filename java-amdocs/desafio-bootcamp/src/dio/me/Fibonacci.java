package dio.me;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.util.Locale;
import java.util.Scanner;

public class Fibonacci {

    public static void main(String[] args) {

        Scanner sc = new Scanner(System.in);

        // Digite o número
        double n = sc.nextDouble();

        // Pega o resultado e calcula
        double resultado = fibonacci(n);
        System.out.printf("%.1f", resultado);

    }

    private static double fibonacci(double n) {

        double sqrtFive = Math.sqrt(5.0);
        double phi = (1 + sqrtFive) / 2;
        double result = Math.pow(phi, n) / sqrtFive;
        return result;
    }

}