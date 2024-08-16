using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App039 : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "ClientEntityId",
                schema: "account",
                table: "case_fee",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                schema: "account",
                table: "case_fee",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_case_fee_ClientEntityId",
                schema: "account",
                table: "case_fee",
                column: "ClientEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_fee_client_ClientEntityId",
                schema: "account",
                table: "case_fee",
                column: "ClientEntityId",
                principalSchema: "ld",
                principalTable: "client",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_fee_client_ClientEntityId",
                schema: "account",
                table: "case_fee");

            migrationBuilder.DropIndex(
                name: "IX_case_fee_ClientEntityId",
                schema: "account",
                table: "case_fee");

            migrationBuilder.DropColumn(
                name: "ClientEntityId",
                schema: "account",
                table: "case_fee");

            migrationBuilder.DropColumn(
                name: "ClientId",
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
