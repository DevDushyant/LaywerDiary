using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_form_m_state_court_language_LanguageId",
                table: "m_court_form");

            migrationBuilder.DropIndex(
                name: "IX_m_court_form_LanguageId",
                table: "m_court_form");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "m_court_form");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "m_court_form",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_form_LanguageId",
                table: "m_court_form",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_form_m_state_court_language_LanguageId",
                table: "m_court_form",
                column: "LanguageId",
                principalTable: "m_state_court_language",
                principalColumn: "Id");
        }
    }
}
