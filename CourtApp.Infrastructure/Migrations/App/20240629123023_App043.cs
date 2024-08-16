using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App043 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_lawyer_OppositCounseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_OppositCounseId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "OppositCounseId",
                schema: "ld",
                table: "client");

            migrationBuilder.CreateIndex(
                name: "IX_client_OppositCounselId",
                schema: "ld",
                table: "client",
                column: "OppositCounselId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client",
                column: "OppositCounselId",
                principalSchema: "ld",
                principalTable: "m_lawyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.AddColumn<Guid>(
                name: "OppositCounseId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_OppositCounseId",
                schema: "ld",
                table: "client",
                column: "OppositCounseId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_lawyer_OppositCounseId",
                schema: "ld",
                table: "client",
                column: "OppositCounseId",
                principalSchema: "ld",
                principalTable: "m_lawyer",
                principalColumn: "Id");
        }
    }
}
