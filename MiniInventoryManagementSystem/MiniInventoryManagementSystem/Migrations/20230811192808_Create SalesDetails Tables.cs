using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniInventoryManagementSystem.Migrations
{
    public partial class CreateSalesDetailsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesDetailsTable",
                columns: table => new
                {
                    SalesDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesDetailsPrice = table.Column<double>(type: "float", nullable: false),
                    SalesDetailsQuantity = table.Column<int>(type: "int", nullable: false),
                    SalesId = table.Column<int>(type: "int", nullable: false),
                    SalesDetails_ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDetailsTable", x => x.SalesDetailsId);
                    table.ForeignKey(
                        name: "FK_SalesDetailsTable_ProductTable_SalesDetails_ProductId",
                        column: x => x.SalesDetails_ProductId,
                        principalTable: "ProductTable",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesDetailsTable_SalesTable_SalesId",
                        column: x => x.SalesId,
                        principalTable: "SalesTable",
                        principalColumn: "SalesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailsTable_SalesDetails_ProductId",
                table: "SalesDetailsTable",
                column: "SalesDetails_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailsTable_SalesId",
                table: "SalesDetailsTable",
                column: "SalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesDetailsTable");
        }
    }
}
