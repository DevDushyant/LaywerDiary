using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtBenchId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtBenchId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail",
                column: "CourtBenchId",
                principalSchema: "ld",
                principalTable: "r_court_bench",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtBenchId",
                principalSchema: "ld",
                principalTable: "r_court_bench",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtBenchId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtBenchId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail",
                column: "CourtBenchId",
                principalSchema: "ld",
                principalTable: "r_court_bench",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
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

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtBenchId",
                principalSchema: "ld",
                principalTable: "r_court_bench",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
