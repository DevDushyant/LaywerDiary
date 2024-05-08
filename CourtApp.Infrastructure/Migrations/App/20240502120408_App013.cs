using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App013 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ld",
                table: "r_case_proceeding",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "ld",
                table: "r_case_proceeding",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "ld",
                table: "r_case_proceeding",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "ld",
                table: "r_case_proceeding",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "ld",
                table: "r_case_proceeding");
        }
    }
}
