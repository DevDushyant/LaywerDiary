using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class A018 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.DropColumn(
                name: "OppositCounselId",
                schema: "ld",
                table: "client");

            migrationBuilder.RenameTable(
                name: "m_lawyer",
                schema: "ld",
                newName: "m_lawyer",
                newSchema: "common");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "common",
                table: "m_lawyer",
                newName: "Relegion");

            migrationBuilder.AddColumn<string>(
                name: "Caste",
                schema: "common",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                schema: "common",
                table: "m_lawyer",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "common",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "common",
                table: "m_lawyer",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelPerson",
                schema: "common",
                table: "m_lawyer",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "specilization",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specilization", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_LinkedCaseId",
                schema: "ld",
                table: "case_detail",
                column: "LinkedCaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_case_detail_LinkedCaseId",
                schema: "ld",
                table: "case_detail",
                column: "LinkedCaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_case_detail_LinkedCaseId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropTable(
                name: "specilization",
                schema: "ld");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_LinkedCaseId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "Caste",
                schema: "common",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "Dob",
                schema: "common",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "common",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "common",
                table: "m_lawyer");

            migrationBuilder.DropColumn(
                name: "RelPerson",
                schema: "common",
                table: "m_lawyer");

            migrationBuilder.RenameTable(
                name: "m_lawyer",
                schema: "common",
                newName: "m_lawyer",
                newSchema: "ld");

            migrationBuilder.RenameColumn(
                name: "Relegion",
                schema: "ld",
                table: "m_lawyer",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "OppositCounselId",
                schema: "ld",
                table: "client",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_OppositCounselId",
                schema: "ld",
                table: "client",
                column: "OppositCounselId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_lawyer_OppositCounselId",
                schema: "ld",
                table: "client",
                column: "OppositCounselId",
                principalSchema: "ld",
                principalTable: "m_lawyer",
                principalColumn: "Id");
        }
    }
}
