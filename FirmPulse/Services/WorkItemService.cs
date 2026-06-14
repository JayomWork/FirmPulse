using FirmPulse.Data;
using FirmPulse.Entities;
using FirmPulse.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class WorkItemService(FirmPulseDbContext context) : IWorkItemService
{
    public async Task<List<WorkItem>> GetAllAsync()
    {
        return await BaseQuery()
            .OrderBy(x => x.DueDate)
            .ThenBy(x => x.Title)
            .ToListAsync();
    }

    public async Task<WorkItem?> GetByIdAsync(int id)
    {
        return await BaseQuery()
            .Include(x => x.StatusHistory.OrderByDescending(s => s.ChangedAt))
            .Include(x => x.Documents.OrderBy(d => d.DocumentName))
            .Include(x => x.FilingRecords)
                .ThenInclude(x => x.McaFormMaster)
            .Include(x => x.FollowUps.OrderByDescending(f => f.FollowUpDate))
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<WorkItem>> GetByCompanyIdAsync(int companyClientId)
    {
        return await BaseQuery()
            .Where(x => x.CompanyClientId == companyClientId)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<WorkItem>> GetDueThisWeekAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var end = today.AddDays(7);
        return await BaseQuery()
            .Where(x => x.DueDate >= today && x.DueDate <= end && x.Status != WorkItemStatuses.Completed && x.Status != WorkItemStatuses.Cancelled)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<WorkItem>> GetDueThisMonthAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var end = today.AddDays(30);
        return await BaseQuery()
            .Where(x => x.DueDate >= today && x.DueDate <= end && x.Status != WorkItemStatuses.Completed && x.Status != WorkItemStatuses.Cancelled)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<WorkItem>> GetOverdueAsync()
    {
        var items = await BaseQuery().OrderBy(x => x.DueDate).ToListAsync();
        return items.Where(WorkItemHelper.IsOverdue).ToList();
    }

    public async Task<List<WorkItem>> GetPendingClientDocumentsAsync()
    {
        return await BaseQuery()
            .Where(x => x.RequiresClientDocuments && x.ClientDocumentsStatus != WorkItemClientDocumentStatuses.NotRequired && x.ClientDocumentsStatus != WorkItemClientDocumentStatuses.Reviewed)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<WorkItem> CreateAsync(WorkItem entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        context.WorkItems.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(WorkItem entity)
    {
        var existing = await context.WorkItems.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (existing is null)
        {
            return;
        }

        existing.ServiceMasterId = entity.ServiceMasterId;
        existing.McaFormMasterId = entity.McaFormMasterId;
        existing.Title = entity.Title;
        existing.Description = entity.Description;
        existing.FinancialYear = entity.FinancialYear;
        existing.DueDate = entity.DueDate;
        existing.Priority = entity.Priority;
        existing.AssignedTo = entity.AssignedTo;
        existing.RequiresClientDocuments = entity.RequiresClientDocuments;
        existing.ClientDocumentsStatus = entity.ClientDocumentsStatus;
        existing.RequiresMcaFiling = entity.RequiresMcaFiling;
        existing.FilingStatus = entity.FilingStatus;
        existing.Remarks = entity.Remarks;
        existing.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int workItemId, string newStatus, string remarks)
    {
        var existing = await context.WorkItems.FirstOrDefaultAsync(x => x.Id == workItemId);
        if (existing is null || existing.Status == newStatus)
        {
            return;
        }

        var oldStatus = existing.Status;
        existing.Status = newStatus;
        existing.UpdatedAt = DateTime.UtcNow;
        existing.CompletedDate = newStatus == WorkItemStatuses.Completed ? DateOnly.FromDateTime(DateTime.Today) : existing.CompletedDate;

        context.WorkItemStatusHistories.Add(new WorkItemStatusHistory
        {
            WorkItemId = workItemId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            Remarks = remarks,
            ChangedBy = "System",
            ChangedAt = DateTime.UtcNow
        });

        await context.SaveChangesAsync();
    }

    public Task MarkWaitingClientAsync(int workItemId) => ChangeStatusAsync(workItemId, WorkItemStatuses.WaitingClient, "Waiting on client inputs.");
    public Task MarkReadyForFilingAsync(int workItemId) => ChangeStatusAsync(workItemId, WorkItemStatuses.ReadyForFiling, "Ready for filing.");
    public Task MarkFiledAsync(int workItemId) => ChangeStatusAsync(workItemId, WorkItemStatuses.Filed, "Work item marked as filed.");
    public Task MarkCompletedAsync(int workItemId) => ChangeStatusAsync(workItemId, WorkItemStatuses.Completed, "Work item marked as completed.");

    private IQueryable<WorkItem> BaseQuery()
    {
        return context.WorkItems
            .Include(x => x.CompanyClient)
            .Include(x => x.ServiceMaster)
            .Include(x => x.McaFormMaster)
            .Include(x => x.CompanyCompliancePlan)
            .Where(x => x.CompanyClient != null && x.CompanyClient.IsActive);
    }
}
