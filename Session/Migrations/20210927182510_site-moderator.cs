using Microsoft.EntityFrameworkCore.Migrations;

namespace Session.Migrations
{
    public partial class sitemoderator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SiteModerator",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteModerator",
                table: "Users");
        }
    }
}
