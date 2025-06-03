using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class UpdateUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressInfo",
                schema: "Identity",
                table: "Users",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo",
                schema: "Identity",
                table: "Users",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalInfo",
                schema: "Identity",
                table: "Users",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkLocInfo",
                schema: "Identity",
                table: "Users",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressInfo",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfessionalInfo",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkLocInfo",
                schema: "Identity",
                table: "Users");
        }
    }
}
