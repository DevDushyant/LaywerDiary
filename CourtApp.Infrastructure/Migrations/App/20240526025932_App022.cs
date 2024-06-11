using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_do_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_do_type", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_CaseId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_HeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_StageId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_r_case_proceeding_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "SubHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_case_detail_CaseId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "CaseId",
                principalSchema: "ld",
                principalTable: "case_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_case_stage_StageId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "StageId",
                principalSchema: "ld",
                principalTable: "m_case_stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_head_HeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "HeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_sub_head_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding",
                column: "SubHeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_sub_head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_case_detail_CaseId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_case_stage_StageId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_head_HeadId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropForeignKey(
                name: "FK_r_case_proceeding_m_proceeding_sub_head_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropTable(
                name: "m_do_type",
                schema: "ld");

            migrationBuilder.DropIndex(
                name: "IX_r_case_proceeding_CaseId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropIndex(
                name: "IX_r_case_proceeding_HeadId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropIndex(
                name: "IX_r_case_proceeding_StageId",
                schema: "ld",
                table: "r_case_proceeding");

            migrationBuilder.DropIndex(
                name: "IX_r_case_proceeding_SubHeadId",
                schema: "ld",
                table: "r_case_proceeding");
        }
    }
}
