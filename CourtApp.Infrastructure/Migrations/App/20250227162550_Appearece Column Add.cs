using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class AppeareceColumnAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppearenceID",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_AppearenceID",
                schema: "ld",
                table: "case_detail",
                column: "AppearenceID");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_fs_title_AppearenceID",
                schema: "ld",
                table: "case_detail",
                column: "AppearenceID",
                principalSchema: "ld",
                principalTable: "m_fs_title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_fs_title_AppearenceID",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_AppearenceID",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "AppearenceID",
                schema: "ld",
                table: "case_detail");
        }
    }
}
