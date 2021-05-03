using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Repositories.Migrations
{
    public partial class AddSourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "RssSource",
               columns: new[] { "Id", "Name", "Url" },
               values: new object[]
               {
                    Guid.NewGuid(),
                    "S13.ru",
                    "http://s13.ru/rss"
               });

            migrationBuilder.InsertData(
               table: "RssSource",
               columns: new[] { "Id", "Name", "Url" },
               values: new object[]
               {
                    Guid.NewGuid(),
                    "Onliner",
                    "https://www.onliner.by/feed"
               }); 

            migrationBuilder.InsertData(
                table: "RssSource",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "TUT.by",
                    "https://news.tut.by/rss/all.rss"
                }); 
            migrationBuilder.InsertData(
                table: "RssSource",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "tjournal.ru",
                    "https://tjournal.ru/rss"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
