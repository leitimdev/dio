import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        Map<String, String> aluno = new HashMap<>();

        aluno.put("Nome", "Thiago");
        aluno.put("Idade", "39");
        aluno.put("Media", "10");


        out.println(aluno);
        out.println(aluno.keySet());
        out.println(aluno.values());

        List<Map<String, String>> listaAlunos = new ArrayList<>();

        listaAlunos.add(aluno);


        Map<String, String> aluno2 = new HashMap<>();

        aluno2.put("Nome", "Ana Karina");
        aluno2.put("Idade", "35");
        aluno2.put("Media", "9");

        listaAlunos.add(aluno2);

        out.println(listaAlunos);

        out.println(aluno.containsKey("Nome"));

    }

}
