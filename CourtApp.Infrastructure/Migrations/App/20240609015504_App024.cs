using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DOTypeId",
                schema: "ld",
                table: "r_case_docs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "ld",
                table: "r_case_docs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "ld",
                table: "r_case_docs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "ld",
                table: "r_case_docs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "ld",
                table: "r_case_docs",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "ld",
                table: "r_case_docs");

            migrationBuilder.AlterColumn<Guid>(
                name: "DOTypeId",
                schema: "ld",
                table: "r_case_docs",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
