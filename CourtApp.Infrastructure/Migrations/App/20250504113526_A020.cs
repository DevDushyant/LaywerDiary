using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A020 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "m_state_court_language");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "m_state_court_language");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "m_state_court_language",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Languages",
                table: "m_state_court_language");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "m_state_court_language",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "m_state_court_language",
                type: "text",
                nullable: true);
        }
    }
}
