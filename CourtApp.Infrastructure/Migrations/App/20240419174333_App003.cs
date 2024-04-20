using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "m_court",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "m_court",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
