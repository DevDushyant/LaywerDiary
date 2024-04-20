using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictCode",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_state_StateCode",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_DistrictCode",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_StateCode",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "Bench",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "HeadQuerter",
                schema: "ld",
                table: "m_court");

            migrationBuilder.AlterColumn<int>(
                name: "StateCode",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictCode",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtTypeId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourtComplexId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourtDistrictId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "r_court_bench",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtBench_En = table.Column<string>(type: "text", nullable: true),
                    CourtBench_Hn = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CourtMasterEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_r_court_bench", x => x.Id);
                    table.ForeignKey(
                        name: "FK_r_court_bench_m_court_CourtMasterEntityId",
                        column: x => x.CourtMasterEntityId,
                        principalSchema: "ld",
                        principalTable: "m_court",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtComplexId",
                schema: "ld",
                table: "m_court",
                column: "CourtComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtDistrictId",
                schema: "ld",
                table: "m_court",
                column: "CourtDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_DistrictId",
                schema: "ld",
                table: "m_court",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_StateId",
                schema: "ld",
                table: "m_court",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_r_court_bench_CourtMasterEntityId",
                schema: "ld",
                table: "r_court_bench",
                column: "CourtMasterEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "m_court",
                column: "CourtComplexId",
                principalSchema: "ld",
                principalTable: "m_court_complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court",
                column: "CourtDistrictId",
                principalSchema: "ld",
                principalTable: "m_court_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_court",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_state_StateId",
                schema: "ld",
                table: "m_court",
                column: "StateId",
                principalTable: "m_state",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_complex_CourtComplexId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_district_CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_state_StateId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropTable(
                name: "r_court_bench",
                schema: "ld");

            migrationBuilder.DropIndex(
                name: "IX_m_court_CourtComplexId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropIndex(
                name: "IX_m_court_StateId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "CourtComplexId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "CourtDistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.AlterColumn<int>(
                name: "StateCode",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictCode",
                schema: "ld",
                table: "m_court",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtTypeId",
                schema: "ld",
                table: "m_court",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "ld",
                table: "m_court",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bench",
                schema: "ld",
                table: "m_court",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadQuerter",
                schema: "ld",
                table: "m_court",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_court_DistrictCode",
                schema: "ld",
                table: "m_court",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_StateCode",
                schema: "ld",
                table: "m_court",
                column: "StateCode");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_court_type_CourtTypeId",
                schema: "ld",
                table: "m_court",
                column: "CourtTypeId",
                principalSchema: "ld",
                principalTable: "m_court_type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictCode",
                schema: "ld",
                table: "m_court",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_state_StateCode",
                schema: "ld",
                table: "m_court",
                column: "StateCode",
                principalTable: "m_state",
                principalColumn: "Id");
        }
    }
}
