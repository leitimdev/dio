package one.dio.listasDuplaEncadeadas;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        ListaDuplaEncadeada<String> minhaLista = new ListaDuplaEncadeada<>();

        minhaLista.add("teste 1");
        minhaLista.add("teste 2");
        minhaLista.add("teste 3");
        minhaLista.add("teste 4");
        minhaLista.add("teste 5");

        out.println(minhaLista);

    }
}
