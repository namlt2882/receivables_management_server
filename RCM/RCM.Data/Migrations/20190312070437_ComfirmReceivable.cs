using Microsoft.EntityFrameworkCore.Migrations;

namespace RCM.Data.Migrations
{
    public partial class ComfirmReceivable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Receivables",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Receivables");
        }
    }
}
