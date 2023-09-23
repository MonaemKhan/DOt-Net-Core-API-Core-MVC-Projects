using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alphabyte_Technology_Ltd_.Migrations
{
    public partial class InitialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivisionTable",
                columns: table => new
                {
                    DivisionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionTable", x => x.DivisionId);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentTable",
                columns: table => new
                {
                    Dept_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Department_DivisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTable", x => x.Dept_Id);
                    table.ForeignKey(
                        name: "FK_DepartmentTable_DivisionTable_Department_DivisionId",
                        column: x => x.Department_DivisionId,
                        principalTable: "DivisionTable",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterViewerDetailsTable",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InterViewerDetails_DivisionId = table.Column<int>(type: "int", nullable: false),
                    InterViewerDetails_DepartmentDept_Id = table.Column<int>(type: "int", nullable: false),
                    DoB = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResumeFile = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterViewerDetailsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterViewerDetailsTable_DepartmentTable_InterViewerDetails_DepartmentDept_Id",
                        column: x => x.InterViewerDetails_DepartmentDept_Id,
                        principalTable: "DepartmentTable",
                        principalColumn: "Dept_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterViewerDetailsTable_DivisionTable_InterViewerDetails_DivisionId",
                        column: x => x.InterViewerDetails_DivisionId,
                        principalTable: "DivisionTable",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DivisionTable",
                columns: new[] { "DivisionId", "DivisionName" },
                values: new object[] { 1, "IT" });

            migrationBuilder.InsertData(
                table: "DivisionTable",
                columns: new[] { "DivisionId", "DivisionName" },
                values: new object[] { 2, "BBA" });

            migrationBuilder.InsertData(
                table: "DepartmentTable",
                columns: new[] { "Dept_Id", "Department_DivisionId", "DeptName" },
                values: new object[,]
                {
                    { 1, 1, "CSE" },
                    { 2, 1, "SE" },
                    { 3, 2, "MGT" },
                    { 4, 2, "THM" },
                    { 5, 1, "Software" }
                });

            migrationBuilder.InsertData(
                table: "InterViewerDetailsTable",
                columns: new[] { "Id", "DoB", "InterViewerDetails_DepartmentDept_Id", "InterViewerDetails_DivisionId", "Name", "ResumeFile" },
                values: new object[] { "10000001", "2000-09-07", 5, 1, "Ashraful Alam", "File1.pdf" });

            migrationBuilder.InsertData(
                table: "InterViewerDetailsTable",
                columns: new[] { "Id", "DoB", "InterViewerDetails_DepartmentDept_Id", "InterViewerDetails_DivisionId", "Name", "ResumeFile" },
                values: new object[] { "10000002", "1998-04-21", 1, 1, "M.A. Monaem Khan", "File2.pdf" });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentTable_Department_DivisionId",
                table: "DepartmentTable",
                column: "Department_DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_InterViewerDetailsTable_InterViewerDetails_DepartmentDept_Id",
                table: "InterViewerDetailsTable",
                column: "InterViewerDetails_DepartmentDept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_InterViewerDetailsTable_InterViewerDetails_DivisionId",
                table: "InterViewerDetailsTable",
                column: "InterViewerDetails_DivisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterViewerDetailsTable");

            migrationBuilder.DropTable(
                name: "DepartmentTable");

            migrationBuilder.DropTable(
                name: "DivisionTable");
        }
    }
}
