using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_form_m_c_type_CaseTypeId",
                table: "m_court_form");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_form_m_court_type_CourtTypeId",
                table: "m_court_form");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_form_m_nature_CaseCategoryId",
                table: "m_court_form");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_form_m_state_StateId",
                table: "m_court_form");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_court_form",
                table: "m_court_form");

            migrationBuilder.RenameTable(
                name: "m_court_form",
                newName: "m_court_case_template");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_form_StateId",
                table: "m_court_case_template",
                newName: "IX_m_court_case_template_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_form_CourtTypeId",
                table: "m_court_case_template",
                newName: "IX_m_court_case_template_CourtTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_form_CaseTypeId",
                table: "m_court_case_template",
                newName: "IX_m_court_case_template_CaseTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_form_CaseCategoryId",
                table: "m_court_case_template",
                newName: "IX_m_court_case_template_CaseCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_court_case_template",
                table: "m_court_case_template",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_case_template_m_c_type_CaseTypeId",
                table: "m_court_case_template",
                column: "CaseTypeId",
                principalSchema: "ld",
                principalTable: "m_c_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_case_template_m_court_type_CourtTypeId",
                table: "m_court_case_template",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_case_template_m_nature_CaseCategoryId",
                table: "m_court_case_template",
                column: "CaseCategoryId",
                principalSchema: "ld",
                principalTable: "m_nature",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_case_template_m_state_StateId",
                table: "m_court_case_template",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_case_template_m_c_type_CaseTypeId",
                table: "m_court_case_template");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_case_template_m_court_type_CourtTypeId",
                table: "m_court_case_template");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_case_template_m_nature_CaseCategoryId",
                table: "m_court_case_template");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_case_template_m_state_StateId",
                table: "m_court_case_template");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_court_case_template",
                table: "m_court_case_template");

            migrationBuilder.RenameTable(
                name: "m_court_case_template",
                newName: "m_court_form");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_case_template_StateId",
                table: "m_court_form",
                newName: "IX_m_court_form_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_case_template_CourtTypeId",
                table: "m_court_form",
                newName: "IX_m_court_form_CourtTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_case_template_CaseTypeId",
                table: "m_court_form",
                newName: "IX_m_court_form_CaseTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_m_court_case_template_CaseCategoryId",
                table: "m_court_form",
                newName: "IX_m_court_form_CaseCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_court_form",
                table: "m_court_form",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_form_m_c_type_CaseTypeId",
                table: "m_court_form",
                column: "CaseTypeId",
                principalSchema: "ld",
                principalTable: "m_c_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_form_m_court_type_CourtTypeId",
                table: "m_court_form",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_form_m_nature_CaseCategoryId",
                table: "m_court_form",
                column: "CaseCategoryId",
                principalSchema: "ld",
                principalTable: "m_nature",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_form_m_state_StateId",
                table: "m_court_form",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
