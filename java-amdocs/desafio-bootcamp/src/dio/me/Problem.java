package dio.me;

import java.util.Scanner;

public class Problem {

    public static void main(String[] args) {

        Scanner entrada = new Scanner(System.in);

        int N = 1;

        System.out.println("Digite um numero: ");
        int num = entrada.nextInt();

        for (int i = 1; i <= num; i++)
            N = (N * i);
        System.out.println(" O Fatorial é: " + N);


    }
}
