using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class UpdateDemographicAndOperatorProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EnrollmentNo",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Telephone",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Identity",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "Identity",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Identity",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "Mobile",
                schema: "Identity",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Identity",
                table: "demographic");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Identity",
                table: "demographic");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "Identity",
                table: "demographic");

            migrationBuilder.RenameColumn(
                name: "Website",
                schema: "Identity",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "Identity",
                table: "demographic",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                schema: "Identity",
                table: "demographic",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                schema: "Identity",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Identity",
                table: "operator",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressInfo",
                schema: "Identity",
                table: "operator",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfJoining",
                schema: "Identity",
                table: "operator",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Identity",
                table: "demographic",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Identity",
                table: "demographic",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressInfo",
                schema: "Identity",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "DateOfJoining",
                schema: "Identity",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Identity",
                table: "demographic");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Identity",
                table: "demographic");

            migrationBuilder.RenameColumn(
                name: "Gender",
                schema: "Identity",
                table: "Users",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                schema: "Identity",
                table: "demographic",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "Identity",
                table: "demographic",
                newName: "DateOfBirth");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrollmentNo",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "Identity",
                table: "operator",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Identity",
                table: "operator",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "Identity",
                table: "operator",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Identity",
                table: "operator",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                schema: "Identity",
                table: "operator",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Identity",
                table: "demographic",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "Identity",
                table: "demographic",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "Identity",
                table: "demographic",
                type: "bytea",
                nullable: true);
        }
    }
}
