using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PeopleManagement.Infrastructure.Persistence;

#nullable disable

namespace PeopleManagement.Infrastructure.Migrations;

[DbContext(typeof(PeopleManagementDbContext))]
[Migration("20260323020000_AddAtitudeValorExpectativa")]
public partial class AddAtitudeValorExpectativa : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Atitude",
            columns: table => new
            {
                IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                Data = table.Column<string>(type: "TEXT", nullable: false),
                Valor = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Atitude", x => new { x.IdLiderado, x.Data });
            });

        migrationBuilder.CreateTable(
            name: "Valor",
            columns: table => new
            {
                IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                Data = table.Column<string>(type: "TEXT", nullable: false),
                Valor = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Valor", x => new { x.IdLiderado, x.Data });
            });

        migrationBuilder.CreateTable(
            name: "Expectativa",
            columns: table => new
            {
                IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                Data = table.Column<string>(type: "TEXT", nullable: false),
                Valor = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Expectativa", x => new { x.IdLiderado, x.Data });
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Atitude");
        migrationBuilder.DropTable(name: "Valor");
        migrationBuilder.DropTable(name: "Expectativa");
    }
}

