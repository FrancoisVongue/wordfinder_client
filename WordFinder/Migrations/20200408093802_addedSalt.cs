using Microsoft.EntityFrameworkCore.Migrations;

namespace WordFinder.Migrations
{
    public partial class addedSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }
    }
}
