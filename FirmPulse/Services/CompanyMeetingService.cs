using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class CompanyMeetingService(FirmPulseDbContext context) : ICompanyMeetingService
{
    public async Task<List<CompanyMeeting>> GetAllAsync()
    {
        return await context.CompanyMeetings
            .Include(x => x.CompanyClient)
            .OrderBy(x => x.DueDate)
            .ThenBy(x => x.MeetingDate)
            .ToListAsync();
    }

    public async Task<List<CompanyMeeting>> GetByCompanyIdAsync(int companyClientId)
    {
        return await context.CompanyMeetings
            .Where(x => x.CompanyClientId == companyClientId)
            .OrderByDescending(x => x.MeetingDate)
            .ToListAsync();
    }

    public async Task<CompanyMeeting?> GetByIdAsync(int id)
    {
        return await context.CompanyMeetings.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CompanyMeeting> CreateAsync(CompanyMeeting entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        context.CompanyMeetings.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(CompanyMeeting entity)
    {
        var existing = await context.CompanyMeetings.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (existing is null)
        {
            return;
        }

        existing.CompanyCompliancePlanId = entity.CompanyCompliancePlanId;
        existing.MeetingType = entity.MeetingType;
        existing.Title = entity.Title;
        existing.MeetingDate = entity.MeetingDate;
        existing.DueDate = entity.DueDate;
        existing.VenueOrMode = entity.VenueOrMode;
        existing.NoticePrepared = entity.NoticePrepared;
        existing.AgendaPrepared = entity.AgendaPrepared;
        existing.MinutesPrepared = entity.MinutesPrepared;
        existing.SignedCopyReceived = entity.SignedCopyReceived;
        existing.Status = entity.Status;
        existing.Remarks = entity.Remarks;
        existing.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task MarkCompletedAsync(int id)
    {
        var existing = await context.CompanyMeetings.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null)
        {
            return;
        }

        existing.Status = CompanyMeetingStatuses.Completed;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}
