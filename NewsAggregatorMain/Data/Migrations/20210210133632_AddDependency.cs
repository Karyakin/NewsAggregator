using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregatorMain.Data.Migrations
{
    public partial class AddDependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citys_ContactDetails_ContactDetailsId",
                table: "Citys");

            migrationBuilder.DropForeignKey(
                name: "FK_Countrys_ContactDetails_ContactDetailsId",
                table: "Countrys");

            migrationBuilder.DropIndex(
                name: "IX_Countrys_ContactDetailsId",
                table: "Countrys");

            migrationBuilder.DropIndex(
                name: "IX_Citys_ContactDetailsId",
                table: "Citys");

            migrationBuilder.DropColumn(
                name: "ContactDetailsId",
                table: "Countrys");

            migrationBuilder.DropColumn(
                name: "ContactDetailsId",
                table: "Citys");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactDetailsId",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Roles",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Photos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "ContactDetails",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "ContactDetails",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactDetailsId",
                table: "Users",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_CityId",
                table: "ContactDetails",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_CountryId",
                table: "ContactDetails",
                column: "CountryId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ContactDetails_ContactDetailsId",
                table: "Users",
                column: "ContactDetailsId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Citys_CityId",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Countrys_CountryId",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ContactDetails_ContactDetailsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ContactDetailsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_CityId",
                table: "ContactDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_CountryId",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "ContactDetailsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "ContactDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactDetailsId",
                table: "Countrys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContactDetailsId",
                table: "Citys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Countrys_ContactDetailsId",
                table: "Countrys",
                column: "ContactDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citys_ContactDetailsId",
                table: "Citys",
                column: "ContactDetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Citys_ContactDetails_ContactDetailsId",
                table: "Citys",
                column: "ContactDetailsId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Countrys_ContactDetails_ContactDetailsId",
                table: "Countrys",
                column: "ContactDetailsId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
