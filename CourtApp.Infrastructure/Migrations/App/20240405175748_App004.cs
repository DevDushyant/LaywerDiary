using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourtTypeId",
                schema: "ld",
                table: "m_c_type",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                schema: "ld",
                table: "m_c_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_CourtTypeId",
                schema: "ld",
                table: "m_c_type",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_StateId",
                schema: "ld",
                table: "m_c_type",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_c_type_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_c_type",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_c_type_m_state_StateId",
                schema: "ld",
                table: "m_c_type",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_c_type_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_c_type");

            migrationBuilder.DropForeignKey(
                name: "FK_m_c_type_m_state_StateId",
                schema: "ld",
                table: "m_c_type");

            migrationBuilder.DropIndex(
                name: "IX_m_c_type_CourtTypeId",
                schema: "ld",
                table: "m_c_type");

            migrationBuilder.DropIndex(
                name: "IX_m_c_type_StateId",
                schema: "ld",
                table: "m_c_type");

            migrationBuilder.DropColumn(
                name: "CourtTypeId",
                schema: "ld",
                table: "m_c_type");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "ld",
                table: "m_c_type");
        }
    }
}
