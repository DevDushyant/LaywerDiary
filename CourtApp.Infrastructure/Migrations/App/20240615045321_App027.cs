using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App027 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_district_DistrictCode",
                schema: "ld",
                table: "client");

            migrationBuilder.DropForeignKey(
                name: "FK_m_block_m_district_DistrictCode",
                table: "m_block");

            migrationBuilder.DropForeignKey(
                name: "FK_m_city_m_district_DistrictCode",
                table: "m_city");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_district_DistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_district_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_district");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_district",
                table: "m_district");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "m_district",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_district",
                table: "m_district",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_district_DistrictCode",
                schema: "ld",
                table: "client",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_m_block_m_district_DistrictCode",
                table: "m_block",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_m_city_m_district_DistrictCode",
                table: "m_city",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_district_DistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_district_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_district",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_m_district_DistrictCode",
                schema: "ld",
                table: "client");

            migrationBuilder.DropForeignKey(
                name: "FK_m_block_m_district_DistrictCode",
                table: "m_block");

            migrationBuilder.DropForeignKey(
                name: "FK_m_city_m_district_DistrictCode",
                table: "m_city");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_complex_m_district_DistrictId",
                schema: "ld",
                table: "m_court_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_m_court_district_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_district");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_district",
                table: "m_district");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "m_district",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_district",
                table: "m_district",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_client_m_district_DistrictCode",
                schema: "ld",
                table: "client",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_block_m_district_DistrictCode",
                table: "m_block",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_city_m_district_DistrictCode",
                table: "m_city",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_m_district_DistrictId",
                schema: "ld",
                table: "m_court",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_complex_m_district_DistrictId",
                schema: "ld",
                table: "m_court_complex",
                column: "DistrictId",
                principalTable: "m_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_m_court_district_m_district_DistrictCode",
                schema: "ld",
                table: "m_court_district",
                column: "DistrictCode",
                principalTable: "m_district",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
