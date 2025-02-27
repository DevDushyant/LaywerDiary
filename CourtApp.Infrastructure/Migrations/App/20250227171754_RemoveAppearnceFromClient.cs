using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class RemoveAppearnceFromClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_fs_title_AppearenceID",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_AppearenceID",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "AppearenceID",
                schema: "ld",
                table: "client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppearenceID",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_client_AppearenceID",
                schema: "ld",
                table: "client",
                column: "AppearenceID");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_fs_title_AppearenceID",
                schema: "ld",
                table: "client",
                column: "AppearenceID",
                principalSchema: "ld",
                principalTable: "m_fs_title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
