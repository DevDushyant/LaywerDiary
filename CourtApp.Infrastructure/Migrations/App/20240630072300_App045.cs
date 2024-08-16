using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App045 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_case_stage_CaseStageId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkedCaseId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CaseStageId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_case_stage_CaseStageId",
                schema: "ld",
                table: "case_detail",
                column: "CaseStageId",
                principalSchema: "ld",
                principalTable: "m_case_stage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_case_stage_CaseStageId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkedCaseId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CaseStageId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_case_stage_CaseStageId",
                schema: "ld",
                table: "case_detail",
                column: "CaseStageId",
                principalSchema: "ld",
                principalTable: "m_case_stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
