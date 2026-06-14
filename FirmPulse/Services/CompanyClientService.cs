using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class CompanyClientService(FirmPulseDbContext context) : ICompanyClientService
{
    public async Task<List<CompanyClient>> GetAllAsync()
    {
        return await context.CompanyClients
            .Include(x => x.Directors)
            .Include(x => x.ComplianceTasks)
            .Include(x => x.CompanyCompliancePlans)
            .Include(x => x.WorkItems)
            .Include(x => x.FilingRecords)
            .Include(x => x.CompanyMeetings)
            .Include(x => x.ClientFollowUps)
            .Include(x => x.Invoices)
                .ThenInclude(x => x.Payments)
            .Where(x => x.IsActive)
            .OrderBy(x => x.CompanyName)
            .ToListAsync();
    }

    public async Task<CompanyClient?> GetByIdAsync(int id)
    {
        return await context.CompanyClients
            .Include(x => x.Firm)
            .Include(x => x.Directors.OrderBy(d => d.FullName))
            .Include(x => x.ComplianceTasks.OrderBy(t => t.DueDate))
            .Include(x => x.DocumentRecords.OrderByDescending(d => d.UploadedAt))
            .Include(x => x.CompanyCompliancePlans.OrderByDescending(p => p.FinancialYear))
                .ThenInclude(x => x.ComplianceTemplate)
            .Include(x => x.WorkItems.OrderBy(w => w.DueDate))
                .ThenInclude(x => x.McaFormMaster)
            .Include(x => x.FilingRecords.OrderByDescending(f => f.DueDate))
                .ThenInclude(x => x.McaFormMaster)
            .Include(x => x.CompanyMeetings.OrderBy(m => m.MeetingDate))
            .Include(x => x.ClientFollowUps.OrderByDescending(f => f.FollowUpDate))
            .Include(x => x.Invoices.OrderByDescending(i => i.InvoiceDate))
                .ThenInclude(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public async Task<CompanyClient> CreateAsync(CompanyClient entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        context.CompanyClients.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(CompanyClient entity)
    {
        var existing = await context.CompanyClients.FirstOrDefaultAsync(x => x.Id == entity.Id && x.IsActive);
        if (existing is null)
        {
            return;
        }

        existing.CompanyName = entity.CompanyName;
        existing.CIN = entity.CIN;
        existing.PAN = entity.PAN;
        existing.IncorporationDate = entity.IncorporationDate;
        existing.RegisteredOfficeAddress = entity.RegisteredOfficeAddress;
        existing.CompanyType = entity.CompanyType;
        existing.Status = entity.Status;
        existing.Email = entity.Email;
        existing.Phone = entity.Phone;
        existing.ContactPersonName = entity.ContactPersonName;
        existing.FirmId = entity.FirmId;
        existing.FinancialYearEndMonth = entity.FinancialYearEndMonth;
        existing.FinancialYearEndDay = entity.FinancialYearEndDay;
        existing.LastAGMDate = entity.LastAGMDate;
        existing.CompanyClass = entity.CompanyClass;
        existing.IsListed = entity.IsListed;
        existing.HasShareCapital = entity.HasShareCapital;
        existing.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await context.CompanyClients.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        if (existing is null)
        {
            return;
        }

        existing.IsActive = false;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}
