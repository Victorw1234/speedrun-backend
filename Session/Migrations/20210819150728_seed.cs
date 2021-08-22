using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Session.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryExtensions_Games_GameId",
                table: "CategoryExtensions");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "CategoryExtensions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Halo 3" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Super Mario 64" });

            migrationBuilder.InsertData(
                table: "CategoryExtensions",
                columns: new[] { "Id", "GameId", "Title" },
                values: new object[] { 1, 1, "Easy" });

            migrationBuilder.InsertData(
                table: "CategoryExtensions",
                columns: new[] { "Id", "GameId", "Title" },
                values: new object[] { 2, 1, "Legendary" });

            migrationBuilder.InsertData(
                table: "GameAdmins",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Times",
                columns: new[] { "Id", "CategoryExtensionId", "Link", "RunTime", "UserId" },
                values: new object[] { 1, 1, "https://www.youtube.com/watch?v=uhpuu6B3L8E", new DateTime(1, 1, 1, 1, 1, 57, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryExtensions_Games_GameId",
                table: "CategoryExtensions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryExtensions_Games_GameId",
                table: "CategoryExtensions");

            migrationBuilder.DeleteData(
                table: "CategoryExtensions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameAdmins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Times",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryExtensions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "CategoryExtensions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryExtensions_Games_GameId",
                table: "CategoryExtensions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
