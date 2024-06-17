using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App030 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropIndex(
                name: "IX_r_court_bench_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropColumn(
                name: "CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench");

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

            migrationBuilder.CreateIndex(
                name: "IX_r_court_bench_CourtMasterId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterId");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropIndex(
                name: "IX_r_court_bench_CourtMasterId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropIndex(
                name: "IX_m_court_CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.AddColumn<Guid>(
                name: "CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench",
                type: "uuid",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_r_court_bench_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterEntityId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id");
        }
    }
}
