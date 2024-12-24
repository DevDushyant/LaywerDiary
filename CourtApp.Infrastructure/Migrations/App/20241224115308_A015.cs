using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A015 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_cadre",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_cadre", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_fs_title_AppearenceID",
                schema: "ld",
                table: "client");

            migrationBuilder.DropTable(
                name: "m_cadre",
                schema: "ld");

            migrationBuilder.DropIndex(
                name: "IX_client_AppearenceID",
                schema: "ld",
                table: "client");
        }
    }
}
