package one.dio;

import java.util.*;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        List<Carro> listCarros = new ArrayList<>();

        listCarros.add(new Carro("VW"));
        listCarros.add(new Carro("Fiat"));
        listCarros.add(new Carro("Chevrolet"));
        listCarros.add(new Carro("Renault"));


        out.println(listCarros.contains(new Carro("Ford")));

        out.println(listCarros.get(2));

        out.println(listCarros.indexOf(new Carro("Fiat")));

        out.println(listCarros.remove(2));
        out.println(listCarros);



    }

}
