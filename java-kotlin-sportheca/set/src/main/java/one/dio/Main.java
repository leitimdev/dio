package one.dio;

import java.util.*;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {


        Set<Carro> hashSetCarros = new HashSet<>();
        hashSetCarros.add(new Carro("Ford"));
        hashSetCarros.add(new Carro("Renault"));
        hashSetCarros.add(new Carro("VW"));
        hashSetCarros.add(new Carro("Fiat"));


        out.println(hashSetCarros);

        Set<Carro> treeSetCarros = new TreeSet<>();
        treeSetCarros.add(new Carro("Ford"));
        treeSetCarros.add(new Carro("Renault"));
        treeSetCarros.add(new Carro("VW"));
        treeSetCarros.add(new Carro("Fiat"));

        out.println(treeSetCarros);



    }

}
