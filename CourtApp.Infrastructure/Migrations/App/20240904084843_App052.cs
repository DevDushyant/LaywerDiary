using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App052 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags_Capacity",
                table: "m_template_info");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "m_template_info",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "m_template_info");

            migrationBuilder.AddColumn<int>(
                name: "Tags_Capacity",
                table: "m_template_info",
                type: "integer",
                nullable: true);
        }
    }
}
