using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IClientFollowUpService
{
    Task<List<ClientFollowUp>> GetByCompanyIdAsync(int companyClientId);
    Task<List<ClientFollowUp>> GetByWorkItemIdAsync(int workItemId);
    Task<ClientFollowUp> CreateAsync(ClientFollowUp entity);
    Task MarkDoneAsync(int id);
}
