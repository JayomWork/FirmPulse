using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmPulse.Data.Migrations;

public partial class AddSoftDeleteForTasksAndInvoices : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "Invoices",
            type: "boolean",
            nullable: false,
            defaultValue: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "ComplianceTasks",
            type: "boolean",
            nullable: false,
            defaultValue: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "Invoices");

        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "ComplianceTasks");
    }
}
