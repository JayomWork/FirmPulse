using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface ICompanyMeetingService
{
    Task<List<CompanyMeeting>> GetAllAsync();
    Task<List<CompanyMeeting>> GetByCompanyIdAsync(int companyClientId);
    Task<CompanyMeeting?> GetByIdAsync(int id);
    Task<CompanyMeeting> CreateAsync(CompanyMeeting entity);
    Task UpdateAsync(CompanyMeeting entity);
    Task MarkCompletedAsync(int id);
}
