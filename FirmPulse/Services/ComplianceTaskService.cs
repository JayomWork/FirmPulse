using FirmPulse.Data;
using FirmPulse.Entities;
using FirmPulse.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class ComplianceTaskService(FirmPulseDbContext context) : IComplianceTaskService
{
    public async Task<List<ComplianceTask>> GetAllAsync()
    {
        return await context.ComplianceTasks
            .Include(x => x.CompanyClient)
            .Where(x => x.IsActive && x.CompanyClient != null && x.CompanyClient.IsActive)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<ComplianceTask?> GetByIdAsync(int id)
    {
        return await context.ComplianceTasks
            .Include(x => x.CompanyClient)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public async Task<List<ComplianceTask>> GetByCompanyIdAsync(int companyClientId)
    {
        return await context.ComplianceTasks
            .Include(x => x.CompanyClient)
            .Where(x => x.IsActive && x.CompanyClientId == companyClientId && x.CompanyClient != null && x.CompanyClient.IsActive)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<ComplianceTask>> GetUpcomingAsync()
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
            .ToListAsync();
    }

    public async Task<List<ComplianceTask>> GetOverdueAsync()
    {
        var tasks = await context.ComplianceTasks
            .Include(x => x.CompanyClient)
            .Where(x => x.IsActive && x.CompanyClient != null && x.CompanyClient.IsActive)
            .OrderBy(x => x.DueDate)
            .ToListAsync();

        return tasks.Where(ComplianceTaskHelper.IsOverdue).ToList();
    }

    public async Task<ComplianceTask> CreateAsync(ComplianceTask entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsActive = true;

        context.ComplianceTasks.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(ComplianceTask entity)
    {
        var existing = await context.ComplianceTasks.FirstOrDefaultAsync(x => x.Id == entity.Id && x.IsActive);
        if (existing is null)
        {
            return;
        }

        existing.FirmId = entity.FirmId;
        existing.CompanyClientId = entity.CompanyClientId;
        existing.Title = entity.Title;
        existing.Description = entity.Description;
        existing.ComplianceType = entity.ComplianceType;
        existing.DueDate = entity.DueDate;
        existing.Status = entity.Status;
        existing.Priority = entity.Priority;
        existing.AssignedTo = entity.AssignedTo;
        existing.CompletedDate = entity.CompletedDate;
        existing.Remarks = entity.Remarks;
        existing.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await context.ComplianceTasks.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        if (existing is null)
        {
            return;
        }

        existing.IsActive = false;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task MarkCompletedAsync(int id)
    {
        var existing = await context.ComplianceTasks.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        if (existing is null)
        {
            return;
        }

        existing.Status = ComplianceTaskStatuses.Completed;
        existing.CompletedDate = DateOnly.FromDateTime(DateTime.Today);
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}
