using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App057 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CaseCategoryId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CaseTypeId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CaseCategoryId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CaseTypeId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtBenchId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtBenchId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtComplexId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtTypeId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_StateId",
                schema: "ld",
                table: "case_detail_against",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_c_type_CaseTypeId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseTypeId",
                principalSchema: "ld",
                principalTable: "m_c_type",
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
                name: "FK_case_detail_against_m_court_type_CourtTypeId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_nature_CaseCategoryId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseCategoryId",
                principalSchema: "ld",
                principalTable: "m_nature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_m_state_StateId",
                schema: "ld",
                table: "case_detail_against",
                column: "StateId",
                principalTable: "m_state",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_c_type_CaseTypeId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_court_type_CourtTypeId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_nature_CaseCategoryId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_m_state_StateId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CaseCategoryId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CaseTypeId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CourtBenchId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CourtComplexId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CourtTypeId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_StateId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CaseCategoryId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CaseTypeId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CourtComplexId",
                schema: "ld",
                table: "case_detail_against");
        }
    }
}
