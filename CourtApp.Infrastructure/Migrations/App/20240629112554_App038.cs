using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App038 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_district_DistrictCode",
                schema: "ld",
                table: "client");

            migrationBuilder.DropForeignKey(
                name: "FK_client_m_state_StateCode",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_DistrictCode",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_StateCode",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "Dob",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "FatherName",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "IsRural",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "StateCode",
                schema: "ld",
                table: "client");

            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                schema: "ld",
                table: "client",
                newName: "ReferalBy");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "ld",
                table: "client",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Landmark",
                schema: "ld",
                table: "client",
                newName: "Address");

            migrationBuilder.AddColumn<Guid>(
                name: "AppearenceID",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CaseFeeId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CaseId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OppositCounseId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OppositCounselId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "case_fee",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    SettledAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AdvanceAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_fee", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_CaseFeeId",
                schema: "ld",
                table: "client",
                column: "CaseFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_client_CaseId",
                schema: "ld",
                table: "client",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_client_OppositCounseId",
                schema: "ld",
                table: "client",
                column: "OppositCounseId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_detail_CaseId",
                schema: "ld",
                table: "client",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_fee_CaseFeeId",
                schema: "ld",
                table: "client",
                column: "CaseFeeId",
                principalSchema: "account",
                principalTable: "case_fee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_lawyer_OppositCounseId",
                schema: "ld",
                table: "client",
                column: "OppositCounseId",
                principalSchema: "ld",
                principalTable: "m_lawyer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_case_detail_CaseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_fee_CaseFeeId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropForeignKey(
                name: "FK_client_m_lawyer_OppositCounseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropTable(
                name: "case_fee",
                schema: "account");

            migrationBuilder.DropIndex(
                name: "IX_client_CaseFeeId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_CaseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_OppositCounseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "AppearenceID",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "CaseFeeId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "CaseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "OppositCounseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.RenameColumn(
                name: "ReferalBy",
                schema: "ld",
                table: "client",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "ld",
                table: "client",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "ld",
                table: "client",
                newName: "Landmark");

            migrationBuilder.AddColumn<int>(
                name: "DistrictCode",
                schema: "ld",
                table: "client",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dob",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRural",
                schema: "ld",
                table: "client",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StateCode",
                schema: "ld",
                table: "client",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_DistrictCode",
                schema: "ld",
                table: "client",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_client_StateCode",
                schema: "ld",
                table: "client",
                column: "StateCode");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_district_DistrictCode",
                schema: "ld",
                table: "client",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_state_StateCode",
                schema: "ld",
                table: "client",
                column: "StateCode",
                principalTable: "m_state",
                principalColumn: "Id");
        }
    }
}
