package one.dio;

public class BinNo <T extends Comparable<T>>{

    private T conteudo;
    private BinNo<T> noEsquerda;
    private BinNo<T> noDireita;

    public BinNo(T conteudo) {
        this.conteudo = conteudo;
        noEsquerda = noDireita = null;
    }

    public BinNo() {
    }

    public T getConteudo() {
        return conteudo;
    }

    public void setConteudo(T conteudo) {
        this.conteudo = conteudo;
    }

    public BinNo<T> getNoEsquerda() {
        return noEsquerda;
    }

    public void setNoEsquerda(BinNo<T> noEsquerda) {
        this.noEsquerda = noEsquerda;
    }

    public BinNo<T> getNoDireita() {
        return noDireita;
    }

    public void setNoDireita(BinNo<T> noDireita) {
        this.noDireita = noDireita;
    }

    @Override
    public String toString() {
        return "BinNo{" +
                "conteudo=" + conteudo +
                '}';
    }

}
