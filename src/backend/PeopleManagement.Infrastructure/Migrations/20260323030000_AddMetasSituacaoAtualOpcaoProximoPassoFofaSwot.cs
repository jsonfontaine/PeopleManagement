using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PeopleManagement.Infrastructure.Persistence;

#nullable disable

namespace PeopleManagement.Infrastructure.Migrations;

[DbContext(typeof(PeopleManagementDbContext))]
[Migration("20260323030000_AddMetasSituacaoAtualOpcaoProximoPassoFofaSwot")]
public partial class AddMetasSituacaoAtualOpcaoProximoPassoFofaSwot : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        foreach (var name in new[] { "Meta", "SituacaoAtual", "Opcao", "ProximoPasso", "Fortaleza", "Oportunidade", "Fraqueza", "Ameaca" })
        {
            migrationBuilder.CreateTable(
                name: name,
                columns: table => new
                {
                    IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey($"PK_{name}", x => new { x.IdLiderado, x.Data });
                });
        }
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        foreach (var name in new[] { "Meta", "SituacaoAtual", "Opcao", "ProximoPasso", "Fortaleza", "Oportunidade", "Fraqueza", "Ameaca" })
        {
            migrationBuilder.DropTable(name: name);
        }
    }
}

