package dio.me;

import java.io.*;

public class ExemploIOBytes {

    public static void copiarArquivo() throws IOException {
        File f = new File("recomendacoes-copy.txt");
        String nomeArquivo = f.getName();

        BufferedInputStream bis = new BufferedInputStream(new FileInputStream(f.getName()));

        String nomeArquivoCopy = nomeArquivo.substring(0, nomeArquivo.indexOf("-")).concat("-copy2.txt");
        File fCopy = new File(nomeArquivoCopy);

        BufferedOutputStream bos = new BufferedOutputStream(new FileOutputStream(nomeArquivoCopy));

        int line = 0;
        while((line = bis.read()) != -1) {
            bos.write((char) line);
            bos.flush();
        }

        bis.close();
        bos.close();

        System.out.println("Arquivo copiado! " + fCopy.getName() + " " + fCopy.getPath() + " " + fCopy.length());

    }

    public static void main(String[] args) throws IOException {

        copiarArquivo();

    }

}
