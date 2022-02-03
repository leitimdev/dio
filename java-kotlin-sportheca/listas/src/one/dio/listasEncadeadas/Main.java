package one.dio.listasEncadeadas;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        ListaEncadeada<String> minhaListaEncadeada = new ListaEncadeada<>();
        minhaListaEncadeada.add("1");
        minhaListaEncadeada.add("2");
        minhaListaEncadeada.add("3");
        minhaListaEncadeada.add("4");
        minhaListaEncadeada.add("5");

        out.println(minhaListaEncadeada);

    }
}
