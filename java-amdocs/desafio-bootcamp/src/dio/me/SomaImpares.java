package dio.me;
import java.io.IOException;
import java.util.Locale;
import java.util.Scanner;

public class SomaImpares {

    public static void main(String[] args)  throws IOException {
        Scanner sc = new Scanner(System.in);

        // Digite o número
        int X = sc.nextInt();

        // Digite outro número
        int Y = sc.nextInt();

        if (X > Y) {
            for (int i = X - 1; i > Y; i--) {

                if(!(i %2 == 0)) {
                    System.out.println(i);
                    break;
                }
            }
        } else if (X < Y ) {

            for (int i = X + 1; i < Y; i++) {

                if(!(i %2 == 0)) {
                    System.out.println(i);
                    break;
                }
            }

        } else if (X == Y)
        {
            System.out.println("0");
        }


        //Complete o código

    }
}