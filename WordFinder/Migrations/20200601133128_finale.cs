using Microsoft.EntityFrameworkCore.Migrations;

namespace WordFinder.Migrations
{
    public partial class finale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topics",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Topics",
                maxLength: 200000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topics",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Topics",
                maxLength: 50000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200000);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);
        }
    }
}
