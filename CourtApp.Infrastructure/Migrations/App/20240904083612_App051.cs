using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App051 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_template_info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateName = table.Column<string>(type: "text", nullable: true),
                    TemplatePath = table.Column<string>(type: "text", nullable: true),
                    Tags_Capacity = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_template_info", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_CaseId",
                table: "case_petition_detail",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_TemplateId",
                table: "case_petition_detail",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_petition_detail_case_detail_CaseId",
                table: "case_petition_detail",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_petition_detail_m_frm_types_TemplateId",
                table: "case_petition_detail",
                column: "TemplateId",
                principalTable: "m_frm_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_petition_detail_case_detail_CaseId",
                table: "case_petition_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_petition_detail_m_frm_types_TemplateId",
                table: "case_petition_detail");

            migrationBuilder.DropTable(
                name: "m_template_info");

            migrationBuilder.DropIndex(
                name: "IX_case_petition_detail_CaseId",
                table: "case_petition_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_petition_detail_TemplateId",
                table: "case_petition_detail");
        }
    }
}
