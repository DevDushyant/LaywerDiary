using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class updatecase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LinkedCaseId",
                schema: "LDiary",
                table: "UserCase",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedCaseId",
                schema: "LDiary",
                table: "UserCase");
        }
    }
}
