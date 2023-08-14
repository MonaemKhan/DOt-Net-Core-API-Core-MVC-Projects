using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniInventoryManagementSystem.Migrations
{
    public partial class CreateSalesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesTable",
                columns: table => new
                {
                    SalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesManID = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SalesDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTable", x => x.SalesId);
                    table.ForeignKey(
                        name: "FK_SalesTable_CustomerTable_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerTable",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesTable_SalesManTable_SalesManID",
                        column: x => x.SalesManID,
                        principalTable: "SalesManTable",
                        principalColumn: "SalesManId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesTable_CustomerId",
                table: "SalesTable",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesTable_SalesManID",
                table: "SalesTable",
                column: "SalesManID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesTable");
        }
    }
}
