using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A021 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_form_m_state_court_language_LanguageId",
                table: "m_court_form");

            migrationBuilder.AlterColumn<Guid>(
                name: "LanguageId",
                table: "m_court_form",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "m_court_form",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_form_m_state_court_language_LanguageId",
                table: "m_court_form",
                column: "LanguageId",
                principalTable: "m_state_court_language",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_form_m_state_court_language_LanguageId",
                table: "m_court_form");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "m_court_form");

            migrationBuilder.AlterColumn<Guid>(
                name: "LanguageId",
                table: "m_court_form",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_form_m_state_court_language_LanguageId",
                table: "m_court_form",
                column: "LanguageId",
                principalTable: "m_state_court_language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
