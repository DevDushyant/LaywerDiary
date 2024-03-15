using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourtApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class App001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ld");

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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseStage = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_case_stage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_court_fee_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtFeeType = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_fee_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_court_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtType = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_expense_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_expense_head", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_lawyer",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_lawyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_nature",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_nature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_proceeding_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_proceeding_head", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_publisher",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PublicationName = table.Column<string>(type: "text", nullable: false),
                    PropriatorName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_state",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_state", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_subject",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_court_fee",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtFeeTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseKind = table.Column<string>(type: "text", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_case_kind", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_case_kind_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_c_type",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    NatureId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_c_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_c_type_m_nature_NatureId",
                        column: x => x.NatureId,
                        principalSchema: "ld",
                        principalTable: "m_nature",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_book",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    PublisherId = table.Column<Guid>(type: "uuid", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_book_m_book_type_BookTypeId",
                        column: x => x.BookTypeId,
                        principalSchema: "ld",
                        principalTable: "m_book_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_book_m_publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "ld",
                        principalTable: "m_publisher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_court_fee_structure",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MinValue = table.Column<double>(type: "double precision", nullable: false),
                    MaxValue = table.Column<double>(type: "double precision", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    FixAmount = table.Column<double>(type: "double precision", nullable: false),
                    StateCode = table.Column<int>(type: "integer", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court_fee_structure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_fee_structure_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_district",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    StateCode = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_district", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_district_m_state_StateCode",
                        column: x => x.StateCode,
                        principalTable: "m_state",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "client",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FatherName = table.Column<string>(type: "text", nullable: true),
                    Dob = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    OfficeEmail = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    IsRural = table.Column<bool>(type: "boolean", nullable: false),
                    Landmark = table.Column<string>(type: "text", nullable: true),
                    StateCode = table.Column<int>(type: "integer", nullable: true),
                    DistrictCode = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_m_district_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "m_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_m_state_StateCode",
                        column: x => x.StateCode,
                        principalTable: "m_state",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_block",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    DistrictCode = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_block_m_district_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "m_district",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_city",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    DistrictCode = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_city", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_city_m_district_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "m_district",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_court",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Bench = table.Column<string>(type: "text", nullable: true),
                    HeadQuerter = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    StateCode = table.Column<int>(type: "integer", nullable: true),
                    DistrictCode = table.Column<int>(type: "integer", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_court", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_court_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_m_district_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "m_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_court_m_state_StateCode",
                        column: x => x.StateCode,
                        principalTable: "m_state",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_ward",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CityCode = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_ward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_ward_m_city_CityCode",
                        column: x => x.CityCode,
                        principalTable: "m_city",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "client_case",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    TitleTypeFirst = table.Column<int>(type: "integer", nullable: false),
                    FirstTitle = table.Column<string>(type: "text", nullable: true),
                    TitleTypeSecond = table.Column<int>(type: "integer", nullable: false),
                    SecondTitle = table.Column<string>(type: "text", nullable: true),
                    CaseStageCode = table.Column<string>(type: "text", nullable: true),
                    CaseAgainstDecisionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AgainstCaseNumber = table.Column<string>(type: "text", nullable: true),
                    AgainstYear = table.Column<int>(type: "integer", nullable: true),
                    LinkedCaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    NatureId = table.Column<Guid>(type: "uuid", nullable: true),
                    TypeCaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: true),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    AgainstCourtTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    AgainstCourtId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_case", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_case_client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "ld",
                        principalTable: "client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_case_m_c_type_TypeCaseId",
                        column: x => x.TypeCaseId,
                        principalSchema: "ld",
                        principalTable: "m_c_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_case_m_case_kind_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "ld",
                        principalTable: "m_case_kind",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_case_m_court_AgainstCourtId",
                        column: x => x.AgainstCourtId,
                        principalSchema: "ld",
                        principalTable: "m_court",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_case_m_court_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "ld",
                        principalTable: "m_court",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_case_m_court_type_AgainstCourtTypeId",
                        column: x => x.AgainstCourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_case_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_client_case_m_nature_NatureId",
                        column: x => x.NatureId,
                        principalSchema: "ld",
                        principalTable: "m_nature",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_DistrictCode",
                schema: "ld",
                table: "client",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_client_StateCode",
                schema: "ld",
                table: "client",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_AgainstCourtId",
                schema: "ld",
                table: "client_case",
                column: "AgainstCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_AgainstCourtTypeId",
                schema: "ld",
                table: "client_case",
                column: "AgainstCourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_CaseTypeId",
                schema: "ld",
                table: "client_case",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_ClientId",
                schema: "ld",
                table: "client_case",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_CourtId",
                schema: "ld",
                table: "client_case",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_CourtTypeId",
                schema: "ld",
                table: "client_case",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_NatureId",
                schema: "ld",
                table: "client_case",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_client_case_TypeCaseId",
                schema: "ld",
                table: "client_case",
                column: "TypeCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_m_block_Code",
                table: "m_block",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_block_DistrictCode",
                table: "m_block",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_book_BookTypeId",
                schema: "ld",
                table: "m_book",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_book_PublisherId",
                schema: "ld",
                table: "m_book",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_NatureId",
                schema: "ld",
                table: "m_c_type",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_m_case_kind_CourtTypeId",
                schema: "ld",
                table: "m_case_kind",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_city_Code",
                table: "m_city",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_city_DistrictCode",
                table: "m_city",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_CourtTypeId",
                schema: "ld",
                table: "m_court",
                column: "CourtTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_m_court_fee_CourtFeeTypeId",
                schema: "ld",
                table: "m_court_fee",
                column: "CourtFeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_fee_structure_StateId",
                schema: "ld",
                table: "m_court_fee_structure",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_district_Code",
                table: "m_district",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_district_StateCode",
                table: "m_district",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_state_Code",
                table: "m_state",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_ward_CityCode",
                table: "m_ward",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_m_ward_Code",
                table: "m_ward",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "client_case",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_block");

            migrationBuilder.DropTable(
                name: "m_book",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_case_stage",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_fee",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_fee_structure",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_expense_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_lawyer",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_proceeding_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_subject",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_ward");

            migrationBuilder.DropTable(
                name: "client",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_c_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_case_kind",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_book_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_publisher",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_fee_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_city");

            migrationBuilder.DropTable(
                name: "m_nature",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_district");

            migrationBuilder.DropTable(
                name: "m_state");
        }
    }
}
