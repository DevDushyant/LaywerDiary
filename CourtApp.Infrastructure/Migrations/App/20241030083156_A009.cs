using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Works",
                schema: "ld",
                table: "r_case_proceeding",
                newName: "ProcWork");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcWork",
                schema: "ld",
                table: "r_case_proceeding",
                newName: "Works");
        }
    }
}
