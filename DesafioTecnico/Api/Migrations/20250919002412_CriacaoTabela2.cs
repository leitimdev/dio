using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioTecnico.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabela2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Perfil", "Senha" },
                values: new object[] { 1, "adm@projetodesafio.com", "Admin", "123456" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
