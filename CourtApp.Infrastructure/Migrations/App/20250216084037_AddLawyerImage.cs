using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class AddLawyerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "common",
                table: "m_lawyer");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImgPath",
                schema: "common",
                table: "m_lawyer",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImgPath",
                schema: "common",
                table: "m_lawyer");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "common",
                table: "m_lawyer",
                type: "bytea",
                nullable: true);
        }
    }
}
