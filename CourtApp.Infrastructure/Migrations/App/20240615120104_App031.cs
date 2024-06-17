using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App031 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                schema: "ld",
                table: "m_court",
                newName: "DistrictCode");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_DistrictId",
                schema: "ld",
                table: "m_court",
                newName: "IX_m_court_DistrictCode");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictCode",
                schema: "ld",
                table: "m_court",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictCode",
                schema: "ld",
                table: "m_court");

            migrationBuilder.RenameColumn(
                name: "DistrictCode",
                schema: "ld",
                table: "m_court",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_DistrictCode",
                schema: "ld",
                table: "m_court",
                newName: "IX_m_court_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
