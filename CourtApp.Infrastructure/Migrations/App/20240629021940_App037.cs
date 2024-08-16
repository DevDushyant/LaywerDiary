using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App037 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrollNumber",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "EnrollNumber",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "Mobile",
                schema: "ld",
                table: "m_lawyer");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "ld",
                table: "m_lawyer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "type",
                schema: "ld",
                table: "m_lawyer",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
