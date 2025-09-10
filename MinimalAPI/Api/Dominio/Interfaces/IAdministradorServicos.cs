using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;

namespace MinimalAPI.Dominio.Interfaces;

public interface IAdministradorServicos
{
    Administrador? Login(LoginDTO loginDTO);
    Administrador Incluir(Administrador administrador);
    Administrador? BuscaPorID(int? pagina);
    List<Administrador> Todos(int? pagina);
}
