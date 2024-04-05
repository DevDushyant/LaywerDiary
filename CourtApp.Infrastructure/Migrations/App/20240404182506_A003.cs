using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourtTypeId",
                schema: "ld",
                table: "m_nature",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                schema: "ld",
                table: "m_nature",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_m_nature_CourtTypeId",
                schema: "ld",
                table: "m_nature",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_nature_StateId",
                schema: "ld",
                table: "m_nature",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_nature_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_nature",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_nature_m_state_StateId",
                schema: "ld",
                table: "m_nature",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_nature_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_nature");

            migrationBuilder.DropForeignKey(
                name: "FK_m_nature_m_state_StateId",
                schema: "ld",
                table: "m_nature");

            migrationBuilder.DropIndex(
                name: "IX_m_nature_CourtTypeId",
                schema: "ld",
                table: "m_nature");

            migrationBuilder.DropIndex(
                name: "IX_m_nature_StateId",
                schema: "ld",
                table: "m_nature");

            migrationBuilder.DropColumn(
                name: "CourtTypeId",
                schema: "ld",
                table: "m_nature");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "ld",
                table: "m_nature");
        }
    }
}
