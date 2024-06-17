using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App028 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_district_DistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                schema: "ld",
                table: "m_court_complex",
                newName: "DistrictCode");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_complex_DistrictId",
                schema: "ld",
                table: "m_court_complex",
                newName: "IX_m_court_complex_DistrictCode");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_complex",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.RenameColumn(
                name: "DistrictCode",
                schema: "ld",
                table: "m_court_complex",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_complex_DistrictCode",
                schema: "ld",
                table: "m_court_complex",
                newName: "IX_m_court_complex_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_district_DistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
