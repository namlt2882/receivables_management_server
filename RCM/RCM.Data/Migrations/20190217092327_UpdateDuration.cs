using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RCM.Data.Migrations
{
    public partial class UpdateDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stage",
                table: "ProgressStages");

            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "ProfileStages",
                newName: "Sequence");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Notifications",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<int>(
                name: "PayableDay",
                table: "Receivables",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClosedDay",
                table: "Receivables",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "ProgressStages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProgressStages",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "ProgressStages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ProgressStageActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "ProfileStages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProfileStages",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ProfileStageActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ProgressStages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProgressStages");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "ProgressStages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProgressStageActions");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ProfileStages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProfileStages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProfileStageActions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Sequence",
                table: "ProfileStages",
                newName: "Stage");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Notifications",
                newName: "DateCreated");

            migrationBuilder.AlterColumn<int>(
                name: "PayableDay",
                table: "Receivables",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClosedDay",
                table: "Receivables",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "ProgressStages",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Contacts",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
