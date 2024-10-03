using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App061 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtComplexId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CourtComplexId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail_against");
        }
    }
}
