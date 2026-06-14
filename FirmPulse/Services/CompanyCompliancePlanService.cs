using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class CompanyCompliancePlanService(FirmPulseDbContext context) : ICompanyCompliancePlanService
{
    public async Task<List<CompanyCompliancePlan>> GetPlansAsync()
    {
        return await context.CompanyCompliancePlans
            .Include(x => x.CompanyClient)
            .Include(x => x.ComplianceTemplate)
            .OrderByDescending(x => x.FinancialYear)
            .ThenBy(x => x.PlanName)
            .ToListAsync();
    }

    public async Task<List<CompanyCompliancePlan>> GetByCompanyIdAsync(int companyClientId)
    {
        return await context.CompanyCompliancePlans
            .Include(x => x.ComplianceTemplate)
            .Include(x => x.WorkItems)
            .Where(x => x.CompanyClientId == companyClientId)
            .OrderByDescending(x => x.FinancialYear)
            .ToListAsync();
    }

    public async Task<CompanyCompliancePlan?> GetByIdAsync(int id)
    {
        return await context.CompanyCompliancePlans
            .Include(x => x.CompanyClient)
            .Include(x => x.ComplianceTemplate)
            .Include(x => x.WorkItems)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CompanyCompliancePlan> CreatePlanAsync(CompanyCompliancePlan plan)
    {
        plan.CreatedAt = DateTime.UtcNow;
        plan.UpdatedAt = DateTime.UtcNow;
        context.CompanyCompliancePlans.Add(plan);
        await context.SaveChangesAsync();
        return plan;
    }

    public async Task GenerateWorkItemsFromTemplateAsync(int planId)
    {
        var plan = await context.CompanyCompliancePlans
            .Include(x => x.CompanyClient)
            .Include(x => x.ComplianceTemplate)
            .FirstOrDefaultAsync(x => x.Id == planId);

        if (plan is null)
        {
            return;
        }

        var hasExisting = await context.WorkItems.AnyAsync(x => x.CompanyCompliancePlanId == planId);
        if (hasExisting)
        {
            return;
        }

        var templateItems = await context.ComplianceTemplateItems
            .Include(x => x.ServiceMaster)
            .Include(x => x.McaFormMaster)
            .Where(x => x.ComplianceTemplateId == plan.ComplianceTemplateId && x.IsActive)
            .OrderBy(x => x.SequenceNo)
            .ToListAsync();

        foreach (var item in templateItems)
        {
            var dueDate = CalculateDueDate(plan, item);

            var workItem = new WorkItem
            {
                FirmId = plan.FirmId,
                CompanyClientId = plan.CompanyClientId,
                CompanyCompliancePlanId = plan.Id,
                ServiceMasterId = item.ServiceMasterId,
                McaFormMasterId = item.McaFormMasterId,
                Title = item.Title,
                Description = item.Description,
                FinancialYear = plan.FinancialYear,
                DueDate = dueDate,
                Status = WorkItemStatuses.NotStarted,
                Priority = item.Priority,
                AssignedTo = "Unassigned",
                RequiresClientDocuments = item.RequiresDocuments,
                ClientDocumentsStatus = item.RequiresDocuments ? WorkItemClientDocumentStatuses.Pending : WorkItemClientDocumentStatuses.NotRequired,
                RequiresMcaFiling = item.RequiresFiling,
                FilingStatus = item.RequiresFiling ? WorkItemFilingStatuses.Pending : WorkItemFilingStatuses.NotRequired,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.WorkItems.Add(workItem);
            await context.SaveChangesAsync();

            var checklistTemplates = await context.DocumentChecklistTemplates
                .Where(x => x.IsActive && (x.ComplianceTemplateItemId == item.Id || (x.ServiceMasterId.HasValue && x.ServiceMasterId == item.ServiceMasterId)))
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();

            foreach (var doc in checklistTemplates)
            {
                context.WorkItemDocuments.Add(new WorkItemDocument
                {
                    FirmId = plan.FirmId,
                    CompanyClientId = plan.CompanyClientId,
                    WorkItemId = workItem.Id,
                    DocumentName = doc.DocumentName,
                    DocumentType = item.McaFormMaster?.FormCode,
                    IsMandatory = doc.IsMandatory,
                    Status = doc.IsMandatory ? WorkItemDocumentStatuses.PendingFromClient : WorkItemDocumentStatuses.NotRequired,
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (item.RequiresFiling && item.McaFormMasterId.HasValue)
            {
                context.FilingRecords.Add(new FilingRecord
                {
                    FirmId = plan.FirmId,
                    CompanyClientId = plan.CompanyClientId,
                    WorkItemId = workItem.Id,
                    McaFormMasterId = item.McaFormMasterId.Value,
                    FinancialYear = plan.FinancialYear,
                    DueDate = dueDate,
                    McaStatus = FilingRecordStatuses.NotStarted,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }
        }

        plan.Status = CompanyCompliancePlanStatuses.Active;
        plan.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task MarkPlanCompletedAsync(int planId)
    {
        var plan = await context.CompanyCompliancePlans.FirstOrDefaultAsync(x => x.Id == planId);
        if (plan is null)
        {
            return;
        }

        plan.Status = CompanyCompliancePlanStatuses.Completed;
        plan.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    private static DateOnly? CalculateDueDate(CompanyCompliancePlan plan, ComplianceTemplateItem item)
    {
        var offset = item.DefaultDueOffsetDays ?? 0;
        return item.DueDateBasedOn switch
        {
            DueDateBasedOnTypes.FinancialYearEnd => plan.EndDate.AddDays(offset),
            DueDateBasedOnTypes.AGMDate => plan.AGMDate?.AddDays(offset),
            DueDateBasedOnTypes.IncorporationDate => plan.CompanyClient?.IncorporationDate?.AddDays(offset),
            DueDateBasedOnTypes.EventDate => plan.StartDate.AddDays(offset),
            _ => plan.StartDate.AddDays(offset)
        };
    }
}
