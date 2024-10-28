using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_case_stage_StageId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.AlterColumn<Guid>(
                name: "StageId",
                schema: "ld",
                table: "r_case_proceeding",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_case_stage_StageId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "StageId",
                principalSchema: "ld",
                principalTable: "m_case_stage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_case_stage_StageId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.AlterColumn<Guid>(
                name: "StageId",
                schema: "ld",
                table: "r_case_proceeding",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_case_stage_StageId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "StageId",
                principalSchema: "ld",
                principalTable: "m_case_stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
