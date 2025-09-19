using DesafioTecnico.Domain.DTOs;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Interfaces
{
    /// <summary>
    /// Interface para serviços de autenticação e gestão de usuários
    /// Responsável por: Login, JWT, CRUD de usuários
    /// </summary>
    public interface IUsuariosServicos
    {
        // Autenticação
        Task<UsuariosLoginResponseDTO> Login(UsuariosLoginDTO loginDto);
        Task<bool> ValidarToken(string token);
        Task Logout(int usuarioId);

        // CRUD de Usuários
        Task<UsuariosDTO> Criar(UsuariosCreateDTO createDto);
        Task<UsuariosDTO> Atualizar(UsuariosUpdateDTO updateDto);
        Task<bool> Excluir(int id);

        // Gestão de Senhas
        Task<bool> AlterarSenha(UsuariosAlterarSenhaDTO alterarSenhaDto);
        Task<bool> ValidarSenha(int usuarioId, string senha);

    }
}
