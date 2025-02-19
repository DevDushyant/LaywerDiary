using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class CreateCorporateAndLawyer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operator_Users_LawyerId",
                schema: "Identity",
                table: "operator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_operator",
                schema: "Identity",
                table: "operator");

            migrationBuilder.RenameTable(
                name: "operator",
                schema: "Identity",
                newName: "lawerusers",
                newSchema: "Identity");

            migrationBuilder.RenameIndex(
                name: "IX_operator_LawyerId",
                schema: "Identity",
                table: "lawerusers",
                newName: "IX_lawerusers_LawyerId");

            migrationBuilder.AddColumn<string>(
                name: "Enrollment",
                schema: "Identity",
                table: "lawerusers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_lawerusers",
                schema: "Identity",
                table: "lawerusers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Corporates",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirmName = table.Column<string>(type: "text", nullable: true),
                    FirmType = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    RegistrationNo = table.Column<string>(type: "text", nullable: true),
                    Owner = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corporates_Users_Id",
                        column: x => x.Id,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_lawerusers_Users_LawyerId",
                schema: "Identity",
                table: "lawerusers",
                column: "LawyerId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lawerusers_Users_LawyerId",
                schema: "Identity",
                table: "lawerusers");

            migrationBuilder.DropTable(
                name: "Corporates",
                schema: "Identity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lawerusers",
                schema: "Identity",
                table: "lawerusers");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                schema: "Identity",
                table: "lawerusers");

            migrationBuilder.RenameTable(
                name: "lawerusers",
                schema: "Identity",
                newName: "operator",
                newSchema: "Identity");

            migrationBuilder.RenameIndex(
                name: "IX_lawerusers_LawyerId",
                schema: "Identity",
                table: "operator",
                newName: "IX_operator_LawyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_operator",
                schema: "Identity",
                table: "operator",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_operator_Users_LawyerId",
                schema: "Identity",
                table: "operator",
                column: "LawyerId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
