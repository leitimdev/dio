package one.dio.listasCirculares;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {
        ListaCircular<String> minhaListaCircular = new ListaCircular<>();

        minhaListaCircular.add("conteudo 0");
        minhaListaCircular.add("conteudo 1");
        minhaListaCircular.add("conteudo 2");
        minhaListaCircular.add("conteudo 3");
        minhaListaCircular.add("conteudo 4");
        minhaListaCircular.add("conteudo 5");
        out.println(minhaListaCircular);

        out.println(minhaListaCircular.get(3));

        for(int i = 0; i < 20; i++){
            out.println(minhaListaCircular.get(i));
        }

    }
}
