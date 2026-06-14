using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FirmPulse.Data.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Firms",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Firms", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CompanyClients",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirmId = table.Column<int>(type: "integer", nullable: false),
                CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                CIN = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: true),
                PAN = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                IncorporationDate = table.Column<DateOnly>(type: "date", nullable: true),
                RegisteredOfficeAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                CompanyType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                Status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                ContactPersonName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CompanyClients", x => x.Id);
                table.ForeignKey(
                    name: "FK_CompanyClients_Firms_FirmId",
                    column: x => x.FirmId,
                    principalTable: "Firms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "ComplianceTasks",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirmId = table.Column<int>(type: "integer", nullable: false),
                CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                ComplianceType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                AssignedTo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                CompletedDate = table.Column<DateOnly>(type: "date", nullable: true),
                Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ComplianceTasks", x => x.Id);
                table.ForeignKey(
                    name: "FK_ComplianceTasks_CompanyClients_CompanyClientId",
                    column: x => x.CompanyClientId,
                    principalTable: "CompanyClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ComplianceTasks_Firms_FirmId",
                    column: x => x.FirmId,
                    principalTable: "Firms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Directors",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                DIN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                PAN = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                AppointmentDate = table.Column<DateOnly>(type: "date", nullable: true),
                ResignationDate = table.Column<DateOnly>(type: "date", nullable: true),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Directors", x => x.Id);
                table.ForeignKey(
                    name: "FK_Directors_CompanyClients_CompanyClientId",
                    column: x => x.CompanyClientId,
                    principalTable: "CompanyClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Invoices",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirmId = table.Column<int>(type: "integer", nullable: false),
                CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                InvoiceNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                InvoiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                TaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                TotalAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Invoices", x => x.Id);
                table.ForeignKey(
                    name: "FK_Invoices_CompanyClients_CompanyClientId",
                    column: x => x.CompanyClientId,
                    principalTable: "CompanyClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Invoices_Firms_FirmId",
                    column: x => x.FirmId,
                    principalTable: "Firms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "DocumentRecords",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirmId = table.Column<int>(type: "integer", nullable: false),
                CompanyClientId = table.Column<int>(type: "integer", nullable: false),
                ComplianceTaskId = table.Column<int>(type: "integer", nullable: true),
                DocumentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                DocumentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                FilePath = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentRecords", x => x.Id);
                table.ForeignKey(
                    name: "FK_DocumentRecords_ComplianceTasks_ComplianceTaskId",
                    column: x => x.ComplianceTaskId,
                    principalTable: "ComplianceTasks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
                table.ForeignKey(
                    name: "FK_DocumentRecords_CompanyClients_CompanyClientId",
                    column: x => x.CompanyClientId,
                    principalTable: "CompanyClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DocumentRecords_Firms_FirmId",
                    column: x => x.FirmId,
                    principalTable: "Firms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Payments",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                InvoiceId = table.Column<int>(type: "integer", nullable: false),
                PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                PaymentMode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                ReferenceNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Payments_Invoices_InvoiceId",
                    column: x => x.InvoiceId,
                    principalTable: "Invoices",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CompanyClients_FirmId",
            table: "CompanyClients",
            column: "FirmId");

        migrationBuilder.CreateIndex(
            name: "IX_ComplianceTasks_CompanyClientId",
            table: "ComplianceTasks",
            column: "CompanyClientId");

        migrationBuilder.CreateIndex(
            name: "IX_ComplianceTasks_FirmId",
            table: "ComplianceTasks",
            column: "FirmId");

        migrationBuilder.CreateIndex(
            name: "IX_Directors_CompanyClientId",
            table: "Directors",
            column: "CompanyClientId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentRecords_ComplianceTaskId",
            table: "DocumentRecords",
            column: "ComplianceTaskId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentRecords_CompanyClientId",
            table: "DocumentRecords",
            column: "CompanyClientId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentRecords_FirmId",
            table: "DocumentRecords",
            column: "FirmId");

        migrationBuilder.CreateIndex(
            name: "IX_Invoices_CompanyClientId",
            table: "Invoices",
            column: "CompanyClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Invoices_FirmId",
            table: "Invoices",
            column: "FirmId");

        migrationBuilder.CreateIndex(
            name: "IX_Payments_InvoiceId",
            table: "Payments",
            column: "InvoiceId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Directors");
        migrationBuilder.DropTable(name: "DocumentRecords");
        migrationBuilder.DropTable(name: "Payments");
        migrationBuilder.DropTable(name: "ComplianceTasks");
        migrationBuilder.DropTable(name: "Invoices");
        migrationBuilder.DropTable(name: "CompanyClients");
        migrationBuilder.DropTable(name: "Firms");
    }
}
