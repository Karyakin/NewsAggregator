using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregatorMain.Data.Migrations
{
    public partial class RemoveNameErrors_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "Сountries");

            migrationBuilder.RenameTable(
                name: "Cityes",
                newName: "Сities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Сountries",
                table: "Сountries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Сities",
                table: "Сities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Сities_CityId",
                table: "ContactDetails",
                column: "CityId",
                principalTable: "Сities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Сountries_CountryId",
                table: "ContactDetails",
                column: "CountryId",
                principalTable: "Сountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Сities_CityId",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Сountries_CountryId",
                table: "ContactDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Сountries",
                table: "Сountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Сities",
                table: "Сities");

            migrationBuilder.RenameTable(
                name: "Сountries",
                newName: "Countryes");

            migrationBuilder.RenameTable(
                name: "Сities",
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
    }
}
