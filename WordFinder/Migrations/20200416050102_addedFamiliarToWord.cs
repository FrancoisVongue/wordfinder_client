using Microsoft.EntityFrameworkCore.Migrations;

namespace WordFinder.Migrations
{
    public partial class addedFamiliarToWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Familiar",
                table: "Words",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Familiar",
                table: "Words");
        }
    }
}
