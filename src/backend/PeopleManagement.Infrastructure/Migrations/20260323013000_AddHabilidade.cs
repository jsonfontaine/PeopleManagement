using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PeopleManagement.Infrastructure.Persistence;

#nullable disable

namespace PeopleManagement.Infrastructure.Migrations;

[DbContext(typeof(PeopleManagementDbContext))]
[Migration("20260323013000_AddHabilidade")]
public partial class AddHabilidade : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Habilidade",
            columns: table => new
            {
                IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                Data = table.Column<string>(type: "TEXT", nullable: false),
                Valor = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Habilidade", x => new { x.IdLiderado, x.Data });
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Habilidade");
    }
}

