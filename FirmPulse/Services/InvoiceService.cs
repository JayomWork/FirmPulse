using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public class InvoiceService(FirmPulseDbContext context) : IInvoiceService
{
    public async Task<List<Invoice>> GetAllAsync()
    {
        return await context.Invoices
            .Include(x => x.CompanyClient)
            .Include(x => x.Payments)
            .Where(x => x.IsActive && x.CompanyClient != null && x.CompanyClient.IsActive)
            .OrderByDescending(x => x.InvoiceDate)
            .ThenByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<Invoice>> GetByCompanyIdAsync(int companyClientId)
    {
        return await context.Invoices
            .Include(x => x.CompanyClient)
            .Include(x => x.Payments)
            .Where(x => x.IsActive && x.CompanyClientId == companyClientId)
            .OrderByDescending(x => x.InvoiceDate)
            .ToListAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        return await context.Invoices
            .Include(x => x.CompanyClient)
            .Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public async Task<Invoice> CreateAsync(Invoice entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsActive = true;

        context.Invoices.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Invoice entity)
    {
        var existing = await context.Invoices.FirstOrDefaultAsync(x => x.Id == entity.Id && x.IsActive);
        if (existing is null)
        {
            return;
        }

        existing.FirmId = entity.FirmId;
        existing.CompanyClientId = entity.CompanyClientId;
        existing.InvoiceNumber = entity.InvoiceNumber;
        existing.InvoiceDate = entity.InvoiceDate;
        existing.DueDate = entity.DueDate;
        existing.Amount = entity.Amount;
        existing.TaxAmount = entity.TaxAmount;
        existing.TotalAmount = entity.TotalAmount;
        existing.Status = entity.Status;
        existing.Remarks = entity.Remarks;
        existing.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await context.Invoices.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        if (existing is null)
        {
            return;
        }

        existing.IsActive = false;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        payment.CreatedAt = DateTime.UtcNow;

        context.Payments.Add(payment);
        await context.SaveChangesAsync();

        var invoice = await context.Invoices
            .Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == payment.InvoiceId);

        if (invoice is null)
        {
            return;
        }

        var paidAmount = invoice.Payments.Sum(x => x.Amount);
        invoice.Status = paidAmount switch
        {
            <= 0 => invoice.Status,
            var value when value >= invoice.TotalAmount => InvoiceStatuses.Paid,
            _ => InvoiceStatuses.PartiallyPaid
        };
        invoice.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }
}
