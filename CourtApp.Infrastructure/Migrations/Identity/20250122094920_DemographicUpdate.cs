using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class DemographicUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DemographicId",
                schema: "Identity",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                schema: "Identity",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "demographic",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    ContactInfo = table.Column<string>(type: "jsonb", nullable: true),
                    AddressInfo = table.Column<string>(type: "jsonb", nullable: true),
                    WorkLocInfo = table.Column<string>(type: "jsonb", nullable: true),
                    ProfessionalInfo = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demographic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "operator",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LawyerId = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operator_Users_LawyerId",
                        column: x => x.LawyerId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DemographicId",
                schema: "Identity",
                table: "Users",
                column: "DemographicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operator_LawyerId",
                schema: "Identity",
                table: "operator",
                column: "LawyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_demographic_DemographicId",
                schema: "Identity",
                table: "Users",
                column: "DemographicId",
                principalSchema: "Identity",
                principalTable: "demographic",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_demographic_DemographicId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropTable(
                name: "demographic",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "operator",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Users_DemographicId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DemographicId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                schema: "Identity",
                table: "Users");
        }
    }
}
