using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalidadeNineBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personalidade",
                columns: table => new
                {
                    IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalidade", x => new { x.IdLiderado, x.Data });
                });

            migrationBuilder.CreateTable(
                name: "NineBox",
                columns: table => new
                {
                    IdLiderado = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NineBox", x => new { x.IdLiderado, x.Data });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Personalidade");
            migrationBuilder.DropTable(name: "NineBox");
        }
    }
}

