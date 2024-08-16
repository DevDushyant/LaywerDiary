using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App042 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_case_detail_CaseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_CaseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "CaseId",
                schema: "ld",
                table: "client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CaseId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_client_CaseId",
                schema: "ld",
                table: "client",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_detail_CaseId",
                schema: "ld",
                table: "client",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
