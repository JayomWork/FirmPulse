using FirmPulse.Data;
using FirmPulse.Entities;
using FirmPulse.Helpers;
using FirmPulse.Models;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class DashboardService(FirmPulseDbContext context) : IDashboardService
{
    public async Task<DashboardSummaryModel> GetSummaryAsync()
    {
        var companies = await context.CompanyClients
            .Where(x => x.IsActive)
            .ToListAsync();

        var tasks = await context.ComplianceTasks
            .Include(x => x.CompanyClient)
            .Where(x => x.CompanyClient != null && x.CompanyClient.IsActive)
            .ToListAsync();

        var invoices = await context.Invoices
            .Include(x => x.CompanyClient)
            .Include(x => x.Payments)
            .Where(x => x.CompanyClient != null && x.CompanyClient.IsActive)
            .ToListAsync();

        var today = DateOnly.FromDateTime(DateTime.Today);
        var upcomingThreshold = today.AddDays(30);

        var overdueTasks = tasks.Count(ComplianceTaskHelper.IsOverdue);
        var pendingTasks = tasks.Count(x =>
            !ComplianceTaskHelper.IsOverdue(x) &&
            x.Status is not ComplianceTaskStatuses.Completed and not ComplianceTaskStatuses.Filed);
        var upcomingTasks = tasks.Count(x =>
            x.DueDate >= today &&
            x.DueDate <= upcomingThreshold &&
            x.Status is not ComplianceTaskStatuses.Completed and not ComplianceTaskStatuses.Filed);

        var paidTotal = invoices.Sum(x => x.Payments.Sum(p => p.Amount));
        var unpaidTotal = invoices.Sum(x =>
        {
            var paid = x.Payments.Sum(p => p.Amount);
            return Math.Max(0m, x.TotalAmount - paid);
        });

        return new DashboardSummaryModel
        {
            TotalCompanies = companies.Count,
            PendingTasks = pendingTasks,
            OverdueTasks = overdueTasks,
            UpcomingTasks = upcomingTasks,
            UnpaidInvoiceTotal = unpaidTotal,
            PaidInvoiceTotal = paidTotal
        };
    }

    public async Task<List<ComplianceTask>> GetUpcomingTasksAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var threshold = today.AddDays(30);

        return await context.ComplianceTasks
            .Include(x => x.CompanyClient)
            .Where(x =>
                x.CompanyClient != null &&
                x.CompanyClient.IsActive &&
                x.DueDate >= today &&
                x.DueDate <= threshold &&
                x.Status != ComplianceTaskStatuses.Completed &&
                x.Status != ComplianceTaskStatuses.Filed)
            .OrderBy(x => x.DueDate)
            .Take(10)
            .ToListAsync();
    }
}
