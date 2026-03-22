using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropriedadesHistoricas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropriedadesHistoricas",
                columns: table => new
                {
                    IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Data = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropriedadesHistoricas", x => new { x.IdLiderado, x.Tipo, x.Data });
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropriedadesHistoricas_IdLiderado_Tipo",
                table: "PropriedadesHistoricas",
                columns: new[] { "IdLiderado", "Tipo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropriedadesHistoricas");
        }
    }
}

