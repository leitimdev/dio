namespace DesafioTecnico.Domain.ModelViews
{
    public struct Home
    {
        public string Mensagem { get => "API do Desafio Técnico. Use o endpoint /login para autenticação."; }
        public string Documentacao { get => "/swagger"; }
    }

}