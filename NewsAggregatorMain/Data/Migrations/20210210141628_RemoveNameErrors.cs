using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregatorMain.Data.Migrations
{
    public partial class RemoveNameErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Citys_CityId",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Countrys_CountryId",
                table: "ContactDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countrys",
                table: "Countrys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citys",
                table: "Citys");

            migrationBuilder.RenameTable(
                name: "Countrys",
                newName: "Countryes");

            migrationBuilder.RenameTable(
                name: "Citys",
                newName: "Cityes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countryes",
                table: "Countryes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cityes",
                table: "Cityes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Cityes_CityId",
                table: "ContactDetails",
                column: "CityId",
                principalTable: "Cityes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Countryes_CountryId",
                table: "ContactDetails",
                column: "CountryId",
                principalTable: "Countryes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Cityes_CityId",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Countryes_CountryId",
                table: "ContactDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countryes",
                table: "Countryes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cityes",
                table: "Cityes");

            migrationBuilder.RenameTable(
                name: "Countryes",
                newName: "Countrys");

            migrationBuilder.RenameTable(
                name: "Cityes",
                newName: "Citys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countrys",
                table: "Countrys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citys",
                table: "Citys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Citys_CityId",
                table: "ContactDetails",
                column: "CityId",
                principalTable: "Citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Countrys_CountryId",
                table: "ContactDetails",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
