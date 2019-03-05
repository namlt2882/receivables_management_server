using Microsoft.EntityFrameworkCore.Migrations;

namespace RCM.Data.Migrations
{
    public partial class UpdateReceivablePayable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PayableDay",
                table: "Receivables",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PayableDay",
                table: "Receivables",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
