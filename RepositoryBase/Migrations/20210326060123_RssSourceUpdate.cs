using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class RssSourceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorRssSource_Sources_SourcesId",
                table: "AuthorRssSource");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Sources_SourceId",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sources",
                table: "Sources");

            migrationBuilder.RenameTable(
                name: "Sources",
                newName: "RssSource");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RssSource",
                table: "RssSource",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorRssSource_RssSource_SourcesId",
                table: "AuthorRssSource",
                column: "SourcesId",
                principalTable: "RssSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_RssSource_SourceId",
                table: "News",
                column: "SourceId",
                principalTable: "RssSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorRssSource_RssSource_SourcesId",
                table: "AuthorRssSource");

            migrationBuilder.DropForeignKey(
                name: "FK_News_RssSource_SourceId",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RssSource",
                table: "RssSource");

            migrationBuilder.RenameTable(
                name: "RssSource",
                newName: "Sources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sources",
                table: "Sources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorRssSource_Sources_SourcesId",
                table: "AuthorRssSource",
                column: "SourcesId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Sources_SourceId",
                table: "News",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
