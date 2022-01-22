package dio.one;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.Queue;

public class ExemploLinkedList {

    public static void main(String[] args) {

        Queue<String> filaBanco = new LinkedList<>();

        filaBanco.add("Patrícia");
        filaBanco.add("Roberto");
        filaBanco.add("Flávio");
        filaBanco.add("Pamela");
        filaBanco.add("Anderson");

        System.out.println(filaBanco);

        // Remove a Patricia, primeira a chegar a primeira a sair
        String clienteAtendida = filaBanco.poll();
        System.out.println(clienteAtendida);

        System.out.println(filaBanco);

        // Somente retorna o primeiro elemento da Fila
        String primeiroCliente = filaBanco.peek();
        System.out.println(primeiroCliente);
        System.out.println(filaBanco);

        // Nao pode ter uma fila vazia pra ser chamado // Retorna se estiver vazio
        // filaBanco.clear();
        String primeiroClienteOuErro = filaBanco.element();
        System.out.println(primeiroClienteOuErro);
        System.out.println(filaBanco);

        // for each
        for (String client: filaBanco) {
            System.out.println(client);
        }

        Iterator<String> iteratorFilaBanco = filaBanco.iterator();

        while(iteratorFilaBanco.hasNext()){
            System.out.println("-->" + iteratorFilaBanco.next());

        }

        System.out.println(filaBanco.size());
        System.out.println(filaBanco.isEmpty());


    }
}
