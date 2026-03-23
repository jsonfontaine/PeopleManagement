using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PeopleManagement.Infrastructure.Persistence;

#nullable disable

namespace PeopleManagement.Infrastructure.Migrations;

[DbContext(typeof(PeopleManagementDbContext))]
[Migration("20260323010000_RemovePropriedadesHistoricasAddConhecimento")]
public partial class RemovePropriedadesHistoricasAddConhecimento : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Conhecimento",
            columns: table => new
            {
                IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                Data = table.Column<string>(type: "TEXT", nullable: false),
                Valor = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Conhecimento", x => new { x.IdLiderado, x.Data });
            });

        migrationBuilder.Sql(
            """
            INSERT OR IGNORE INTO Conhecimento (IdLiderado, Data, Valor)
            SELECT IdLiderado, Data, Valor
            FROM PropriedadesHistoricas
            WHERE lower(Tipo) = 'conhecimentos';
            """);

        migrationBuilder.DropTable(name: "PropriedadesHistoricas");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
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

        migrationBuilder.Sql(
            """
            INSERT OR IGNORE INTO PropriedadesHistoricas (IdLiderado, Tipo, Data, Valor)
            SELECT IdLiderado, 'conhecimentos', Data, Valor
            FROM Conhecimento;
            """);

        migrationBuilder.DropTable(name: "Conhecimento");
    }
}

