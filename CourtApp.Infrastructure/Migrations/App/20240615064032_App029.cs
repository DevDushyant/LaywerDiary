using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App029 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_court_district_CDistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.DropIndex(
                name: "IX_m_court_complex_CDistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.DropColumn(
                name: "CDistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_CourtDistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "CourtDistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court_complex",
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
                name: "FK_m_court_complex_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.DropIndex(
                name: "IX_m_court_complex_CourtDistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.AddColumn<Guid>(
                name: "CDistrictId",
                schema: "ld",
                table: "m_court_complex",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_CDistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "CDistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_court_district_CDistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "CDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id");
        }
    }
}
