package one.dio;

import java.util.LinkedList;
import java.util.Queue;
import java.util.Stack;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        Queue<Carro> queueCarros = new LinkedList<>(); // Lista de Carros

        queueCarros.add(new Carro("Ford"));
        queueCarros.add(new Carro("VW"));
        queueCarros.add(new Carro("Fiat"));


        out.println(queueCarros.add(new Carro("Peugeot")));
        out.println(queueCarros);

        out.println(queueCarros.offer(new Carro("Renault"))); // nao retorna erro, apenas false
        out.println(queueCarros);

        out.println(queueCarros.peek()); // pega o cabeça da fila, mas não remove
        out.println(queueCarros);

        out.println(queueCarros.poll()); // pega o cabeça da fila, mas remove
        out.println(queueCarros);

        out.println(queueCarros.isEmpty()); // se ta fazia ou nao a fila.

        out.println(queueCarros.size()); // tamanho da fila


    }

}
