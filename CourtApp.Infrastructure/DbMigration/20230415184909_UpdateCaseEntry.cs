using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class UpdateCaseEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_CourtType_AgainstCourtTypeCode",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_CourtType_CourtTypeCode",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.DropIndex(
                name: "IX_UserCase_AgainstCourtTypeCode",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.DropColumn(
                name: "AgainstCourtTypeCode",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.RenameColumn(
                name: "CourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                newName: "CourtTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCase_CourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                newName: "IX_UserCase_CourtTypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextDate",
                schema: "LDiary",
                table: "UserCase",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CaseStageCode",
                schema: "LDiary",
                table: "UserCase",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CaseAgainstDecisionDate",
                schema: "LDiary",
                table: "UserCase",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AgainstYear",
                schema: "LDiary",
                table: "UserCase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AgainstCourtTypeId",
                schema: "LDiary",
                table: "UserCase",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_AgainstCourtTypeId",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_CourtType_AgainstCourtTypeId",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtTypeId",
                principalSchema: "LDiary",
                principalTable: "Mst_CourtType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_CourtType_CourtTypeId",
                schema: "LDiary",
                table: "UserCase",
                column: "CourtTypeId",
                principalSchema: "LDiary",
                principalTable: "Mst_CourtType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_CourtType_AgainstCourtTypeId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_CourtType_CourtTypeId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.DropIndex(
                name: "IX_UserCase_AgainstCourtTypeId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.DropColumn(
                name: "AgainstCourtTypeId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.RenameColumn(
                name: "CourtTypeId",
                schema: "LDiary",
                table: "UserCase",
                newName: "CourtTypeCode");

            migrationBuilder.RenameIndex(
                name: "IX_UserCase_CourtTypeId",
                schema: "LDiary",
                table: "UserCase",
                newName: "IX_UserCase_CourtTypeCode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextDate",
                schema: "LDiary",
                table: "UserCase",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CaseStageCode",
                schema: "LDiary",
                table: "UserCase",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CaseAgainstDecisionDate",
                schema: "LDiary",
                table: "UserCase",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgainstYear",
                schema: "LDiary",
                table: "UserCase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgainstCourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_AgainstCourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_CourtType_AgainstCourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtTypeCode",
                principalSchema: "LDiary",
                principalTable: "Mst_CourtType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_CourtType_CourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                column: "CourtTypeCode",
                principalSchema: "LDiary",
                principalTable: "Mst_CourtType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
