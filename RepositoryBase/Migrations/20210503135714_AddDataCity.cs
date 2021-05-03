using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Repositories.Migrations
{
    public partial class AddDataCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Сities",
               columns: new[] { "Id", "Name" },
               values: new object[]
               {
                    Guid.NewGuid(),
                    "Минск",

               });
            migrationBuilder.InsertData(
                table: "Сities",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Могилев",

                }); migrationBuilder.InsertData(
                 table: "Сities",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Витебск",

                 }); migrationBuilder.InsertData(
                 table: "Сities",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Гомель",

                 }); migrationBuilder.InsertData(
                 table: "Сities",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Брест",

                 }); migrationBuilder.InsertData(
                 table: "Сities",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Гродно",

                 }); migrationBuilder.InsertData(
                 table: "Сities",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Солигорск",

                 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
