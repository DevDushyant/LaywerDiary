using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class update_register : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrollmentNo",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EnrollmentNo",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Mobile",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Telephone",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Website",
                schema: "Identity",
                table: "Users");
        }
    }
}
