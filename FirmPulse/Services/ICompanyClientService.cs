using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface ICompanyClientService
{
    Task<List<CompanyClient>> GetAllAsync();
    Task<CompanyClient?> GetByIdAsync(int id);
    Task<CompanyClient> CreateAsync(CompanyClient entity);
    Task UpdateAsync(CompanyClient entity);
    Task DeleteAsync(int id);
}
