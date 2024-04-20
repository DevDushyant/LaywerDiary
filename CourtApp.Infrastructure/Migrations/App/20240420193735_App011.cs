using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_c_type_TypeOfCaseId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_case_kind_CaseKindId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_CourtId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_nature_NatureId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CaseKindId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_TypeOfCaseId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "Cadder",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CaseKindId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "TypeOfCaseId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.RenameColumn(
                name: "Year",
                schema: "ld",
                table: "case_detail_against",
                newName: "StrengthId");

            migrationBuilder.RenameColumn(
                name: "ProcOfficer",
                schema: "ld",
                table: "case_detail_against",
                newName: "OfficerName");

            migrationBuilder.RenameColumn(
                name: "Number",
                schema: "ld",
                table: "case_detail_against",
                newName: "CnrNo");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                schema: "ld",
                table: "case_detail_against",
                newName: "CisNo");

            migrationBuilder.RenameColumn(
                name: "CourtId",
                schema: "ld",
                table: "case_detail_against",
                newName: "CourtBenchId");

            migrationBuilder.RenameColumn(
                name: "CnrNumber",
                schema: "ld",
                table: "case_detail_against",
                newName: "CaseNo");

            migrationBuilder.RenameColumn(
                name: "CisNumber",
                schema: "ld",
                table: "case_detail_against",
                newName: "Cadre");

            migrationBuilder.RenameColumn(
                name: "Year",
                schema: "ld",
                table: "case_detail",
                newName: "CaseYear");

            migrationBuilder.RenameColumn(
                name: "Number",
                schema: "ld",
                table: "case_detail",
                newName: "CaseNo");

            migrationBuilder.RenameColumn(
                name: "NatureId",
                schema: "ld",
                table: "case_detail",
                newName: "CourtBenchId");

            migrationBuilder.RenameColumn(
                name: "CourtId",
                schema: "ld",
                table: "case_detail",
                newName: "CaseCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_NatureId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_CourtBenchId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_CourtId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_CaseCategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CaseId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CaseYear",
                schema: "ld",
                table: "case_detail_against",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                schema: "ld",
                table: "case_detail_against",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseDetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseTypeId",
                schema: "ld",
                table: "case_detail",
                column: "CaseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_c_type_CaseTypeId",
                schema: "ld",
                table: "case_detail",
                column: "CaseTypeId",
                principalSchema: "ld",
                principalTable: "m_c_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_nature_CaseCategoryId",
                schema: "ld",
                table: "case_detail",
                column: "CaseCategoryId",
                principalSchema: "ld",
                principalTable: "m_nature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_case_detail_against_case_detail_CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseDetailEntityId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_c_type_CaseTypeId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_nature_CaseCategoryId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_r_court_bench_CourtBenchId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_case_detail_CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_against_CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CaseTypeId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CaseId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CaseYear",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.RenameColumn(
                name: "StrengthId",
                schema: "ld",
                table: "case_detail_against",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "OfficerName",
                schema: "ld",
                table: "case_detail_against",
                newName: "ProcOfficer");

            migrationBuilder.RenameColumn(
                name: "CourtBenchId",
                schema: "ld",
                table: "case_detail_against",
                newName: "CourtId");

            migrationBuilder.RenameColumn(
                name: "CnrNo",
                schema: "ld",
                table: "case_detail_against",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "CisNo",
                schema: "ld",
                table: "case_detail_against",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CaseNo",
                schema: "ld",
                table: "case_detail_against",
                newName: "CnrNumber");

            migrationBuilder.RenameColumn(
                name: "Cadre",
                schema: "ld",
                table: "case_detail_against",
                newName: "CisNumber");

            migrationBuilder.RenameColumn(
                name: "CourtBenchId",
                schema: "ld",
                table: "case_detail",
                newName: "NatureId");

            migrationBuilder.RenameColumn(
                name: "CaseYear",
                schema: "ld",
                table: "case_detail",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "CaseNo",
                schema: "ld",
                table: "case_detail",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "CaseCategoryId",
                schema: "ld",
                table: "case_detail",
                newName: "CourtId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_CourtBenchId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_NatureId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_CaseCategoryId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_CourtId");

            migrationBuilder.AddColumn<string>(
                name: "Cadder",
                schema: "ld",
                table: "case_detail_against",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ld",
                table: "case_detail_against",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "ld",
                table: "case_detail_against",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "ld",
                table: "case_detail_against",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CaseKindId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TypeOfCaseId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseKindId",
                schema: "ld",
                table: "case_detail",
                column: "CaseKindId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_TypeOfCaseId",
                schema: "ld",
                table: "case_detail",
                column: "TypeOfCaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_c_type_TypeOfCaseId",
                schema: "ld",
                table: "case_detail",
                column: "TypeOfCaseId",
                principalSchema: "ld",
                principalTable: "m_c_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_case_kind_CaseKindId",
                schema: "ld",
                table: "case_detail",
                column: "CaseKindId",
                principalSchema: "ld",
                principalTable: "m_case_kind",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_CourtId",
                schema: "ld",
                table: "case_detail",
                column: "CourtId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_nature_NatureId",
                schema: "ld",
                table: "case_detail",
                column: "NatureId",
                principalSchema: "ld",
                principalTable: "m_nature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
