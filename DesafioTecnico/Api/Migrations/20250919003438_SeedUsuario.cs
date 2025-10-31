using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioTecnico.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "adm@desafiotecnico.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "adm@projetodesafio.com");
        }
    }
}
