using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourtApp.Infrastructure.DbMigration
{
    public partial class initialAppMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LDiary");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_BookType",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_BookType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CaseKind",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseKind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_CaseKind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CaseNature",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_CaseNature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CaseStage",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseStage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_CaseStage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Country",
                schema: "Common",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Country", x => x.CountryCode);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CourtFeeType",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtFeeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_CourtFeeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CourtType",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_CourtType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_ExpenseHead",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_ExpenseHead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Practice_Subject",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Practice_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Publisher",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropriatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Typeofcases",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNatureId = table.Column<int>(type: "int", nullable: false),
                    Typeofcases = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Typeofcases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_Typeofcases_Mst_CaseNature_CaseNatureId",
                        column: x => x.CaseNatureId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CaseNature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_State",
                schema: "Common",
                columns: table => new
                {
                    StateCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_State", x => x.StateCode);
                    table.ForeignKey(
                        name: "FK_Mst_State_Mst_Country_CountryCode",
                        column: x => x.CountryCode,
                        principalSchema: "Common",
                        principalTable: "Mst_Country",
                        principalColumn: "CountryCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CourtFee",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtFeeTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_CourtFee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_CourtFee_Mst_CourtFeeType_CourtFeeTypeId",
                        column: x => x.CourtFeeTypeId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CourtFeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Book",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTypeId = table.Column<int>(type: "int", nullable: false),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_Book_Mst_BookType_BookTypeId",
                        column: x => x.BookTypeId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_BookType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mst_Book_Mst_Publisher_PublisherID",
                        column: x => x.PublisherID,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CourtFeeStructure",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MinValue = table.Column<double>(type: "float", nullable: false),
                    MaxValue = table.Column<double>(type: "float", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    FixAmount = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_CourtFeeStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_CourtFeeStructure_Mst_State_StateCode",
                        column: x => x.StateCode,
                        principalSchema: "Common",
                        principalTable: "Mst_State",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mst_District",
                schema: "Common",
                columns: table => new
                {
                    DistrictCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_District", x => x.DistrictCode);
                    table.ForeignKey(
                        name: "FK_Mst_District_Mst_State_StateCode",
                        column: x => x.StateCode,
                        principalSchema: "Common",
                        principalTable: "Mst_State",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DistrictCode = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Mst_District_DistrictCode",
                        column: x => x.DistrictCode,
                        principalSchema: "Common",
                        principalTable: "Mst_District",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_Mst_State_StateCode",
                        column: x => x.StateCode,
                        principalSchema: "Common",
                        principalTable: "Mst_State",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mst_City",
                schema: "Common",
                columns: table => new
                {
                    CityCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_City", x => x.CityCode);
                    table.ForeignKey(
                        name: "FK_Mst_City_Mst_District_DistrictCode",
                        column: x => x.DistrictCode,
                        principalSchema: "Common",
                        principalTable: "Mst_District",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Court",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CourtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bench = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadQuerter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictCode = table.Column<int>(type: "int", nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourtTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Court", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_Court_Mst_CourtType_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CourtType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mst_Court_Mst_District_DistrictCode",
                        column: x => x.DistrictCode,
                        principalSchema: "Common",
                        principalTable: "Mst_District",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mst_Court_Mst_State_StateCode",
                        column: x => x.StateCode,
                        principalSchema: "Common",
                        principalTable: "Mst_State",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_DistrictCode",
                schema: "LDiary",
                table: "Client",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Client_StateCode",
                schema: "LDiary",
                table: "Client",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Book_BookTypeId",
                schema: "LDiary",
                table: "Mst_Book",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Book_PublisherID",
                schema: "LDiary",
                table: "Mst_Book",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_City_DistrictCode",
                schema: "Common",
                table: "Mst_City",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Court_CourtTypeId",
                schema: "LDiary",
                table: "Mst_Court",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Court_DistrictCode",
                schema: "LDiary",
                table: "Mst_Court",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Court_StateCode",
                schema: "LDiary",
                table: "Mst_Court",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Court_UniqueId",
                schema: "LDiary",
                table: "Mst_Court",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mst_CourtFee_CourtFeeTypeId",
                schema: "LDiary",
                table: "Mst_CourtFee",
                column: "CourtFeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_CourtFeeStructure_StateCode",
                schema: "LDiary",
                table: "Mst_CourtFeeStructure",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_District_StateCode",
                schema: "Common",
                table: "Mst_District",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_State_CountryCode",
                schema: "Common",
                table: "Mst_State",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Mst_Typeofcases_CaseNatureId",
                schema: "LDiary",
                table: "Mst_Typeofcases",
                column: "CaseNatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_Book",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_CaseKind",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_CaseStage",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_City",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Mst_Court",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_CourtFee",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_CourtFeeStructure",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_ExpenseHead",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_Practice_Subject",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_Typeofcases",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_BookType",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_Publisher",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_CourtType",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_District",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Mst_CourtFeeType",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_CaseNature",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_State",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Mst_Country",
                schema: "Common");
        }
    }
}
