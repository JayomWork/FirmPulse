using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FirmPulse.Data.Migrations
{
    /// <inheritdoc />
    public partial class Phase2CsWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilingRecordId",
                table: "DocumentRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DocumentRecords",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkItemId",
                table: "DocumentRecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyClass",
                table: "CompanyClients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinancialYearEndDay",
                table: "CompanyClients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinancialYearEndMonth",
                table: "CompanyClients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasShareCapital",
                table: "CompanyClients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsListed",
                table: "CompanyClients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastAGMDate",
                table: "CompanyClients",
                type: "date",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComplianceTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    TemplateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CompanyType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FinancialYearBased = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemTemplate = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplianceTemplates_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "McaFormMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FormName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ApplicableTo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DefaultDueRule = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsAnnualForm = table.Column<bool>(type: "boolean", nullable: false),
                    IsEventBasedForm = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_McaFormMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCompliancePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: false),
                    CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                    ComplianceTemplateId = table.Column<int>(type: "integer", nullable: false),
                    FinancialYear = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PlanName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AGMDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCompliancePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCompliancePlans_CompanyClients_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyCompliancePlans_ComplianceTemplates_ComplianceTempla~",
                        column: x => x.ComplianceTemplateId,
                        principalTable: "ComplianceTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyCompliancePlans_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceCategoryId = table.Column<int>(type: "integer", nullable: false),
                    ServiceCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DefaultFee = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    DefaultDueDays = table.Column<int>(type: "integer", nullable: true),
                    IsRecurring = table.Column<bool>(type: "boolean", nullable: false),
                    RecurringType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceMasters_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMeetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: false),
                    CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                    CompanyCompliancePlanId = table.Column<int>(type: "integer", nullable: true),
                    MeetingType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MeetingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    VenueOrMode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NoticePrepared = table.Column<bool>(type: "boolean", nullable: false),
                    AgendaPrepared = table.Column<bool>(type: "boolean", nullable: false),
                    MinutesPrepared = table.Column<bool>(type: "boolean", nullable: false),
                    SignedCopyReceived = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMeetings_CompanyClients_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyMeetings_CompanyCompliancePlans_CompanyCompliancePla~",
                        column: x => x.CompanyCompliancePlanId,
                        principalTable: "CompanyCompliancePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyMeetings_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceTemplateItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComplianceTemplateId = table.Column<int>(type: "integer", nullable: false),
                    ServiceMasterId = table.Column<int>(type: "integer", nullable: true),
                    McaFormMasterId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SequenceNo = table.Column<int>(type: "integer", nullable: false),
                    DefaultDueOffsetDays = table.Column<int>(type: "integer", nullable: true),
                    DueDateBasedOn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequiresFiling = table.Column<bool>(type: "boolean", nullable: false),
                    RequiresDocuments = table.Column<bool>(type: "boolean", nullable: false),
                    RequiresMeeting = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceTemplateItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplianceTemplateItems_ComplianceTemplates_ComplianceTempl~",
                        column: x => x.ComplianceTemplateId,
                        principalTable: "ComplianceTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplianceTemplateItems_McaFormMasters_McaFormMasterId",
                        column: x => x.McaFormMasterId,
                        principalTable: "McaFormMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplianceTemplateItems_ServiceMasters_ServiceMasterId",
                        column: x => x.ServiceMasterId,
                        principalTable: "ServiceMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: false),
                    CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                    CompanyCompliancePlanId = table.Column<int>(type: "integer", nullable: true),
                    ServiceMasterId = table.Column<int>(type: "integer", nullable: true),
                    McaFormMasterId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FinancialYear = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AssignedTo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    RequiresClientDocuments = table.Column<bool>(type: "boolean", nullable: false),
                    ClientDocumentsStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequiresMcaFiling = table.Column<bool>(type: "boolean", nullable: false),
                    FilingStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompletedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItems_CompanyClients_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkItems_CompanyCompliancePlans_CompanyCompliancePlanId",
                        column: x => x.CompanyCompliancePlanId,
                        principalTable: "CompanyCompliancePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkItems_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkItems_McaFormMasters_McaFormMasterId",
                        column: x => x.McaFormMasterId,
                        principalTable: "McaFormMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkItems_ServiceMasters_ServiceMasterId",
                        column: x => x.ServiceMasterId,
                        principalTable: "ServiceMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentChecklistTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceMasterId = table.Column<int>(type: "integer", nullable: true),
                    ComplianceTemplateItemId = table.Column<int>(type: "integer", nullable: true),
                    DocumentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentChecklistTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentChecklistTemplates_ComplianceTemplateItems_Complian~",
                        column: x => x.ComplianceTemplateItemId,
                        principalTable: "ComplianceTemplateItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentChecklistTemplates_ServiceMasters_ServiceMasterId",
                        column: x => x.ServiceMasterId,
                        principalTable: "ServiceMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientFollowUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: false),
                    CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                    WorkItemId = table.Column<int>(type: "integer", nullable: true),
                    FollowUpDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FollowUpType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    NextFollowUpDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientFollowUps_CompanyClients_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientFollowUps_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientFollowUps_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalTable: "WorkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: false),
                    CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                    WorkItemId = table.Column<int>(type: "integer", nullable: true),
                    McaFormMasterId = table.Column<int>(type: "integer", nullable: false),
                    FinancialYear = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FilingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SRN = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChallanNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChallanAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    McaStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AcknowledgementFilePath = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilingRecords_CompanyClients_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilingRecords_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilingRecords_McaFormMasters_McaFormMasterId",
                        column: x => x.McaFormMasterId,
                        principalTable: "McaFormMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilingRecords_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalTable: "WorkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmId = table.Column<int>(type: "integer", nullable: false),
                    CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                    WorkItemId = table.Column<int>(type: "integer", nullable: false),
                    DocumentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DocumentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    ReceivedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ReviewedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemDocuments_CompanyClients_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkItemDocuments_Firms_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkItemDocuments_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalTable: "WorkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemStatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkItemId = table.Column<int>(type: "integer", nullable: false),
                    OldStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NewStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ChangedBy = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemStatusHistories_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalTable: "WorkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRecords_FilingRecordId",
                table: "DocumentRecords",
                column: "FilingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRecords_WorkItemId",
                table: "DocumentRecords",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFollowUps_CompanyClientId",
                table: "ClientFollowUps",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFollowUps_FirmId",
                table: "ClientFollowUps",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFollowUps_WorkItemId",
                table: "ClientFollowUps",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCompliancePlans_CompanyClientId",
                table: "CompanyCompliancePlans",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCompliancePlans_ComplianceTemplateId",
                table: "CompanyCompliancePlans",
                column: "ComplianceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCompliancePlans_FirmId",
                table: "CompanyCompliancePlans",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMeetings_CompanyClientId",
                table: "CompanyMeetings",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMeetings_CompanyCompliancePlanId",
                table: "CompanyMeetings",
                column: "CompanyCompliancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMeetings_FirmId",
                table: "CompanyMeetings",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceTemplateItems_ComplianceTemplateId",
                table: "ComplianceTemplateItems",
                column: "ComplianceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceTemplateItems_McaFormMasterId",
                table: "ComplianceTemplateItems",
                column: "McaFormMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceTemplateItems_ServiceMasterId",
                table: "ComplianceTemplateItems",
                column: "ServiceMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceTemplates_FirmId",
                table: "ComplianceTemplates",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChecklistTemplates_ComplianceTemplateItemId",
                table: "DocumentChecklistTemplates",
                column: "ComplianceTemplateItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChecklistTemplates_ServiceMasterId",
                table: "DocumentChecklistTemplates",
                column: "ServiceMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_FilingRecords_CompanyClientId",
                table: "FilingRecords",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FilingRecords_FirmId",
                table: "FilingRecords",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilingRecords_McaFormMasterId",
                table: "FilingRecords",
                column: "McaFormMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_FilingRecords_WorkItemId",
                table: "FilingRecords",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceMasters_ServiceCategoryId",
                table: "ServiceMasters",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemDocuments_CompanyClientId",
                table: "WorkItemDocuments",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemDocuments_FirmId",
                table: "WorkItemDocuments",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemDocuments_WorkItemId",
                table: "WorkItemDocuments",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_CompanyClientId",
                table: "WorkItems",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_CompanyCompliancePlanId",
                table: "WorkItems",
                column: "CompanyCompliancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_FirmId",
                table: "WorkItems",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_McaFormMasterId",
                table: "WorkItems",
                column: "McaFormMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_ServiceMasterId",
                table: "WorkItems",
                column: "ServiceMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemStatusHistories_WorkItemId",
                table: "WorkItemStatusHistories",
                column: "WorkItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_FilingRecords_FilingRecordId",
                table: "DocumentRecords",
                column: "FilingRecordId",
                principalTable: "FilingRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_WorkItems_WorkItemId",
                table: "DocumentRecords",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_FilingRecords_FilingRecordId",
                table: "DocumentRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_WorkItems_WorkItemId",
                table: "DocumentRecords");

            migrationBuilder.DropTable(
                name: "ClientFollowUps");

            migrationBuilder.DropTable(
                name: "CompanyMeetings");

            migrationBuilder.DropTable(
                name: "DocumentChecklistTemplates");

            migrationBuilder.DropTable(
                name: "FilingRecords");

            migrationBuilder.DropTable(
                name: "WorkItemDocuments");

            migrationBuilder.DropTable(
                name: "WorkItemStatusHistories");

            migrationBuilder.DropTable(
                name: "ComplianceTemplateItems");

            migrationBuilder.DropTable(
                name: "WorkItems");

            migrationBuilder.DropTable(
                name: "CompanyCompliancePlans");

            migrationBuilder.DropTable(
                name: "McaFormMasters");

            migrationBuilder.DropTable(
                name: "ServiceMasters");

            migrationBuilder.DropTable(
                name: "ComplianceTemplates");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropIndex(
                name: "IX_DocumentRecords_FilingRecordId",
                table: "DocumentRecords");

            migrationBuilder.DropIndex(
                name: "IX_DocumentRecords_WorkItemId",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "FilingRecordId",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "WorkItemId",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "CompanyClass",
                table: "CompanyClients");

            migrationBuilder.DropColumn(
                name: "FinancialYearEndDay",
                table: "CompanyClients");

            migrationBuilder.DropColumn(
                name: "FinancialYearEndMonth",
                table: "CompanyClients");

            migrationBuilder.DropColumn(
                name: "HasShareCapital",
                table: "CompanyClients");

            migrationBuilder.DropColumn(
                name: "IsListed",
                table: "CompanyClients");

            migrationBuilder.DropColumn(
                name: "LastAGMDate",
                table: "CompanyClients");
        }
    }
}
