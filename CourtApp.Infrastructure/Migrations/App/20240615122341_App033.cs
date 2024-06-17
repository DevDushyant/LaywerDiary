using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App033 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_CourtDistrictId",
                schema: "ld",
                table: "m_court");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtDistrictId",
                schema: "ld",
                table: "m_court",
                column: "CourtDistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id");
        }
    }
}
