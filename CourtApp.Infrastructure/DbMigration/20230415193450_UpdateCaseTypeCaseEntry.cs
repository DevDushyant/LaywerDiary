using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class UpdateCaseTypeCaseEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_Typeofcases_CaseTypeId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_CaseKind_CaseTypeId",
                schema: "LDiary",
                table: "UserCase",
                column: "CaseTypeId",
                principalSchema: "LDiary",
                principalTable: "Mst_CaseKind",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_CaseKind_CaseTypeId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_Typeofcases_CaseTypeId",
                schema: "LDiary",
                table: "UserCase",
                column: "CaseTypeId",
                principalSchema: "LDiary",
                principalTable: "Mst_Typeofcases",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
