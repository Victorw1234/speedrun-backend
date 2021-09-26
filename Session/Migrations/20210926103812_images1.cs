using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Session.Migrations
{
    public partial class images1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageName",
                value: "halo3.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageName",
                value: "sm64.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Games");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Games",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
