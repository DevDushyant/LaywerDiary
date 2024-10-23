using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: true);

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
                principalColumn: "Id");
        }
    }
}
