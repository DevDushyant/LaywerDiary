using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App065 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CourtComplexId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CourtDistrictId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id");
        }
    }
}
