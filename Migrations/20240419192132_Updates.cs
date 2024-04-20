using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImobiliariaApi.Migrations
{
    /// <inheritdoc />
    public partial class Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imoveis_Corretores_CorretorId",
                table: "Imoveis");

            migrationBuilder.DropIndex(
                name: "IX_Imoveis_CorretorId",
                table: "Imoveis");

            migrationBuilder.DropColumn(
                name: "CorretorId",
                table: "Imoveis");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Proprietarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Inquilinos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Corretores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CorretorInquilino",
                columns: table => new
                {
                    CorretorId = table.Column<int>(type: "int", nullable: false),
                    InquilinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorretorInquilino", x => new { x.CorretorId, x.InquilinoId });
                    table.ForeignKey(
                        name: "FK_CorretorInquilino_Corretores_CorretorId",
                        column: x => x.CorretorId,
                        principalTable: "Corretores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorretorInquilino_Inquilinos_InquilinoId",
                        column: x => x.InquilinoId,
                        principalTable: "Inquilinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProprietarioCorretor",
                columns: table => new
                {
                    ProprietarioId = table.Column<int>(type: "int", nullable: false),
                    CorretorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProprietarioCorretor", x => new { x.ProprietarioId, x.CorretorId });
                    table.ForeignKey(
                        name: "FK_ProprietarioCorretor_Corretores_CorretorId",
                        column: x => x.CorretorId,
                        principalTable: "Corretores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProprietarioCorretor_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorretorInquilino_InquilinoId",
                table: "CorretorInquilino",
                column: "InquilinoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProprietarioCorretor_CorretorId",
                table: "ProprietarioCorretor",
                column: "CorretorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorretorInquilino");

            migrationBuilder.DropTable(
                name: "ProprietarioCorretor");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Proprietarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Inquilinos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CorretorId",
                table: "Imoveis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Corretores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Imoveis_CorretorId",
                table: "Imoveis",
                column: "CorretorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imoveis_Corretores_CorretorId",
                table: "Imoveis",
                column: "CorretorId",
                principalTable: "Corretores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
