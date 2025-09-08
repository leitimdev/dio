using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;

namespace MinimalAPI.Dominio.Interfaces;

public interface IAdministradorServicos
{
    Administrador? Login(LoginDTO loginDTO);
}
