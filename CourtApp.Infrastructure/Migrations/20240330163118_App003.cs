using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class App003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_case_client_ClientId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_m_c_type_TypeCaseId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_m_case_kind_CaseTypeId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_m_court_AgainstCourtId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_m_court_CourtId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_m_court_type_AgainstCourtTypeId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_m_court_type_CourtTypeId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropForeignKey(
                name: "FK_client_case_m_nature_NatureId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropPrimaryKey(
                name: "PK_client_case",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropIndex(
                name: "IX_client_case_AgainstCourtId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropIndex(
                name: "IX_client_case_AgainstCourtTypeId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropColumn(
                name: "AgainstCaseNumber",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropColumn(
                name: "AgainstCourtId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropColumn(
                name: "AgainstCourtTypeId",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropColumn(
                name: "AgainstYear",
                schema: "ld",
                table: "client_case");

            migrationBuilder.DropColumn(
                name: "CaseAgainstDecisionDate",
                schema: "ld",
                table: "client_case");

            migrationBuilder.RenameTable(
                name: "client_case",
                schema: "ld",
                newName: "case_detail",
                newSchema: "ld");

            migrationBuilder.RenameIndex(
                name: "IX_client_case_TypeCaseId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_TypeCaseId");

            migrationBuilder.RenameIndex(
                name: "IX_client_case_NatureId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_NatureId");

            migrationBuilder.RenameIndex(
                name: "IX_client_case_CourtTypeId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_CourtTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_client_case_CourtId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_CourtId");

            migrationBuilder.RenameIndex(
                name: "IX_client_case_ClientId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_client_case_CaseTypeId",
                schema: "ld",
                table: "case_detail",
                newName: "IX_case_detail_CaseTypeId");

            migrationBuilder.AddColumn<int>(
                name: "CisNumber",
                schema: "ld",
                table: "case_detail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CisYear",
                schema: "ld",
                table: "case_detail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CnrNumber",
                schema: "ld",
                table: "case_detail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextDate",
                schema: "ld",
                table: "case_detail",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_case_detail",
                schema: "ld",
                table: "case_detail",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "case_detail_against",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImpugedOrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CisNumber = table.Column<int>(type: "integer", nullable: false),
                    CisYear = table.Column<int>(type: "integer", nullable: false),
                    CnrNumber = table.Column<int>(type: "integer", nullable: false),
                    ProcOfficer = table.Column<string>(type: "text", nullable: true),
                    Cadder = table.Column<string>(type: "text", nullable: true),
                    CaseEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_detail_against", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_detail_against_case_detail_CaseEntityId",
                        column: x => x.CaseEntityId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_court_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "ld",
                        principalTable: "m_court",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_against_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CaseEntityId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_against_CourtTypeId",
                schema: "ld",
                table: "case_detail_against",
                column: "CourtTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_client_ClientId",
                schema: "ld",
                table: "case_detail",
                column: "ClientId",
                principalSchema: "ld",
                principalTable: "client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_c_type_TypeCaseId",
                schema: "ld",
                table: "case_detail",
                column: "TypeCaseId",
                principalSchema: "ld",
                principalTable: "m_c_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_case_kind_CaseTypeId",
                schema: "ld",
                table: "case_detail",
                column: "CaseTypeId",
                principalSchema: "ld",
                principalTable: "m_case_kind",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_CourtId",
                schema: "ld",
                table: "case_detail",
                column: "CourtId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_court_type_CourtTypeId",
                schema: "ld",
                table: "case_detail",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_nature_NatureId",
                schema: "ld",
                table: "case_detail",
                column: "NatureId",
                principalSchema: "ld",
                principalTable: "m_nature",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_client_ClientId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_c_type_TypeCaseId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_case_kind_CaseTypeId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_CourtId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_court_type_CourtTypeId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_nature_NatureId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropTable(
                name: "case_detail_against",
                schema: "ld");

            migrationBuilder.DropPrimaryKey(
                name: "PK_case_detail",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CisNumber",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CisYear",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CnrNumber",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "NextDate",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.RenameTable(
                name: "case_detail",
                schema: "ld",
                newName: "client_case",
                newSchema: "ld");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_TypeCaseId",
                schema: "ld",
                table: "client_case",
                newName: "IX_client_case_TypeCaseId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_NatureId",
                schema: "ld",
                table: "client_case",
                newName: "IX_client_case_NatureId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_CourtTypeId",
                schema: "ld",
                table: "client_case",
                newName: "IX_client_case_CourtTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_CourtId",
                schema: "ld",
                table: "client_case",
                newName: "IX_client_case_CourtId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_ClientId",
                schema: "ld",
                table: "client_case",
                newName: "IX_client_case_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_CaseTypeId",
                schema: "ld",
                table: "client_case",
                newName: "IX_client_case_CaseTypeId");

            migrationBuilder.AddColumn<string>(
                name: "AgainstCaseNumber",
                schema: "ld",
                table: "client_case",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgainstCourtId",
                schema: "ld",
                table: "client_case",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgainstCourtTypeId",
                schema: "ld",
                table: "client_case",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgainstYear",
                schema: "ld",
                table: "client_case",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CaseAgainstDecisionDate",
                schema: "ld",
                table: "client_case",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_client_case",
                schema: "ld",
                table: "client_case",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_AgainstCourtId",
                schema: "ld",
                table: "client_case",
                column: "AgainstCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_AgainstCourtTypeId",
                schema: "ld",
                table: "client_case",
                column: "AgainstCourtTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_client_ClientId",
                schema: "ld",
                table: "client_case",
                column: "ClientId",
                principalSchema: "ld",
                principalTable: "client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_m_c_type_TypeCaseId",
                schema: "ld",
                table: "client_case",
                column: "TypeCaseId",
                principalSchema: "ld",
                principalTable: "m_c_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_m_case_kind_CaseTypeId",
                schema: "ld",
                table: "client_case",
                column: "CaseTypeId",
                principalSchema: "ld",
                principalTable: "m_case_kind",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_m_court_AgainstCourtId",
                schema: "ld",
                table: "client_case",
                column: "AgainstCourtId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_m_court_CourtId",
                schema: "ld",
                table: "client_case",
                column: "CourtId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_m_court_type_AgainstCourtTypeId",
                schema: "ld",
                table: "client_case",
                column: "AgainstCourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_m_court_type_CourtTypeId",
                schema: "ld",
                table: "client_case",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_m_nature_NatureId",
                schema: "ld",
                table: "client_case",
                column: "NatureId",
                principalSchema: "ld",
                principalTable: "m_nature",
                principalColumn: "Id");
        }
    }
}
