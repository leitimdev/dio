package one.dio;

import static java.lang.System.out;

public class Main {

    public static void main(String[] args) {

        Pilha minhaPilha = new Pilha();

        minhaPilha.push(new No(1));
        minhaPilha.push(new No(2));
        minhaPilha.push(new No(3));
        minhaPilha.push(new No(4));
        minhaPilha.push(new No(5));

        out.println(minhaPilha);
        out.println(minhaPilha.pop());
        out.println(minhaPilha);
        minhaPilha.push(new No(6));
        out.println(minhaPilha);


    }

}
