using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.AddColumn<string>(
                name: "Works",
                schema: "ld",
                table: "r_case_proceeding",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Works",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "r_case_proceeding",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "ld",
                table: "r_case_proceeding",
                type: "text",
                nullable: true);
        }
    }
}
