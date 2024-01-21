using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trade_Test_Web.Migrations
{
    public partial class Addeddatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDateTime",
                table: "TblCharacters",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "TblCharacters");
        }
    }
}
