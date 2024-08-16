using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App036 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseId",
                schema: "ld",
                table: "case_detail_against");

            migrationBuilder.DropColumn(
                name: "CaseStageCode",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "FirstTitleCode",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "SecoundTitleCode",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AddColumn<Guid>(
                name: "CaseStageId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FTitleId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "STitleId",
                schema: "ld",
                table: "case_detail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "m_fs_title",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_fs_title", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseStageId",
                schema: "ld",
                table: "case_detail",
                column: "CaseStageId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_FTitleId",
                schema: "ld",
                table: "case_detail",
                column: "FTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_STitleId",
                schema: "ld",
                table: "case_detail",
                column: "STitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_case_stage_CaseStageId",
                schema: "ld",
                table: "case_detail",
                column: "CaseStageId",
                principalSchema: "ld",
                principalTable: "m_case_stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_fs_title_FTitleId",
                schema: "ld",
                table: "case_detail",
                column: "FTitleId",
                principalSchema: "ld",
                principalTable: "m_fs_title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_case_detail_m_fs_title_STitleId",
                schema: "ld",
                table: "case_detail",
                column: "STitleId",
                principalSchema: "ld",
                principalTable: "m_fs_title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_case_stage_CaseStageId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_fs_title_FTitleId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_case_detail_m_fs_title_STitleId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropTable(
                name: "m_fs_title",
                schema: "ld");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_CaseStageId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_FTitleId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropIndex(
                name: "IX_case_detail_STitleId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "CaseStageId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "FTitleId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.DropColumn(
                name: "STitleId",
                schema: "ld",
                table: "case_detail");

            migrationBuilder.AddColumn<Guid>(
                name: "CaseId",
                schema: "ld",
                table: "case_detail_against",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CaseStageCode",
                schema: "ld",
                table: "case_detail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstTitleCode",
                schema: "ld",
                table: "case_detail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecoundTitleCode",
                schema: "ld",
                table: "case_detail",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
