using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeSoftCatalogTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "analogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Article1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Article2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrustLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analogs");
        }
    }
}
