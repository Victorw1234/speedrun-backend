using Microsoft.EntityFrameworkCore.Migrations;

namespace Session.Migrations
{
    public partial class GameAndCategoryRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "CategoryExtensions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExtensions_GameId",
                table: "CategoryExtensions",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryExtensions_Games_GameId",
                table: "CategoryExtensions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryExtensions_Games_GameId",
                table: "CategoryExtensions");

            migrationBuilder.DropIndex(
                name: "IX_CategoryExtensions_GameId",
                table: "CategoryExtensions");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "CategoryExtensions");
        }
    }
}
