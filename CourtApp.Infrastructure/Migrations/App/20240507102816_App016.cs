using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App016 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "ld",
                table: "r_case_working",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextDate",
                schema: "ld",
                table: "case_detail",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_working_CaseId",
                schema: "ld",
                table: "r_case_working",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_working_SubWorkId",
                schema: "ld",
                table: "r_case_working",
                column: "SubWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_working_WorkId",
                schema: "ld",
                table: "r_case_working",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_working_case_detail_CaseId",
                schema: "ld",
                table: "r_case_working",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_working_m_work_master_WorkId",
                schema: "ld",
                table: "r_case_working",
                column: "WorkId",
                principalSchema: "ld",
                principalTable: "m_work_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_working_m_work_master_sub_SubWorkId",
                schema: "ld",
                table: "r_case_working",
                column: "SubWorkId",
                principalSchema: "ld",
                principalTable: "m_work_master_sub",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_working_case_detail_CaseId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_working_m_work_master_WorkId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_working_m_work_master_sub_SubWorkId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropIndex(
                name: "IX_r_case_working_CaseId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropIndex(
                name: "IX_r_case_working_SubWorkId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropIndex(
                name: "IX_r_case_working_WorkId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextDate",
                schema: "ld",
                table: "case_detail",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
