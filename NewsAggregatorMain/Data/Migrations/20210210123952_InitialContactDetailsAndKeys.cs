using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregatorMain.Data.Migrations
{
    public partial class InitialContactDetailsAndKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdditionalInformation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ContactDetailsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citys_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countrys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CountryCod = table.Column<string>(nullable: true),
                    CountryPhoneCod = table.Column<string>(nullable: true),
                    ContactDetailsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countrys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countrys_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserEMail = table.Column<string>(nullable: true),
                    Activated = table.Column<DateTime>(nullable: false),
                    Deactivated = table.Column<DateTime>(nullable: true),
                    ContactDetailsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EMails_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Activated = table.Column<DateTime>(nullable: false),
                    Deactivated = table.Column<DateTime>(nullable: true),
                    ContactDetailsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citys_ContactDetailsId",
                table: "Citys",
                column: "ContactDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countrys_ContactDetailsId",
                table: "Countrys",
                column: "ContactDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMails_ContactDetailsId",
                table: "EMails",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ContactDetailsId",
                table: "Phones",
                column: "ContactDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropTable(
                name: "Countrys");

            migrationBuilder.DropTable(
                name: "EMails");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "ContactDetails");
        }
    }
}
