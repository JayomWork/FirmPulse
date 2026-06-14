using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class WorkItemDocumentService(FirmPulseDbContext context) : IWorkItemDocumentService
{
    public async Task<List<WorkItemDocument>> GetByWorkItemIdAsync(int workItemId)
    {
        return await context.WorkItemDocuments
            .Where(x => x.WorkItemId == workItemId)
            .OrderBy(x => x.DocumentName)
            .ToListAsync();
    }

    public async Task UpdateStatusAsync(int id, string status, string? remarks)
    {
        var existing = await context.WorkItemDocuments.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null)
        {
            return;
        }

        existing.Status = status;
        existing.Remarks = remarks;
        existing.ReceivedDate = status == WorkItemDocumentStatuses.Received ? DateOnly.FromDateTime(DateTime.Today) : existing.ReceivedDate;
        existing.ReviewedDate = status == WorkItemDocumentStatuses.Reviewed ? DateOnly.FromDateTime(DateTime.Today) : existing.ReviewedDate;
        await context.SaveChangesAsync();
    }

    public async Task<WorkItemDocument> AddDocumentAsync(WorkItemDocument entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        context.WorkItemDocuments.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
