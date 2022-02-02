package one.dio;

public class Pilha {

    private No refNoEntradaPilha;

    public Pilha() {
        this.refNoEntradaPilha = null;
    }

    public void push(No novoNo){
        No refAuxiliar = refNoEntradaPilha;
        refNoEntradaPilha = novoNo;
        refNoEntradaPilha.setRefNo(refAuxiliar);
    }



    public No top(){
        return refNoEntradaPilha;
    }

    public Boolean isEmpty(){
        return refNoEntradaPilha == null ? true : false;
    }

}
