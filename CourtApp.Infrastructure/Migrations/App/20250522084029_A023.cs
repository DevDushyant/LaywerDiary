using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_court_form",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    LanguageCode = table.Column<string>(type: "text", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    FormName = table.Column<string>(type: "text", nullable: true),
                    FormTemplate = table.Column<string>(type: "text", nullable: true),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_form", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_form_m_c_type_CaseTypeId",
                        column: x => x.CaseTypeId,                        
                        principalSchema: "ld",
                        principalTable: "m_c_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_form_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_form_m_nature_CaseCategoryId",
                        column: x => x.CaseCategoryId,
                        principalSchema: "ld",
                        principalTable: "m_nature",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_form_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_court_form_CaseCategoryId",
                table: "m_court_form",
                column: "CaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_form_CaseTypeId",
                table: "m_court_form",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_form_CourtTypeId",
                table: "m_court_form",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_form_StateId",
                table: "m_court_form",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_court_form");
        }
    }
}
