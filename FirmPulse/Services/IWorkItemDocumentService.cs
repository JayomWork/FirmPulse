using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IWorkItemDocumentService
{
    Task<List<WorkItemDocument>> GetByWorkItemIdAsync(int workItemId);
    Task UpdateStatusAsync(int id, string status, string? remarks);
    Task<WorkItemDocument> AddDocumentAsync(WorkItemDocument entity);
}
