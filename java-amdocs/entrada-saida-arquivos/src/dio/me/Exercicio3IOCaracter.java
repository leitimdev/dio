package dio.me;

import java.io.*;

public class Exercicio3IOCaracter {

    public static void copiarArquivo() throws IOException {
        File f = new File("recomendacoes.txt");
        String nomeArquivo = f.getName();

        BufferedReader br = new BufferedReader(new FileReader(nomeArquivo));
        String line = br.readLine();

        String nomeArquivoCopy = nomeArquivo.substring(0, nomeArquivo.indexOf(".")).concat("-copy.txt");
        File fcopy = new File(nomeArquivoCopy);

        BufferedWriter bw = new BufferedWriter(new FileWriter(fcopy.getName()));

        do {
            bw.write(line);
            bw.newLine();
            bw.flush();
            line = br.readLine();

        }while(!(line == null));

        System.out.printf("Arquivo \"%s\" copiado com sucesso \n", f.getName());
        System.out.printf("Arquivo \"%s\" copiado com sucesso \n", fcopy.getName());


        PrintWriter pw = new PrintWriter(System.out);
        pw.println("Recomende 3 livros:");
        pw.flush();

        adicionarInfoNoArquivo(fcopy.getName());

        pw.println("OK, tudo certo. \n");

        bw.close();
        br.close();

        pw.close();

    }

    public static void adicionarInfoNoArquivo(String arquivo) throws IOException {

        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        String line = br.readLine();

        // Adiciona o conteudo no arquivo que ja existe
        BufferedWriter bw = new BufferedWriter(new FileWriter(arquivo, true));

        // BufferedWriter bw = new BufferedWriter(new FileWriter(arquivo));

        do{
            bw.write(line);
            bw.newLine();
            line = br.readLine();

        }while(!(line.equalsIgnoreCase("fim")));

        br.close();
        bw.close();


    }

    public static void main(String[] args) throws IOException {
        copiarArquivo();

    }


}
