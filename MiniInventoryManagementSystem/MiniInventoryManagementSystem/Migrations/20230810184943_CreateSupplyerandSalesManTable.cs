using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniInventoryManagementSystem.Migrations
{
    public partial class CreateSupplyerandSalesManTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesManTable",
                columns: table => new
                {
                    SalesManId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesManName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesManDOB = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesManAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SalesManDesignation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesManSalary = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesManJoiningDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesManPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesManEmail = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesManTable", x => x.SalesManId);
                });

            migrationBuilder.CreateTable(
                name: "SupplyerTable",
                columns: table => new
                {
                    SupplyerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplyerName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SupplyerAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplyerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyerTable", x => x.SupplyerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesManTable");

            migrationBuilder.DropTable(
                name: "SupplyerTable");
        }
    }
}
