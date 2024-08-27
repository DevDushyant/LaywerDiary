using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App047 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NextDate",
                schema: "ld",
                table: "r_case_proceeding",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                schema: "ld",
                table: "case_detail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_CaseId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_StateId",
                schema: "ld",
                table: "case_detail",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_state_StateId",
                schema: "ld",
                table: "case_detail",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_case_detail_CaseId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_state_StateId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_case_detail_CaseId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropIndex(
                name: "IX_r_case_proceeding_CaseId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_StateId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextDate",
                schema: "ld",
                table: "r_case_proceeding",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
