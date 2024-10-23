using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_ComplexId",
                schema: "ld",
                table: "case_detail",
                column: "ComplexId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_complex_ComplexId",
                schema: "ld",
                table: "case_detail",
                column: "ComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_complex_ComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_ComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AddColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtComplexId",
                schema: "ld",
                table: "case_detail",
                column: "CourtComplexId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id");
        }
    }
}
