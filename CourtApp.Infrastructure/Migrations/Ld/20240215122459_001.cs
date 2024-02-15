using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.Ld
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ld");

            migrationBuilder.EnsureSchema(
                name: "LDiary");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    AffectedColumns = table.Column<string>(type: "text", nullable: true),
                    PrimaryKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_book_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookType = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_book_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_case_stage",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaseStage = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_case_stage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_casenature",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaseNature = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_casenature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_Country",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "text", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_Country", x => x.CountryCode);
                });

            migrationBuilder.CreateTable(
                name: "m_court_fee_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourtFeeType = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_fee_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_CourtType",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourtType = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HeadName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_ExpenseHead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Lawyer",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Lawyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Practice_Subject",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicationName = table.Column<string>(type: "text", nullable: false),
                    PropriatorName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaseNatureId = table.Column<int>(type: "integer", nullable: false),
                    Typeofcases = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Typeofcases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_Typeofcases_m_casenature_CaseNatureId",
                        column: x => x.CaseNatureId,
                        principalSchema: "ld",
                        principalTable: "m_casenature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_State",
                columns: table => new
                {
                    StateCode = table.Column<string>(type: "text", nullable: false),
                    StateName = table.Column<string>(type: "text", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_State", x => x.StateCode);
                    table.ForeignKey(
                        name: "FK_m_State_m_Country_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "m_Country",
                        principalColumn: "CountryCode");
                });

            migrationBuilder.CreateTable(
                name: "m_court_fee",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourtFeeTypeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_fee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_fee_m_court_fee_type_CourtFeeTypeId",
                        column: x => x.CourtFeeTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_fee_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_case_kind",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaseKind = table.Column<string>(type: "text", nullable: false),
                    CourtTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_case_kind", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_case_kind_Mst_CourtType_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CourtType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mst_Book",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookTypeId = table.Column<int>(type: "integer", nullable: false),
                    PublisherID = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mst_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mst_Book_Mst_Publisher_PublisherID",
                        column: x => x.PublisherID,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mst_Book_m_book_type_BookTypeId",
                        column: x => x.BookTypeId,
                        principalSchema: "ld",
                        principalTable: "m_book_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_court_fee_structure",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateCode = table.Column<string>(type: "text", nullable: true),
                    MinValue = table.Column<double>(type: "double precision", nullable: false),
                    MaxValue = table.Column<double>(type: "double precision", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    FixAmount = table.Column<double>(type: "double precision", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_fee_structure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_fee_structure_m_State_StateCode",
                        column: x => x.StateCode,
                        principalTable: "m_State",
                        principalColumn: "StateCode");
                });

            migrationBuilder.CreateTable(
                name: "m_District",
                columns: table => new
                {
                    DistrictCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistrictName = table.Column<string>(type: "text", nullable: true),
                    StateCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_District", x => x.DistrictCode);
                    table.ForeignKey(
                        name: "FK_m_District_m_State_StateCode",
                        column: x => x.StateCode,
                        principalTable: "m_State",
                        principalColumn: "StateCode");
                });

            migrationBuilder.CreateTable(
                name: "m_City",
                columns: table => new
                {
                    CityCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityName = table.Column<string>(type: "text", nullable: true),
                    DistrictCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_City", x => x.CityCode);
                    table.ForeignKey(
                        name: "FK_m_City_m_District_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "m_District",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_client",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    OfficeEmail = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    StateCode = table.Column<string>(type: "text", nullable: true),
                    DistrictCode = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_client_m_District_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "m_District",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_client_m_State_StateCode",
                        column: x => x.StateCode,
                        principalTable: "m_State",
                        principalColumn: "StateCode");
                });

            migrationBuilder.CreateTable(
                name: "Mst_Court",
                schema: "LDiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CourtName = table.Column<string>(type: "text", nullable: true),
                    Bench = table.Column<string>(type: "text", nullable: true),
                    HeadQuerter = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    DistrictCode = table.Column<int>(type: "integer", nullable: false),
                    StateCode = table.Column<string>(type: "text", nullable: false),
                    CourtTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                        name: "FK_Mst_Court_m_District_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "m_District",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mst_Court_m_State_StateCode",
                        column: x => x.StateCode,
                        principalTable: "m_State",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "u_case_detail",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    InstitutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LinkedClient = table.Column<int>(type: "integer", nullable: false),
                    CaseNatureId = table.Column<int>(type: "integer", nullable: false),
                    TypeOfCaseId = table.Column<int>(type: "integer", nullable: false),
                    CourtTypeId = table.Column<int>(type: "integer", nullable: false),
                    CourtId = table.Column<int>(type: "integer", nullable: false),
                    CaseTypeId = table.Column<int>(type: "integer", nullable: false),
                    CaseNumber = table.Column<string>(type: "text", nullable: false),
                    CaseYear = table.Column<int>(type: "integer", nullable: false),
                    TitleFirst = table.Column<string>(type: "text", nullable: false),
                    FirstTitleType = table.Column<int>(type: "integer", nullable: false),
                    TitleSecond = table.Column<string>(type: "text", nullable: false),
                    SecondTitleType = table.Column<int>(type: "integer", nullable: false),
                    NextDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CaseStageCode = table.Column<string>(type: "text", nullable: true),
                    CaseAgainstDecisionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AgainstCourtTypeId = table.Column<int>(type: "integer", nullable: true),
                    AgainstCourtId = table.Column<int>(type: "integer", nullable: true),
                    AgainstCaseNumber = table.Column<string>(type: "text", nullable: true),
                    AgainstYear = table.Column<int>(type: "integer", nullable: true),
                    LinkedCaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_u_case_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_u_case_detail_Mst_CourtType_AgainstCourtTypeId",
                        column: x => x.AgainstCourtTypeId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CourtType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_u_case_detail_Mst_CourtType_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_CourtType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_u_case_detail_Mst_Court_AgainstCourtId",
                        column: x => x.AgainstCourtId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Court",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_u_case_detail_Mst_Court_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_u_case_detail_Mst_Typeofcases_TypeOfCaseId",
                        column: x => x.TypeOfCaseId,
                        principalSchema: "LDiary",
                        principalTable: "Mst_Typeofcases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_u_case_detail_m_case_kind_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "ld",
                        principalTable: "m_case_kind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_u_case_detail_m_casenature_CaseNatureId",
                        column: x => x.CaseNatureId,
                        principalSchema: "ld",
                        principalTable: "m_casenature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_u_case_detail_m_client_LinkedClient",
                        column: x => x.LinkedClient,
                        principalSchema: "ld",
                        principalTable: "m_client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_case_kind_CourtTypeId",
                schema: "ld",
                table: "m_case_kind",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_City_DistrictCode",
                table: "m_City",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_client_DistrictCode",
                schema: "ld",
                table: "m_client",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_client_StateCode",
                schema: "ld",
                table: "m_client",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_fee_CourtFeeTypeId",
                schema: "ld",
                table: "m_court_fee",
                column: "CourtFeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_fee_structure_StateCode",
                schema: "ld",
                table: "m_court_fee_structure",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_District_StateCode",
                table: "m_District",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_State_CountryCode",
                table: "m_State",
                column: "CountryCode");

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
                name: "IX_Mst_Typeofcases_CaseNatureId",
                schema: "LDiary",
                table: "Mst_Typeofcases",
                column: "CaseNatureId");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_AgainstCourtId",
                schema: "ld",
                table: "u_case_detail",
                column: "AgainstCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_AgainstCourtTypeId",
                schema: "ld",
                table: "u_case_detail",
                column: "AgainstCourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_CaseNatureId",
                schema: "ld",
                table: "u_case_detail",
                column: "CaseNatureId");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_CaseTypeId",
                schema: "ld",
                table: "u_case_detail",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_CourtId",
                schema: "ld",
                table: "u_case_detail",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_CourtTypeId",
                schema: "ld",
                table: "u_case_detail",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_LinkedClient",
                schema: "ld",
                table: "u_case_detail",
                column: "LinkedClient");

            migrationBuilder.CreateIndex(
                name: "IX_u_case_detail_TypeOfCaseId",
                schema: "ld",
                table: "u_case_detail",
                column: "TypeOfCaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "m_case_stage",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_City");

            migrationBuilder.DropTable(
                name: "m_court_fee",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_fee_structure",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "Mst_Book",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_ExpenseHead",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_Lawyer",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_Practice_Subject",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "u_case_detail",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_fee_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "Mst_Publisher",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "m_book_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "Mst_Court",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "Mst_Typeofcases",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "m_case_kind",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_client",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_casenature",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "Mst_CourtType",
                schema: "LDiary");

            migrationBuilder.DropTable(
                name: "m_District");

            migrationBuilder.DropTable(
                name: "m_State");

            migrationBuilder.DropTable(
                name: "m_Country");
        }
    }
}
