package one.dio;

import java.util.Stack;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        Stack<Carro> stackCarros = new Stack<>();
        stackCarros.push(new Carro("Ford"));
        stackCarros.push(new Carro("Chevrolet"));
        stackCarros.push(new Carro("VW"));

        out.println(stackCarros);
        out.println(stackCarros.pop()); //topo da pilha, retira

        out.println(stackCarros);
        out.println(stackCarros.peek()); //mostra mas não retira
        out.println(stackCarros);

        out.println(stackCarros.empty());
        out.println(stackCarros);


    }

}
