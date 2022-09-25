using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cliente.Entities.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cliente");

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.InsertData(
                schema: "Cliente",
                table: "Clientes",
                columns: new[] { "ClienteId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Cliente 1" },
                    { 2, "Cliente 2" },
                    { 3, "Cliente 3" },
                    { 4, "Cliente 4" },
                    { 5, "Cliente 5" },
                    { 6, "Cliente 6" },
                    { 7, "Cliente 7" },
                    { 8, "Cliente 8" },
                    { 9, "Cliente 9" },
                    { 10, "Cliente 10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "Cliente");
        }
    }
}
