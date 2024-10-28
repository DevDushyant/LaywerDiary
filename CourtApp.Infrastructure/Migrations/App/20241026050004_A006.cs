using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.AlterColumn<Guid>(
                name: "OppositCounselId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_ClientId",
                schema: "ld",
                table: "case_detail",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_client_ClientId",
                schema: "ld",
                table: "case_detail",
                column: "ClientId",
                principalSchema: "ld",
                principalTable: "client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client",
                column: "OppositCounselId",
                principalSchema: "ld",
                principalTable: "m_lawyer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_client_ClientId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_ClientId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AlterColumn<Guid>(
                name: "OppositCounselId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client",
                column: "OppositCounselId",
                principalSchema: "ld",
                principalTable: "m_lawyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
