using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App062 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtComplexId",
                schema: "ld",
                table: "case_detail",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtDistrictId",
                schema: "ld",
                table: "case_detail",
                column: "CourtDistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CourtDistrictId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail");
        }
    }
}
