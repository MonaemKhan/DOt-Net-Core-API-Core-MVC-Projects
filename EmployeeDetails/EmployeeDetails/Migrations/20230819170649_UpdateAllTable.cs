using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDetails.Migrations
{
    public partial class UpdateAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryTable",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTable", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "StateTable",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    State_CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateTable", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_StateTable_CountryTable_State_CountryId",
                        column: x => x.State_CountryId,
                        principalTable: "CountryTable",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityTable",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City_StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityTable", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_CityTable_StateTable_City_StateId",
                        column: x => x.City_StateId,
                        principalTable: "StateTable",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTable",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SSC = table.Column<bool>(type: "bit", nullable: false),
                    HSC = table.Column<bool>(type: "bit", nullable: false),
                    BSC = table.Column<bool>(type: "bit", nullable: false),
                    MSC = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    countryId = table.Column<int>(type: "int", nullable: false),
                    stateId = table.Column<int>(type: "int", nullable: false),
                    cityId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTable", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeTable_CityTable_cityId",
                        column: x => x.cityId,
                        principalTable: "CityTable",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmployeeTable_CountryTable_countryId",
                        column: x => x.countryId,
                        principalTable: "CountryTable",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmployeeTable_StateTable_stateId",
                        column: x => x.stateId,
                        principalTable: "StateTable",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityTable_City_StateId",
                table: "CityTable",
                column: "City_StateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTable_cityId",
                table: "EmployeeTable",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTable_countryId",
                table: "EmployeeTable",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTable_stateId",
                table: "EmployeeTable",
                column: "stateId");

            migrationBuilder.CreateIndex(
                name: "IX_StateTable_State_CountryId",
                table: "StateTable",
                column: "State_CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTable");

            migrationBuilder.DropTable(
                name: "CityTable");

            migrationBuilder.DropTable(
                name: "StateTable");

            migrationBuilder.DropTable(
                name: "CountryTable");
        }
    }
}
