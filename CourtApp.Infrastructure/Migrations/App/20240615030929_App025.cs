using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                schema: "ld",
                table: "m_court_district",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_r_case_docs_DOId",
                schema: "ld",
                table: "r_case_docs",
                column: "DOId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_docs_m_do_type_DOId",
                schema: "ld",
                table: "r_case_docs",
                column: "DOId",
                principalSchema: "ld",
                principalTable: "m_do_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_docs_m_do_type_DOId",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropIndex(
                name: "IX_r_case_docs_DOId",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "ld",
                table: "m_court_district");
        }
    }
}
