package one.dio2;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        Fila minhaFila = new Fila();
        minhaFila.enqueue(new No("Primeiro"));
        minhaFila.enqueue(new No("Segundo"));
        minhaFila.enqueue(new No("Terceiro"));
        minhaFila.enqueue(new No("Quarto"));

        out.println(minhaFila);



    }
}
