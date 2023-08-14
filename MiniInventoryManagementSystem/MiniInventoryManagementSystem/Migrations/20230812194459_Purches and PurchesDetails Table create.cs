using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniInventoryManagementSystem.Migrations
{
    public partial class PurchesandPurchesDetailsTablecreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchesTable",
                columns: table => new
                {
                    PurchesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchesDate = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SupplyerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchesTable", x => x.PurchesId);
                    table.ForeignKey(
                        name: "FK_PurchesTable_SupplyerTable_SupplyerId",
                        column: x => x.SupplyerId,
                        principalTable: "SupplyerTable",
                        principalColumn: "SupplyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchesDetailsTable",
                columns: table => new
                {
                    PurchesDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchesId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PurchesDetailsPrice = table.Column<double>(type: "float", nullable: false),
                    PurchesDetailsQuantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchesDetailsTable", x => x.PurchesDetailsId);
                    table.ForeignKey(
                        name: "FK_PurchesDetailsTable_ProductTable_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductTable",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchesDetailsTable_PurchesTable_PurchesId",
                        column: x => x.PurchesId,
                        principalTable: "PurchesTable",
                        principalColumn: "PurchesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchesDetailsTable_ProductId",
                table: "PurchesDetailsTable",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchesDetailsTable_PurchesId",
                table: "PurchesDetailsTable",
                column: "PurchesId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchesTable_SupplyerId",
                table: "PurchesTable",
                column: "SupplyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchesDetailsTable");

            migrationBuilder.DropTable(
                name: "PurchesTable");
        }
    }
}
