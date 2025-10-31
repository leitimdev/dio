namespace DesafioTecnico.Shared.DTOs
{
    // DTO para consulta de usuários (sem expor senha)
    public record UsuariosDTO(
        int Id,
        string Email,
        string Perfil
    );

    // DTO para criação de usuário
    public record UsuariosCreateDTO(
        string Email,
        string Senha,
        string Perfil
    );

    // DTO para atualização de usuário
    public record UsuariosUpdateDTO(
        int Id,
        string Email,
        string Perfil
    );

    // DTO para login
    public record UsuariosLoginDTO(
        string Email,
        string Senha
    );

    // DTO para resposta de login (sem expor senha)
    public record UsuariosLoginResponseDTO(
        int Id,
        string Email,
        string Perfil,
        string Token,
        DateTime ExpiracaoToken
    );

    // DTO para alteração de senha
    public record UsuariosAlterarSenhaDTO(
        int Id,
        string SenhaAtual,
        string NovaSenha
    );
}