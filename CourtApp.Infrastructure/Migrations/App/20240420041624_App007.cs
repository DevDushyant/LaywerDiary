using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropIndex(
                name: "IX_m_proceeding_sub_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropColumn(
                name: "ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.RenameColumn(
                name: "PHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                newName: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_sub_head_HeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "HeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_HeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "HeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_HeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropIndex(
                name: "IX_m_proceeding_sub_head_HeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.RenameColumn(
                name: "HeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                newName: "PHeadId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_sub_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "ProceedingHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "ProceedingHeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_head",
                principalColumn: "Id");
        }
    }
}
