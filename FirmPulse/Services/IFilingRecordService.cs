using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IFilingRecordService
{
    Task<List<FilingRecord>> GetAllAsync();
    Task<List<FilingRecord>> GetByCompanyIdAsync(int companyClientId);
    Task<List<FilingRecord>> GetByWorkItemIdAsync(int workItemId);
    Task<FilingRecord?> GetByIdAsync(int id);
    Task<FilingRecord> CreateAsync(FilingRecord entity);
    Task UpdateAsync(FilingRecord entity);
    Task UpdateSrnAndChallanAsync(int id, string? srn, string? challanNumber, decimal? challanAmount, DateOnly? filingDate, string mcaStatus, string? remarks);
}
