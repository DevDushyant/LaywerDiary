using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App041 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_case_fee_CaseFeeId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_CaseFeeId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "CaseFeeId",
                schema: "ld",
                table: "client");

            migrationBuilder.CreateIndex(
                name: "IX_case_fee_ClientId",
                schema: "account",
                table: "case_fee",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_case_fee_client_ClientId",
                schema: "account",
                table: "case_fee",
                column: "ClientId",
                principalSchema: "ld",
                principalTable: "client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_fee_client_ClientId",
                schema: "account",
                table: "case_fee");

            migrationBuilder.DropIndex(
                name: "IX_case_fee_ClientId",
                schema: "account",
                table: "case_fee");

            migrationBuilder.AddColumn<Guid>(
                name: "CaseFeeId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_CaseFeeId",
                schema: "ld",
                table: "client",
                column: "CaseFeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_case_fee_CaseFeeId",
                schema: "ld",
                table: "client",
                column: "CaseFeeId",
                principalSchema: "account",
                principalTable: "case_fee",
                principalColumn: "Id");
        }
    }
}
