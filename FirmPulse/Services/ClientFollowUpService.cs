using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class ClientFollowUpService(FirmPulseDbContext context) : IClientFollowUpService
{
    public async Task<List<ClientFollowUp>> GetByCompanyIdAsync(int companyClientId)
    {
        return await context.ClientFollowUps
            .Where(x => x.CompanyClientId == companyClientId)
            .OrderByDescending(x => x.FollowUpDate)
            .ToListAsync();
    }

    public async Task<List<ClientFollowUp>> GetByWorkItemIdAsync(int workItemId)
    {
        return await context.ClientFollowUps
            .Where(x => x.WorkItemId == workItemId)
            .OrderByDescending(x => x.FollowUpDate)
            .ToListAsync();
    }

    public async Task<ClientFollowUp> CreateAsync(ClientFollowUp entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        context.ClientFollowUps.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task MarkDoneAsync(int id)
    {
        var existing = await context.ClientFollowUps.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null)
        {
            return;
        }

        existing.Status = ClientFollowUpStatuses.Done;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}
