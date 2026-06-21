using System;
using FirmPulse.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FirmPulse.Data.Migrations;

[Migration("20260621183000_AddDraftLibraryAndDocumentGeneration")]
[DbContext(typeof(FirmPulseDbContext))]
public partial class AddDraftLibraryAndDocumentGeneration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "AuthorizedCapital",
            table: "CompanyClients",
            type: "numeric(18,2)",
            precision: 18,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "City",
            table: "CompanyClients",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<DateOnly>(
            name: "FinancialYearEnd",
            table: "CompanyClients",
            type: "date",
            nullable: true);

        migrationBuilder.AddColumn<DateOnly>(
            name: "FinancialYearStart",
            table: "CompanyClients",
            type: "date",
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "PaidUpCapital",
            table: "CompanyClients",
            type: "numeric(18,2)",
            precision: 18,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "PinCode",
            table: "CompanyClients",
            type: "character varying(20)",
            maxLength: 20,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "RegistrationNumber",
            table: "CompanyClients",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "State",
            table: "CompanyClients",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Address",
            table: "Directors",
            type: "character varying(500)",
            maxLength: 500,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Designation",
            table: "Directors",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.CreateTable(
            name: "DocumentTemplates",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                TemplateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                TemplateCategory = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                TemplateContent = table.Column<string>(type: "text", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentTemplates", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DocumentTemplateFields",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                DocumentTemplateId = table.Column<int>(type: "integer", nullable: false),
                FieldKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                FieldLabel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                FieldType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                DataSource = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                DefaultValue = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentTemplateFields", x => x.Id);
                table.ForeignKey(
                    name: "FK_DocumentTemplateFields_DocumentTemplates_DocumentTemplateId",
                    column: x => x.DocumentTemplateId,
                    principalTable: "DocumentTemplates",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "GeneratedDocuments",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                DocumentTemplateId = table.Column<int>(type: "integer", nullable: false),
                DocumentTitle = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                GeneratedContent = table.Column<string>(type: "text", nullable: false),
                WordFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                PdfFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                GeneratedBy = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                GeneratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GeneratedDocuments", x => x.Id);
                table.ForeignKey(
                    name: "FK_GeneratedDocuments_CompanyClients_CompanyClientId",
                    column: x => x.CompanyClientId,
                    principalTable: "CompanyClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_GeneratedDocuments_DocumentTemplates_DocumentTemplateId",
                    column: x => x.DocumentTemplateId,
                    principalTable: "DocumentTemplates",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "GeneratedDocumentValues",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                GeneratedDocumentId = table.Column<int>(type: "integer", nullable: false),
                FieldKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                FieldValue = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GeneratedDocumentValues", x => x.Id);
                table.ForeignKey(
                    name: "FK_GeneratedDocumentValues_GeneratedDocuments_GeneratedDocumentId",
                    column: x => x.GeneratedDocumentId,
                    principalTable: "GeneratedDocuments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_DocumentTemplateFields_DocumentTemplateId",
            table: "DocumentTemplateFields",
            column: "DocumentTemplateId");

        migrationBuilder.CreateIndex(
            name: "IX_GeneratedDocuments_CompanyClientId",
            table: "GeneratedDocuments",
            column: "CompanyClientId");

        migrationBuilder.CreateIndex(
            name: "IX_GeneratedDocuments_DocumentTemplateId",
            table: "GeneratedDocuments",
            column: "DocumentTemplateId");

        migrationBuilder.CreateIndex(
            name: "IX_GeneratedDocumentValues_GeneratedDocumentId",
            table: "GeneratedDocumentValues",
            column: "GeneratedDocumentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "DocumentTemplateFields");
        migrationBuilder.DropTable(name: "GeneratedDocumentValues");
        migrationBuilder.DropTable(name: "GeneratedDocuments");
        migrationBuilder.DropTable(name: "DocumentTemplates");

        migrationBuilder.DropColumn(name: "AuthorizedCapital", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "City", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "FinancialYearEnd", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "FinancialYearStart", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "PaidUpCapital", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "PinCode", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "RegistrationNumber", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "State", table: "CompanyClients");
        migrationBuilder.DropColumn(name: "Address", table: "Directors");
        migrationBuilder.DropColumn(name: "Designation", table: "Directors");
    }
}
