using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IInvoiceService
{
    Task<List<Invoice>> GetAllAsync();
    Task<List<Invoice>> GetByCompanyIdAsync(int companyClientId);
    Task<Invoice?> GetByIdAsync(int id);
    Task<Invoice> CreateAsync(Invoice entity);
    Task UpdateAsync(Invoice entity);
    Task AddPaymentAsync(Payment payment);
}
