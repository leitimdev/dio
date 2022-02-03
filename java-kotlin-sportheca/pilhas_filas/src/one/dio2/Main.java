package one.dio2;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {
//      Fila minhaFila = new Fila();
        Fila<String> minhaFila = new Fila<>();

     /*   minhaFila.enqueue(new No("Primeiro"));
        minhaFila.enqueue(new No("Segundo"));
        minhaFila.enqueue(new No("Terceiro"));
        minhaFila.enqueue(new No("Quarto"));
      */

        minhaFila.enqueue("Primeiro");
        minhaFila.enqueue("Segundo");
        minhaFila.enqueue("Terceiro");
        minhaFila.enqueue("Quarto");

        out.println(minhaFila);

        out.println(minhaFila.dequeue());

        out.println(minhaFila);

        minhaFila.enqueue("Ultimo");

        out.println(minhaFila);

        out.println(minhaFila.first());

        out.println(minhaFila);


    }
}
