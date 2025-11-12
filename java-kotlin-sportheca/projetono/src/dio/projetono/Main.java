package dio.projetono;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        No<String> no1 = new No<>("Conteudo no1");
        No<String> no2 = new No<>("Conteudo no2");

        no1.setProximoNo(no2);

        No<String> no3 = new No<>("Conteudo no3");
        no2.setProximoNo(no3);

        No<String> no4 = new No<>("Conteudo no4");
        no3.setProximoNo(no4);

        out.println(no1);
        out.println(no1.getProximoNo());
        out.println(no1.getProximoNo().getProximoNo());
        out.println(no1.getProximoNo().getProximoNo().getProximoNo());
        out.println(no1.getProximoNo().getProximoNo().getProximoNo().getProximoNo());




    }

}
