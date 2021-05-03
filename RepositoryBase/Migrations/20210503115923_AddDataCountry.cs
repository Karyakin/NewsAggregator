using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Repositories.Migrations
{
    public partial class AddDataCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name"},
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Беларусь",
                    
                });
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    Guid.NewGuid(),
                    "Россия",

                }); migrationBuilder.InsertData(
                 table: "Countries",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Украина",

                 }); migrationBuilder.InsertData(
                 table: "Countries",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Польша",

                 }); migrationBuilder.InsertData(
                 table: "Countries",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Латвия",

                 }); migrationBuilder.InsertData(
                 table: "Countries",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "Германия",

                 }); migrationBuilder.InsertData(
                 table: "Countries",
                 columns: new[] { "Id", "Name" },
                 values: new object[]
                 {
                    Guid.NewGuid(),
                    "США",

                 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
