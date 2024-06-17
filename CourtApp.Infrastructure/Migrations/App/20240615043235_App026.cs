using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_DistrictCode",
                schema: "ld",
                table: "m_court_district",
                column: "DistrictCode");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_district_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_district",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_district_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_district");

            migrationBuilder.DropIndex(
                name: "IX_m_court_district_DistrictCode",
                schema: "ld",
                table: "m_court_district");
        }
    }
}
