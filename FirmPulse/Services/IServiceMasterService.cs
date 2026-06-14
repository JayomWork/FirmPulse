using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IServiceMasterService
{
    Task<List<ServiceCategory>> GetCategoriesAsync();
    Task<List<ServiceMaster>> GetServicesAsync();
    Task<List<ServiceMaster>> GetActiveServicesAsync();
    Task<ServiceMaster> CreateServiceAsync(ServiceMaster entity);
    Task UpdateServiceAsync(ServiceMaster entity);
}
