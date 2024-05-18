using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App021 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_working_m_work_master_WorkId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_working_m_work_master_sub_SubWorkId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.RenameColumn(
                name: "SubWorkId",
                schema: "ld",
                table: "r_case_working",
                newName: "WorkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_r_case_working_SubWorkId",
                schema: "ld",
                table: "r_case_working",
                newName: "IX_r_case_working_WorkTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_working_m_work_master_WorkTypeId",
                schema: "ld",
                table: "r_case_working",
                column: "WorkTypeId",
                principalSchema: "ld",
                principalTable: "m_work_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_working_m_work_master_sub_WorkId",
                schema: "ld",
                table: "r_case_working",
                column: "WorkId",
                principalSchema: "ld",
                principalTable: "m_work_master_sub",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_working_m_work_master_WorkTypeId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_working_m_work_master_sub_WorkId",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.RenameColumn(
                name: "WorkTypeId",
                schema: "ld",
                table: "r_case_working",
                newName: "SubWorkId");

            migrationBuilder.RenameIndex(
                name: "IX_r_case_working_WorkTypeId",
                schema: "ld",
                table: "r_case_working",
                newName: "IX_r_case_working_SubWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_working_m_work_master_WorkId",
                schema: "ld",
                table: "r_case_working",
                column: "WorkId",
                principalSchema: "ld",
                principalTable: "m_work_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_working_m_work_master_sub_SubWorkId",
                schema: "ld",
                table: "r_case_working",
                column: "SubWorkId",
                principalSchema: "ld",
                principalTable: "m_work_master_sub",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
