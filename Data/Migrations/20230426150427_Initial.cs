using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PojisteniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuracePersonData",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SocialNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PostZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuracePersonData", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PersonInsurance",
                columns: table => new
                {
                    PersonInsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueOfInsurance = table.Column<int>(type: "int", nullable: false),
                    IntrestOfInsurance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsuranceEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInsurance", x => x.PersonInsuranceId);
                    table.ForeignKey(
                        name: "FK_PersonInsurance_InsuracePersonData_PersonId",
                        column: x => x.PersonId,
                        principalTable: "InsuracePersonData",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonInsurance_PersonId",
                table: "PersonInsurance",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonInsurance");

            migrationBuilder.DropTable(
                name: "InsuracePersonData");
        }
    }
}
