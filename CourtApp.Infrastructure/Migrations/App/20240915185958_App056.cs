using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App056 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "ld",
                table: "case_titles");

            migrationBuilder.AddColumn<string>(
                name: "CaseApplicants",
                schema: "ld",
                table: "case_titles",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseApplicants",
                schema: "ld",
                table: "case_titles");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "ld",
                table: "case_titles",
                type: "text",
                nullable: true);
        }
    }
}
