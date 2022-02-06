package one.dio;

import java.util.ArrayList;
import java.util.List;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {
        List<Carro> listaCarros = new ArrayList<>();

        listaCarros.add(new Carro("Ford"));
        listaCarros.add(new Carro("Chevrolet"));
        listaCarros.add(new Carro("VW"));

        out.println(listaCarros.contains(new Carro("Ford")));
        out.println(new Carro("Ford").hashCode());

        Carro carro1 = new Carro("Ford");
        Carro carro2 = new Carro("VW");

        out.println(carro1.equals(carro2));

    }

}
