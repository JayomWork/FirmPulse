using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Seed;

public static class FirmPulseSeedData
{
    public static async Task SeedAsync(FirmPulseDbContext context)
    {
        if (await context.Firms.AnyAsync())
        {
            return;
        }

        var firm = new Firm
        {
            Name = "Apex Secretarial Services",
            Email = "admin@apexsecretarial.in",
            Phone = "+91 98765 43210",
            Address = "Banjara Hills, Hyderabad, Telangana",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var companies = new List<CompanyClient>
        {
            new()
            {
                Firm = firm,
                CompanyName = "Bluewave Technologies Private Limited",
                CIN = "U72900TG2020PTC123456",
                PAN = "AAECB1234F",
                IncorporationDate = new DateOnly(2020, 4, 12),
                RegisteredOfficeAddress = "Madhapur, Hyderabad, Telangana",
                CompanyType = "Private Limited",
                Status = "Active",
                Email = "compliance@bluewave.in",
                Phone = "+91 90000 11111",
                ContactPersonName = "Rohit Sharma"
            },
            new()
            {
                Firm = firm,
                CompanyName = "Greenleaf Ventures LLP",
                CIN = "AAX-7890",
                PAN = "AALFG2345K",
                IncorporationDate = new DateOnly(2021, 8, 2),
                RegisteredOfficeAddress = "Andheri East, Mumbai, Maharashtra",
                CompanyType = "LLP",
                Status = "Active",
                Email = "accounts@greenleaf.in",
                Phone = "+91 90123 45678",
                ContactPersonName = "Nisha Patel"
            },
            new()
            {
                Firm = firm,
                CompanyName = "Vertex Retail Limited",
                CIN = "L52100KA2018PLC654321",
                PAN = "AACCV3456L",
                IncorporationDate = new DateOnly(2018, 11, 19),
                RegisteredOfficeAddress = "Indiranagar, Bengaluru, Karnataka",
                CompanyType = "Public Limited",
                Status = "Active",
                Email = "legal@vertexretail.in",
                Phone = "+91 99887 66554",
                ContactPersonName = "Meera Iyer"
            }
        };

        var directors = new List<Director>
        {
            new() { CompanyClient = companies[0], FullName = "Amit Verma", DIN = "01234567", PAN = "ABCPV1234D", Email = "amit.verma@bluewave.in", Phone = "+91 90001 11111", AppointmentDate = new DateOnly(2020, 4, 12) },
            new() { CompanyClient = companies[0], FullName = "Neha Joshi", DIN = "07654321", PAN = "ADEPJ5678R", Email = "neha.joshi@bluewave.in", Phone = "+91 90002 22222", AppointmentDate = new DateOnly(2021, 3, 10) },
            new() { CompanyClient = companies[1], FullName = "Karan Mehta", DIN = "04561234", PAN = "AFCPM4567Q", Email = "karan@greenleaf.in", Phone = "+91 90123 98765", AppointmentDate = new DateOnly(2021, 8, 2) },
            new() { CompanyClient = companies[2], FullName = "Sonal Rao", DIN = "09876123", PAN = "ACDPR6789N", Email = "sonal@vertexretail.in", Phone = "+91 99887 33445", AppointmentDate = new DateOnly(2018, 11, 19) }
        };

        var tasks = new List<ComplianceTask>
        {
            new() { Firm = firm, CompanyClient = companies[0], Title = "Board Meeting Minutes Filing", Description = "Prepare and file minutes for Q1 board meeting.", ComplianceType = "Board Meeting", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7)), Status = ComplianceTaskStatuses.Pending, Priority = ComplianceTaskPriorities.High, AssignedTo = "Priya" },
            new() { Firm = firm, CompanyClient = companies[0], Title = "DIR-3 KYC", Description = "Complete annual director KYC submission.", ComplianceType = "MCA Filing", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)), Status = ComplianceTaskStatuses.InProgress, Priority = ComplianceTaskPriorities.Critical, AssignedTo = "Ravi" },
            new() { Firm = firm, CompanyClient = companies[1], Title = "LLP Form 11 Filing", Description = "Annual return filing for LLP.", ComplianceType = "Annual Filing", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(18)), Status = ComplianceTaskStatuses.WaitingClient, Priority = ComplianceTaskPriorities.Medium, AssignedTo = "Anita" },
            new() { Firm = firm, CompanyClient = companies[2], Title = "MSME Half-Year Return", Description = "Check vendor ageing and submit return.", ComplianceType = "ROC Compliance", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(28)), Status = ComplianceTaskStatuses.Pending, Priority = ComplianceTaskPriorities.Low, AssignedTo = "Suresh" },
            new() { Firm = firm, CompanyClient = companies[2], Title = "Annual General Meeting Filing", Description = "Upload AGM resolutions and related forms.", ComplianceType = "AGM", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-12)), Status = ComplianceTaskStatuses.Completed, Priority = ComplianceTaskPriorities.High, AssignedTo = "Priya", CompletedDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)), Remarks = "Filed successfully." }
        };

        var invoices = new List<Invoice>
        {
            new() { Firm = firm, CompanyClient = companies[0], InvoiceNumber = "FP-2026-001", InvoiceDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-20)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(10)), Amount = 15000m, TaxAmount = 2700m, TotalAmount = 17700m, Status = InvoiceStatuses.Sent, Remarks = "Quarterly secretarial support" },
            new() { Firm = firm, CompanyClient = companies[1], InvoiceNumber = "FP-2026-002", InvoiceDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-40)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), Amount = 22000m, TaxAmount = 3960m, TotalAmount = 25960m, Status = InvoiceStatuses.Overdue, Remarks = "Annual LLP compliance package" },
            new() { Firm = firm, CompanyClient = companies[2], InvoiceNumber = "FP-2026-003", InvoiceDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-15)), DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(15)), Amount = 32000m, TaxAmount = 5760m, TotalAmount = 37760m, Status = InvoiceStatuses.Paid, Remarks = "AGM and secretarial retainer" }
        };

        var payments = new List<Payment>
        {
            new() { Invoice = invoices[0], PaymentDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), Amount = 5000m, PaymentMode = "Bank Transfer", ReferenceNumber = "UTR10001", Remarks = "Advance received" },
            new() { Invoice = invoices[2], PaymentDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)), Amount = 37760m, PaymentMode = "NEFT", ReferenceNumber = "UTR10002", Remarks = "Paid in full" }
        };

        context.Add(firm);
        context.AddRange(companies);
        context.AddRange(directors);
        context.AddRange(tasks);
        context.AddRange(invoices);
        context.AddRange(payments);

        await context.SaveChangesAsync();
    }
}
