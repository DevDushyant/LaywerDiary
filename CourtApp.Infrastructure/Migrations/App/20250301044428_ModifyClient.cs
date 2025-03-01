using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class ModifyClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "case_fee",
                schema: "account");

            migrationBuilder.AddColumn<string>(
                name: "ClientType",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Properiter",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegNo",
                schema: "ld",
                table: "client",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientType",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "Properiter",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "RegNo",
                schema: "ld",
                table: "client");

            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.CreateTable(
                name: "case_fee",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdvanceAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SettledAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_fee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_fee_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "ld",
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_case_fee_ClientId",
                schema: "account",
                table: "case_fee",
                column: "ClientId",
                unique: true);
        }
    }
}
