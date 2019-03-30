using Microsoft.EntityFrameworkCore.Migrations;

namespace RCM.Data.Migrations
{
    public partial class FixWrongUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStageActions_AspNetUsers_UserId",
                table: "ProfileStageActions");

            migrationBuilder.DropIndex(
                name: "IX_ProfileStageActions_UserId",
                table: "ProfileStageActions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfileStageActions");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProgressStageActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStageActions_UserId",
                table: "ProgressStageActions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressStageActions_AspNetUsers_UserId",
                table: "ProgressStageActions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressStageActions_AspNetUsers_UserId",
                table: "ProgressStageActions");

            migrationBuilder.DropIndex(
                name: "IX_ProgressStageActions_UserId",
                table: "ProgressStageActions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProgressStageActions");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProfileStageActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileStageActions_UserId",
                table: "ProfileStageActions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStageActions_AspNetUsers_UserId",
                table: "ProfileStageActions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
