using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class UpdateKind : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourtTypeId",
                schema: "LDiary",
                table: "Mst_CaseKind",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_CaseKind_CourtTypeId",
                schema: "LDiary",
                table: "Mst_CaseKind",
                column: "CourtTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mst_CaseKind_Mst_CourtType_CourtTypeId",
                schema: "LDiary",
                table: "Mst_CaseKind",
                column: "CourtTypeId",
                principalSchema: "LDiary",
                principalTable: "Mst_CourtType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mst_CaseKind_Mst_CourtType_CourtTypeId",
                schema: "LDiary",
                table: "Mst_CaseKind");

            migrationBuilder.DropIndex(
                name: "IX_Mst_CaseKind_CourtTypeId",
                schema: "LDiary",
                table: "Mst_CaseKind");

            migrationBuilder.DropColumn(
                name: "CourtTypeId",
                schema: "LDiary",
                table: "Mst_CaseKind");
        }
    }
}
