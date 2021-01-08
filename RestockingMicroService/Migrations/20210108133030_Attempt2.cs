using Microsoft.EntityFrameworkCore.Migrations;

namespace RestockingMicroService.Migrations
{
    public partial class Attempt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Restocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Restocks",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);
        }
    }
}
