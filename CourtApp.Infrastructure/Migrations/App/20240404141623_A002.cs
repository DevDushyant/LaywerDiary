using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_c_type",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CaseCategoryId",
                schema: "ld",
                table: "m_c_type",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_c_type");

            migrationBuilder.DropColumn(
                name: "CaseCategoryId",
                schema: "ld",
                table: "m_c_type");
        }
    }
}
