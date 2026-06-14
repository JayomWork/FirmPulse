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
            .Where(x => x.IsActive && x.CompanyClient != null && x.CompanyClient.IsActive)
            .ToListAsync();

        var invoices = await context.Invoices
            .Include(x => x.CompanyClient)
            .Include(x => x.Payments)
            .Where(x => x.IsActive && x.CompanyClient != null && x.CompanyClient.IsActive)
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
                x.IsActive &&
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

    public async Task<CsDashboardSummaryModel> GetCsDashboardSummaryAsync()
    {
        var companies = await context.CompanyClients.Where(x => x.IsActive).ToListAsync();
        var workItems = await context.WorkItems.Include(x => x.CompanyClient).Where(x => x.CompanyClient != null && x.CompanyClient.IsActive).ToListAsync();
        var meetings = await context.CompanyMeetings.Include(x => x.CompanyClient).Where(x => x.CompanyClient != null && x.CompanyClient.IsActive).ToListAsync();
        var invoices = await context.Invoices.Include(x => x.CompanyClient).Include(x => x.Payments).Where(x => x.IsActive && x.CompanyClient != null && x.CompanyClient.IsActive).ToListAsync();

        var today = DateOnly.FromDateTime(DateTime.Today);
        var week = today.AddDays(7);
        var month = today.AddDays(30);
        var monthStart = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, 1);

        return new CsDashboardSummaryModel
        {
            TotalActiveCompanies = companies.Count,
            DueThisWeekCount = workItems.Count(x => x.DueDate >= today && x.DueDate <= week && x.Status is not WorkItemStatuses.Completed and not WorkItemStatuses.Cancelled),
            DueThisMonthCount = workItems.Count(x => x.DueDate >= today && x.DueDate <= month && x.Status is not WorkItemStatuses.Completed and not WorkItemStatuses.Cancelled),
            OverdueWorkItemsCount = workItems.Count(WorkItemHelper.IsOverdue),
            PendingClientDocumentsCount = workItems.Count(x => x.RequiresClientDocuments && x.ClientDocumentsStatus is WorkItemClientDocumentStatuses.Pending or WorkItemClientDocumentStatuses.PartiallyReceived),
            PendingMcaFilingsCount = workItems.Count(x => x.RequiresMcaFiling && x.FilingStatus == WorkItemFilingStatuses.Pending),
            FiledThisMonthCount = workItems.Count(x => x.CompletedDate >= monthStart && x.Status == WorkItemStatuses.Filed),
            UpcomingAgmCount = meetings.Count(x => x.MeetingType == CompanyMeetingTypes.AGM && x.MeetingDate >= today && x.MeetingDate <= month),
            UpcomingBoardMeetingCount = meetings.Count(x => x.MeetingType == CompanyMeetingTypes.BoardMeeting && x.MeetingDate >= today && x.MeetingDate <= month),
            UnpaidInvoiceTotal = invoices.Sum(x => Math.Max(0m, x.TotalAmount - x.Payments.Sum(p => p.Amount)))
        };
    }

    public async Task<List<WorkItem>> GetUpcomingWorkItemsAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var month = today.AddDays(30);
        return await context.WorkItems
            .Include(x => x.CompanyClient)
            .Include(x => x.McaFormMaster)
            .Where(x => x.DueDate >= today && x.DueDate <= month && x.CompanyClient != null && x.CompanyClient.IsActive)
            .OrderBy(x => x.DueDate)
            .Take(10)
            .ToListAsync();
    }

    public async Task<List<WorkItem>> GetOverdueWorkItemsAsync()
    {
        var items = await context.WorkItems
            .Include(x => x.CompanyClient)
            .Include(x => x.McaFormMaster)
            .Where(x => x.CompanyClient != null && x.CompanyClient.IsActive)
            .OrderBy(x => x.DueDate)
            .Take(20)
            .ToListAsync();

        return items.Where(WorkItemHelper.IsOverdue).ToList();
    }

    public async Task<List<WorkItem>> GetPendingDocumentWorkItemsAsync()
    {
        return await context.WorkItems
            .Include(x => x.CompanyClient)
            .Where(x =>
                x.CompanyClient != null &&
                x.CompanyClient.IsActive &&
                x.RequiresClientDocuments &&
                (x.ClientDocumentsStatus == WorkItemClientDocumentStatuses.Pending ||
                 x.ClientDocumentsStatus == WorkItemClientDocumentStatuses.PartiallyReceived))
            .OrderBy(x => x.DueDate)
            .Take(10)
            .ToListAsync();
    }

    public async Task<List<CompanyMeeting>> GetUpcomingMeetingsAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var month = today.AddDays(30);
        return await context.CompanyMeetings
            .Include(x => x.CompanyClient)
            .Where(x => x.CompanyClient != null && x.CompanyClient.IsActive && x.MeetingDate >= today && x.MeetingDate <= month)
            .OrderBy(x => x.MeetingDate)
            .Take(10)
            .ToListAsync();
    }
}
