using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class ServiceMasterService(FirmPulseDbContext context) : IServiceMasterService
{
    public async Task<List<ServiceCategory>> GetCategoriesAsync()
    {
        return await context.ServiceCategories
            .Include(x => x.Services.OrderBy(s => s.ServiceName))
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<List<ServiceMaster>> GetServicesAsync()
    {
        return await context.ServiceMasters
            .Include(x => x.ServiceCategory)
            .OrderBy(x => x.ServiceCategory!.DisplayOrder)
            .ThenBy(x => x.ServiceName)
            .ToListAsync();
    }

    public async Task<List<ServiceMaster>> GetActiveServicesAsync()
    {
        return await context.ServiceMasters
            .Include(x => x.ServiceCategory)
            .Where(x => x.IsActive)
            .OrderBy(x => x.ServiceName)
            .ToListAsync();
    }

    public async Task<ServiceMaster> CreateServiceAsync(ServiceMaster entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        context.ServiceMasters.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateServiceAsync(ServiceMaster entity)
    {
        var existing = await context.ServiceMasters.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (existing is null)
        {
            return;
        }

        existing.ServiceCategoryId = entity.ServiceCategoryId;
        existing.ServiceCode = entity.ServiceCode;
        existing.ServiceName = entity.ServiceName;
        existing.Description = entity.Description;
        existing.DefaultFee = entity.DefaultFee;
        existing.DefaultDueDays = entity.DefaultDueDays;
        existing.IsRecurring = entity.IsRecurring;
        existing.RecurringType = entity.RecurringType;
        existing.IsActive = entity.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }
}
