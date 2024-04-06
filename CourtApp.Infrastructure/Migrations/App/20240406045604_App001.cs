using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourtApp.Infrastructure.Migrations.App
{
    /// <inheritdoc />
    public partial class App001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ld");

            migrationBuilder.EnsureSchema(
                name: "ad");

            migrationBuilder.EnsureSchema(
                name: "common");

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
                name: "case_detail_against",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImpugedOrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CisNumber = table.Column<string>(type: "text", nullable: true),
                    CisYear = table.Column<int>(type: "integer", nullable: false),
                    CnrNumber = table.Column<string>(type: "text", nullable: true),
                    ProcOfficer = table.Column<string>(type: "text", nullable: true),
                    Cadder = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_detail_against", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_act_type",
                schema: "ad",
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
                    table.PrimaryKey("PK_m_act_type", x => x.Id);
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
                name: "m_gazzet_type",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_gazzet_type", x => x.Id);
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
                name: "m_proceeding_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
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
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
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
                name: "m_work_master",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Work_En = table.Column<string>(type: "text", nullable: true),
                    Work_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_work_master", x => x.Id);
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
                name: "m_part",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    GazetteTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_part", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_part_m_gazzet_type_GazetteTypeId",
                        column: x => x.GazetteTypeId,
                        principalSchema: "ad",
                        principalTable: "m_gazzet_type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_proceeding_sub_head",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PHeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    ProceedingHeadId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_proceeding_sub_head", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_proceeding_sub_head_m_proceeding_head_ProceedingHeadId",
                        column: x => x.ProceedingHeadId,
                        principalSchema: "ld",
                        principalTable: "m_proceeding_head",
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
                name: "m_nature",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_nature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_nature_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_nature_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_work_master_sub",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    WorkMasterId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_work_master_sub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_work_master_sub_m_work_master_WorkMasterId",
                        column: x => x.WorkMasterId,
                        principalSchema: "ld",
                        principalTable: "m_work_master",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_act",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActCategory = table.Column<string>(type: "text", nullable: true),
                    ActNumber = table.Column<int>(type: "integer", nullable: false),
                    SubActNumber = table.Column<int>(type: "integer", nullable: false),
                    ActYear = table.Column<int>(type: "integer", nullable: false),
                    AssentBy = table.Column<string>(type: "text", nullable: true),
                    AssentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActName = table.Column<string>(type: "text", nullable: true),
                    GazetteId = table.Column<int>(type: "integer", nullable: false),
                    Nature = table.Column<string>(type: "text", nullable: true),
                    GazetteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PageNo = table.Column<int>(type: "integer", nullable: true),
                    ComeInforce = table.Column<string>(type: "text", nullable: true),
                    PublishedGazeteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    PartId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_act", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_act_m_act_type_ActTypeId",
                        column: x => x.ActTypeId,
                        principalSchema: "ad",
                        principalTable: "m_act_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_act_m_part_PartId",
                        column: x => x.PartId,
                        principalSchema: "ad",
                        principalTable: "m_part",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_act_m_subject_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "common",
                        principalTable: "m_subject",
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
                name: "m_court_district",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
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
                        name: "FK_m_court_district_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
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
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
                    CaseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    NatureId = table.Column<Guid>(type: "uuid", nullable: true),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_c_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_c_type_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_c_type_m_nature_NatureId",
                        column: x => x.NatureId,
                        principalSchema: "ld",
                        principalTable: "m_nature",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_m_c_type_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ad.m_repealed_rule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepealedActID = table.Column<int>(type: "integer", nullable: false),
                    ActID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ad.m_repealed_rule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ad.m_repealed_rule_m_act_ActID",
                        column: x => x.ActID,
                        principalSchema: "ad",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_act_amended",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AmendedActID = table.Column<int>(type: "integer", nullable: false),
                    ActID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_act_amended", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_act_amended_m_act_ActID",
                        column: x => x.ActID,
                        principalSchema: "ad",
                        principalTable: "m_act",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "m_act_book",
                schema: "ad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    BookYear = table.Column<int>(type: "integer", nullable: false),
                    BookPageNo = table.Column<string>(type: "text", nullable: true),
                    BookSrNo = table.Column<int>(type: "integer", nullable: true),
                    Volume = table.Column<string>(type: "text", nullable: true),
                    ActId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_act_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_act_book_m_act_ActId",
                        column: x => x.ActId,
                        principalSchema: "ad",
                        principalTable: "m_act",
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
                name: "m_court_complex",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_En = table.Column<string>(type: "text", nullable: true),
                    Name_Hn = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    CourtDistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: true),
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
                        name: "FK_m_court_complex_m_state_StateId",
                        column: x => x.StateId,
                        principalTable: "m_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "case_detail",
                schema: "ld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseKindId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CisNumber = table.Column<string>(type: "text", nullable: true),
                    CisYear = table.Column<int>(type: "integer", nullable: false),
                    CnrNumber = table.Column<string>(type: "text", nullable: true),
                    FirstTitle = table.Column<string>(type: "text", nullable: true),
                    FirstTitleCode = table.Column<int>(type: "integer", nullable: false),
                    SecondTitle = table.Column<string>(type: "text", nullable: true),
                    SecoundTitleCode = table.Column<int>(type: "integer", nullable: false),
                    NextDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CaseStageCode = table.Column<string>(type: "text", nullable: true),
                    LinkedCaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeOfCaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_case_detail_m_c_type_TypeOfCaseId",
                        column: x => x.TypeOfCaseId,
                        principalSchema: "ld",
                        principalTable: "m_c_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_case_detail_m_case_kind_CaseKindId",
                        column: x => x.CaseKindId,
                        principalSchema: "ld",
                        principalTable: "m_case_kind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_court_CourtId",
                        column: x => x.CourtId,
                        principalSchema: "ld",
                        principalTable: "m_court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_court_type_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalSchema: "ld",
                        principalTable: "m_court_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_case_detail_m_nature_NatureId",
                        column: x => x.NatureId,
                        principalSchema: "ld",
                        principalTable: "m_nature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ad.m_repealed_rule_ActID",
                table: "ad.m_repealed_rule",
                column: "ActID");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CaseKindId",
                schema: "ld",
                table: "case_detail",
                column: "CaseKindId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtId",
                schema: "ld",
                table: "case_detail",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_CourtTypeId",
                schema: "ld",
                table: "case_detail",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_NatureId",
                schema: "ld",
                table: "case_detail",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_case_detail_TypeOfCaseId",
                schema: "ld",
                table: "case_detail",
                column: "TypeOfCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_case_titles_CaseId",
                schema: "ld",
                table: "case_titles",
                column: "CaseId");

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
                name: "IX_m_act_ActTypeId",
                schema: "ad",
                table: "m_act",
                column: "ActTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_PartId",
                schema: "ad",
                table: "m_act",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_SubjectId",
                schema: "ad",
                table: "m_act",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_amended_ActID",
                schema: "ad",
                table: "m_act_amended",
                column: "ActID");

            migrationBuilder.CreateIndex(
                name: "IX_m_act_book_ActId",
                schema: "ad",
                table: "m_act_book",
                column: "ActId");

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
                name: "IX_m_c_type_CourtTypeId",
                schema: "ld",
                table: "m_c_type",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_NatureId",
                schema: "ld",
                table: "m_c_type",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_m_c_type_StateId",
                schema: "ld",
                table: "m_c_type",
                column: "StateId");

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
                name: "IX_m_court_complex_StateId",
                schema: "ld",
                table: "m_court_complex",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_DistrictId",
                schema: "ld",
                table: "m_court_district",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_m_court_district_StateId",
                schema: "ld",
                table: "m_court_district",
                column: "StateId");

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
                name: "IX_m_nature_CourtTypeId",
                schema: "ld",
                table: "m_nature",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_nature_StateId",
                schema: "ld",
                table: "m_nature",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_m_part_GazetteTypeId",
                schema: "ad",
                table: "m_part",
                column: "GazetteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_m_proceeding_sub_head_ProceedingHeadId",
                schema: "ld",
                table: "m_proceeding_sub_head",
                column: "ProceedingHeadId");

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

            migrationBuilder.CreateIndex(
                name: "IX_m_work_master_sub_WorkMasterId",
                schema: "ld",
                table: "m_work_master_sub",
                column: "WorkMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ad.m_repealed_rule");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "case_detail_against",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "case_titles",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "client",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_act_amended",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_act_book",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_block");

            migrationBuilder.DropTable(
                name: "m_book",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_case_stage",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_complex",
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
                name: "m_proceeding_sub_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_ward");

            migrationBuilder.DropTable(
                name: "m_work_master_sub",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "case_detail",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_act",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_book_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_publisher",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_district",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_court_fee_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_proceeding_head",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_city");

            migrationBuilder.DropTable(
                name: "m_work_master",
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
                name: "m_act_type",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_part",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_subject",
                schema: "common");

            migrationBuilder.DropTable(
                name: "m_nature",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_district");

            migrationBuilder.DropTable(
                name: "m_gazzet_type",
                schema: "ad");

            migrationBuilder.DropTable(
                name: "m_court_type",
                schema: "ld");

            migrationBuilder.DropTable(
                name: "m_state");
        }
    }
}
