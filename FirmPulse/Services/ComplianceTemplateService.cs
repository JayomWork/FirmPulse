using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class ComplianceTemplateService(FirmPulseDbContext context) : IComplianceTemplateService
{
    public async Task<List<ComplianceTemplate>> GetTemplatesAsync()
    {
        return await context.ComplianceTemplates
            .Include(x => x.Items)
            .OrderBy(x => x.TemplateName)
            .ToListAsync();
    }

    public async Task<ComplianceTemplate?> GetTemplateByIdAsync(int id)
    {
        return await context.ComplianceTemplates
            .Include(x => x.Items.OrderBy(i => i.SequenceNo))
                .ThenInclude(x => x.ServiceMaster)
            .Include(x => x.Items.OrderBy(i => i.SequenceNo))
                .ThenInclude(x => x.McaFormMaster)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ComplianceTemplateItem>> GetTemplateItemsAsync(int templateId)
    {
        return await context.ComplianceTemplateItems
            .Include(x => x.ServiceMaster)
            .Include(x => x.McaFormMaster)
            .Where(x => x.ComplianceTemplateId == templateId)
            .OrderBy(x => x.SequenceNo)
            .ToListAsync();
    }

    public async Task<ComplianceTemplate> CreateTemplateAsync(ComplianceTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        template.UpdatedAt = DateTime.UtcNow;
        context.ComplianceTemplates.Add(template);
        await context.SaveChangesAsync();
        return template;
    }

    public async Task UpdateTemplateAsync(ComplianceTemplate template)
    {
        var existing = await context.ComplianceTemplates.FirstOrDefaultAsync(x => x.Id == template.Id);
        if (existing is null)
        {
            return;
        }

        existing.TemplateName = template.TemplateName;
        existing.Description = template.Description;
        existing.CompanyType = template.CompanyType;
        existing.FinancialYearBased = template.FinancialYearBased;
        existing.IsSystemTemplate = template.IsSystemTemplate;
        existing.IsActive = template.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task<ComplianceTemplateItem> AddTemplateItemAsync(ComplianceTemplateItem item)
    {
        context.ComplianceTemplateItems.Add(item);
        await context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateTemplateItemAsync(ComplianceTemplateItem item)
    {
        var existing = await context.ComplianceTemplateItems.FirstOrDefaultAsync(x => x.Id == item.Id);
        if (existing is null)
        {
            return;
        }

        existing.ServiceMasterId = item.ServiceMasterId;
        existing.McaFormMasterId = item.McaFormMasterId;
        existing.Title = item.Title;
        existing.Description = item.Description;
        existing.SequenceNo = item.SequenceNo;
        existing.DefaultDueOffsetDays = item.DefaultDueOffsetDays;
        existing.DueDateBasedOn = item.DueDateBasedOn;
        existing.RequiresFiling = item.RequiresFiling;
        existing.RequiresDocuments = item.RequiresDocuments;
        existing.RequiresMeeting = item.RequiresMeeting;
        existing.Priority = item.Priority;
        existing.IsActive = item.IsActive;

        await context.SaveChangesAsync();
    }
}
