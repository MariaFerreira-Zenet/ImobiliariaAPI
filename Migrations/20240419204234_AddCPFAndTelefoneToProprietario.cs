using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImobiliariaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCPFAndTelefoneToProprietario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Proprietarios",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Proprietarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Proprietarios");
        }
    }
}
