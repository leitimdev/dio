package dio.one;

import java.util.List;
import java.util.Vector;

public class ExemploVector {

    public static void main(String[] args) {

        List<String> esportes = new Vector<String>();

        esportes.add("Teste");

        for (String esporte: esportes) {
            System.out.println(esporte);

        }

    }

}
