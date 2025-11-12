package dio.me;

import java.io.*;
import java.util.Scanner;

public class ExemploIOData {

    public static void incluirProduto() throws IOException {

        File f = new File("peca-de-roupa.bin");

        PrintStream ps = new PrintStream(System.out);
        ps.flush();

        //OutputStream os = new FileOutputStream(f.getPath());
        //DataOutputStream dos = new DataOutputStream(os);
        DataOutputStream dos = new DataOutputStream((new FileOutputStream(f.getPath())));

        Scanner scan = new Scanner(System.in);

        ps.print("Nome da peça: ");
        String nome = scan.nextLine();
        dos.writeUTF(nome);

        ps.print("Tamanho da peça (P/M/G/U): ");
        char tamanho = (char) System.in.read();
        dos.writeChar(tamanho);

        ps.print("Quantidade: ");
        int quant = scan.nextInt();
        dos.writeInt(quant);

        ps.print("Preço unitario: ");
        //double preco = scan.nextDouble();
        //dos.writeDouble(preco);
        int preco = scan.nextInt();
        dos.writeInt(preco);

        ps.printf("O arquivo %s foi criado com %d bytes no diretório '%s'.\n",
                f.getName(), f.length(), f.getAbsolutePath());


        dos.close();
        scan.close();
        ps.close();

    }

    public static void lerProduto(String caminhoArquivo) throws IOException {

        File f = new File(caminhoArquivo);

        DataInputStream dis = new DataInputStream(new FileInputStream(f.getPath()));

        String nome = dis.readUTF();
        char tamanho = dis.readChar();
        int quantidade = dis.readInt();
        int preco = dis.readInt();

        System.out.printf("\n Nome.............: %s\n", nome);
        System.out.printf("\n Qtdade...........: %d\n", quantidade);
        System.out.printf("\n Tamanho..........: %s\n", tamanho);
        System.out.printf("\n Preço............: %d\n", preco);
        System.out.printf("\n Valor total......: %d\n", quantidade * preco);

        dis.close();


    }

    public static void main(String[] args) throws IOException {
        // incluirProduto();

        lerProduto("peca-de-roupa.bin");

    }
}
