using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropIndex(
                name: "IX_m_work_master_sub_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropColumn(
                name: "WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.CreateIndex(
                name: "IX_m_work_master_sub_WorkId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WorkId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WorkId",
                principalSchema: "ld",
                principalTable: "m_work_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WorkId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropIndex(
                name: "IX_m_work_master_sub_WorkId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_work_master_sub_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WorkMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WorkMasterId",
                principalSchema: "ld",
                principalTable: "m_work_master",
                principalColumn: "Id");
        }
    }
}
