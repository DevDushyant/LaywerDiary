using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropIndex(
                name: "IX_r_court_bench_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropColumn(
                name: "CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.CreateIndex(
                name: "IX_r_court_bench_CourtMasterId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropIndex(
                name: "IX_r_court_bench_CourtMasterId",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.AddColumn<Guid>(
                name: "CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_r_court_bench_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_court_bench_m_court_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterEntityId",
                principalSchema: "ld",
                principalTable: "m_court",
                principalColumn: "Id");
        }
    }
}
