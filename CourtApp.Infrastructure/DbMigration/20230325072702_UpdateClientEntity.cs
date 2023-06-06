using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class UpdateClientEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNo",
                schema: "LDiary",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "LandMark",
                schema: "LDiary",
                table: "Client",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "LDiary",
                table: "Client",
                newName: "LandMark");

            migrationBuilder.AddColumn<string>(
                name: "HouseNo",
                schema: "LDiary",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
