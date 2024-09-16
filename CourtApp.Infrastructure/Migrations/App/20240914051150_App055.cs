using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App055 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_petition_detail_m_frm_types_TemplateId",
                table: "case_petition_detail");

            migrationBuilder.AddColumn<Guid>(
                name: "DraftingFormId",
                table: "case_petition_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_case_petition_detail_DraftingFormId",
                table: "case_petition_detail",
                column: "DraftingFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_petition_detail_m_frm_types_DraftingFormId",
                table: "case_petition_detail",
                column: "DraftingFormId",
                principalTable: "m_frm_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_petition_detail_m_template_info_TemplateId",
                table: "case_petition_detail",
                column: "TemplateId",
                principalTable: "m_template_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_petition_detail_m_frm_types_DraftingFormId",
                table: "case_petition_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_petition_detail_m_template_info_TemplateId",
                table: "case_petition_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_petition_detail_DraftingFormId",
                table: "case_petition_detail");

            migrationBuilder.DropColumn(
                name: "DraftingFormId",
                table: "case_petition_detail");

            migrationBuilder.AddForeignKey(
                name: "FK_case_petition_detail_m_frm_types_TemplateId",
                table: "case_petition_detail",
                column: "TemplateId",
                principalTable: "m_frm_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
