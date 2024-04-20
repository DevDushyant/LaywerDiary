using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_state_StateId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "StateCode",
                schema: "ld",
                table: "m_court");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_state_StateId",
                schema: "ld",
                table: "m_court",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_state_StateId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DistrictCode",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateCode",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_state_StateId",
                schema: "ld",
                table: "m_court",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id");
        }
    }
}
