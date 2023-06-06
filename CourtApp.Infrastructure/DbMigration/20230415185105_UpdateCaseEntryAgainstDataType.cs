using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class UpdateCaseEntryAgainstDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_Court_AgainstCourtId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.AlterColumn<int>(
                name: "AgainstCourtId",
                schema: "LDiary",
                table: "UserCase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_Court_AgainstCourtId",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtId",
                principalSchema: "LDiary",
                principalTable: "Mst_Court",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCase_Mst_Court_AgainstCourtId",
                schema: "LDiary",
                table: "UserCase");

            migrationBuilder.AlterColumn<int>(
                name: "AgainstCourtId",
                schema: "LDiary",
                table: "UserCase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCase_Mst_Court_AgainstCourtId",
                schema: "LDiary",
                table: "UserCase",
                column: "AgainstCourtId",
                principalSchema: "LDiary",
                principalTable: "Mst_Court",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
