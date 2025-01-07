using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A016 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cadre",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.AddColumn<Guid>(
                name: "CadreId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CadreId",
                schema: "ld",
                table: "case_detail_against",
                column: "CadreId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_cadre_CadreId",
                schema: "ld",
                table: "case_detail_against",
                column: "CadreId",
                principalSchema: "ld",
                principalTable: "m_cadre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_cadre_CadreId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CadreId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CadreId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.AddColumn<string>(
                name: "Cadre",
                schema: "ld",
                table: "case_detail_against",
                type: "text",
                nullable: true);
        }
    }
}
