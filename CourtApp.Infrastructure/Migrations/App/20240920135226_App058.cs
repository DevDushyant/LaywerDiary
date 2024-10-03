using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App058 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_case_detail_CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.RenameColumn(
                name: "CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against",
                newName: "CaseIdId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_against_CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against",
                newName: "IX_case_detail_against_CaseIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_case_detail_CaseIdId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseIdId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_against_case_detail_CaseIdId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.RenameColumn(
                name: "CaseIdId",
                schema: "ld",
                table: "case_detail_against",
                newName: "CaseDetailEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_case_detail_against_CaseIdId",
                schema: "ld",
                table: "case_detail_against",
                newName: "IX_case_detail_against_CaseDetailEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_against_case_detail_CaseDetailEntityId",
                schema: "ld",
                table: "case_detail_against",
                column: "CaseDetailEntityId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id");
        }
    }
}
