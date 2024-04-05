using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_PHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropIndex(
                name: "IX_m_work_master_sub_WMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropIndex(
                name: "IX_m_proceeding_sub_head_PHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.RenameColumn(
                name: "WMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                newName: "WorkId");

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                schema: "ld",
                table: "m_work_master_sub",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_work_master_sub",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Work_En",
                schema: "ld",
                table: "m_work_master",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_work_master",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                schema: "ld",
                table: "m_proceeding_sub_head",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_proceeding_sub_head",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                schema: "ld",
                table: "m_proceeding_head",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "ld",
                table: "m_proceeding_head",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "case_titles",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_titles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_titles_case_detail_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "ld",
                        principalTable: "case_detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_district",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    StateId1 = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_district", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_district_m_district_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "m_district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_district_m_state_StateId1",
                        column: x => x.StateId1,
                        principalTable: "m_state",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_court_complex",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    CourtDistrict = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    StateId1 = table.Column<int>(type: "integer", nullable: true),
                    CDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_complex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_court_district_CDistrictId",
                        column: x => x.CDistrictId,
                        principalSchema: "ld",
                        principalTable: "m_court_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_district_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "m_district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_court_complex_m_state_StateId1",
                        column: x => x.StateId1,
                        principalTable: "m_state",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_work_master_sub_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WorkMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_sub_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "ProceedingHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_case_titles_CaseId",
                schema: "ld",
                table: "case_titles",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_CDistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "CDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_DistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_complex_StateId1",
                schema: "ld",
                table: "m_court_complex",
                column: "StateId1");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_DistrictId",
                schema: "ld",
                table: "m_court_district",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_StateId1",
                schema: "ld",
                table: "m_court_district",
                column: "StateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "ProceedingHeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_head",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WorkMasterId",
                principalSchema: "ld",
                principalTable: "m_work_master",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropTable(
                name: "case_titles",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_complex",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_district",
                schema: "ld");

            migrationBuilder.DropIndex(
                name: "IX_m_work_master_sub_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropIndex(
                name: "IX_m_proceeding_sub_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropColumn(
                name: "WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_work_master");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropColumn(
                name: "ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "ld",
                table: "m_proceeding_head");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                schema: "ld",
                table: "m_work_master_sub",
                newName: "WMasterId");

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                schema: "ld",
                table: "m_work_master_sub",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Work_En",
                schema: "ld",
                table: "m_work_master",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                schema: "ld",
                table: "m_proceeding_sub_head",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                schema: "ld",
                table: "m_proceeding_head",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_work_master_sub_WMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_sub_head_PHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "PHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_proceeding_sub_head_m_proceeding_head_PHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "PHeadId",
                principalSchema: "ld",
                principalTable: "m_proceeding_head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_work_master_sub_m_work_master_WMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WMasterId",
                principalSchema: "ld",
                principalTable: "m_work_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
