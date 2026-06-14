using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class FilingRecordService(FirmPulseDbContext context) : IFilingRecordService
{
    public async Task<List<FilingRecord>> GetAllAsync()
    {
        return await Query().OrderBy(x => x.DueDate).ToListAsync();
    }

    public async Task<List<FilingRecord>> GetByCompanyIdAsync(int companyClientId)
    {
        return await Query().Where(x => x.CompanyClientId == companyClientId).OrderByDescending(x => x.DueDate).ToListAsync();
    }

    public async Task<List<FilingRecord>> GetByWorkItemIdAsync(int workItemId)
    {
        return await Query().Where(x => x.WorkItemId == workItemId).OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<FilingRecord?> GetByIdAsync(int id)
    {
        return await Query().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<FilingRecord> CreateAsync(FilingRecord entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        context.FilingRecords.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(FilingRecord entity)
    {
        var existing = await context.FilingRecords.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (existing is null)
        {
            return;
        }

        existing.McaFormMasterId = entity.McaFormMasterId;
        existing.FinancialYear = entity.FinancialYear;
        existing.DueDate = entity.DueDate;
        existing.FilingDate = entity.FilingDate;
        existing.SRN = entity.SRN;
        existing.ChallanNumber = entity.ChallanNumber;
        existing.ChallanAmount = entity.ChallanAmount;
        existing.McaStatus = entity.McaStatus;
        existing.AcknowledgementFilePath = entity.AcknowledgementFilePath;
        existing.Remarks = entity.Remarks;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task UpdateSrnAndChallanAsync(int id, string? srn, string? challanNumber, decimal? challanAmount, DateOnly? filingDate, string mcaStatus, string? remarks)
    {
        var existing = await context.FilingRecords.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null)
        {
            return;
        }

        existing.SRN = srn;
        existing.ChallanNumber = challanNumber;
        existing.ChallanAmount = challanAmount;
        existing.FilingDate = filingDate;
        existing.McaStatus = mcaStatus;
        existing.Remarks = remarks;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    private IQueryable<FilingRecord> Query()
    {
        return context.FilingRecords
            .Include(x => x.CompanyClient)
            .Include(x => x.McaFormMaster)
            .Include(x => x.WorkItem);
    }
}
