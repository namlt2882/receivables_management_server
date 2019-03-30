using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RCM.Data.Migrations
{
    public partial class ProgressStageActionUserId_ReceivableExpectationCloseDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectationClosedDay",
                table: "Receivables",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStageActions_AspNetUsers_UserId",
                table: "ProfileStageActions");

            migrationBuilder.DropIndex(
                name: "IX_ProfileStageActions_UserId",
                table: "ProfileStageActions");

            migrationBuilder.DropColumn(
                name: "ExpectationClosedDay",
                table: "Receivables");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfileStageActions");
        }
    }
}
