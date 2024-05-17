using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App018 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "r_court_bench",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "r_case_working",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "r_case_proceeding",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_nature",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_court_type",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_case_stage",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "r_court_bench");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "r_case_working");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_nature");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_court_type");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_case_stage");
        }
    }
}
