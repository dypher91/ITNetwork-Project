using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PojisteniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsuranceModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionOfInsurance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.InsuranceId);
                    table.ForeignKey(
                        name: "FK_Insurance_InsuracePersonData_PersonId",
                        column: x => x.PersonId,
                        principalTable: "InsuracePersonData",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionOfInsurance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_PersonId",
                table: "Insurance",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "InsuranceInfo");
        }
    }
}
