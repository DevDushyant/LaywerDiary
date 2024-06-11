using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_case_detail_CaseId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropIndex(
                name: "IX_r_case_proceeding_CaseId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.CreateTable(
                name: "r_case_docs",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DOTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DOId = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_r_case_docs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "r_case_docs",
                schema: "ld");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_CaseId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_case_detail_CaseId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
