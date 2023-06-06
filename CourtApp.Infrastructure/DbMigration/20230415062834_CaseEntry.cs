using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class CaseEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCase",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    InstitutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LinkedClient = table.Column<int>(type: "int", nullable: false),
                    CaseNatureId = table.Column<int>(type: "int", nullable: false),
                    TypeOfCaseId = table.Column<int>(type: "int", nullable: false),
                    CourtTypeCode = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    CaseTypeId = table.Column<int>(type: "int", nullable: false),
                    CaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseYear = table.Column<int>(type: "int", nullable: false),
                    TitleFirst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstTitleType = table.Column<int>(type: "int", nullable: false),
                    TitleSecond = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondTitleType = table.Column<int>(type: "int", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseStageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseAgainstDecisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AgainstCourtTypeCode = table.Column<int>(type: "int", nullable: false),
                    AgainstCourtId = table.Column<int>(type: "int", nullable: false),
                    AgainstCaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgainstYear = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCase_Client_LinkedClient",
                        column: x => x.LinkedClient,
                        principalSchema: "LDiary",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCase_Mst_CaseNature_CaseNatureId",
                        column: x => x.CaseNatureId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CaseNature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCase_Mst_Court_AgainstCourtId",
                        column: x => x.AgainstCourtId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserCase_Mst_Court_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserCase_Mst_CourtType_AgainstCourtTypeCode",
                        column: x => x.AgainstCourtTypeCode,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CourtType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCase_Mst_CourtType_CourtTypeCode",
                        column: x => x.CourtTypeCode,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CourtType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserCase_Mst_Typeofcases_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Typeofcases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserCase_Mst_Typeofcases_TypeOfCaseId",
                        column: x => x.TypeOfCaseId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Typeofcases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_AgainstCourtId",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_AgainstCourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_CaseNatureId",
                schema: "LDiary",
                table: "UserCase",
                column: "CaseNatureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_CaseTypeId",
                schema: "LDiary",
                table: "UserCase",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_CourtId",
                schema: "LDiary",
                table: "UserCase",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_CourtTypeCode",
                schema: "LDiary",
                table: "UserCase",
                column: "CourtTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_LinkedClient",
                schema: "LDiary",
                table: "UserCase",
                column: "LinkedClient");

            migrationBuilder.CreateIndex(
                name: "IX_UserCase_TypeOfCaseId",
                schema: "LDiary",
                table: "UserCase",
                column: "TypeOfCaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCase",
                schema: "LDiary");
        }
    }
}
