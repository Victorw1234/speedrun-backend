using Microsoft.EntityFrameworkCore.Migrations;

namespace Session.Migrations
{
    public partial class Tiems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameAdmins_Games_GameId",
                table: "GameAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_GameAdmins_Users_UserId",
                table: "GameAdmins");

            migrationBuilder.AddColumn<int>(
                name: "CategoryExtensionId",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Times",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GameAdmins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GameAdmins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Times_CategoryExtensionId",
                table: "Times",
                column: "CategoryExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_UserId",
                table: "Times",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameAdmins_Games_GameId",
                table: "GameAdmins",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameAdmins_Users_UserId",
                table: "GameAdmins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_CategoryExtensions_CategoryExtensionId",
                table: "Times",
                column: "CategoryExtensionId",
                principalTable: "CategoryExtensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Users_UserId",
                table: "Times",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameAdmins_Games_GameId",
                table: "GameAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_GameAdmins_Users_UserId",
                table: "GameAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_CategoryExtensions_CategoryExtensionId",
                table: "Times");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Users_UserId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_CategoryExtensionId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_UserId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "CategoryExtensionId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Times");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GameAdmins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GameAdmins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GameAdmins_Games_GameId",
                table: "GameAdmins",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameAdmins_Users_UserId",
                table: "GameAdmins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
