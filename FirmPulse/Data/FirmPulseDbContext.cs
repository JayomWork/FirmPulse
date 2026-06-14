using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Data;

public class FirmPulseDbContext(DbContextOptions<FirmPulseDbContext> options) : DbContext(options)
{
    public DbSet<Firm> Firms => Set<Firm>();
    public DbSet<CompanyClient> CompanyClients => Set<CompanyClient>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<ComplianceTask> ComplianceTasks => Set<ComplianceTask>();
    public DbSet<DocumentRecord> DocumentRecords => Set<DocumentRecord>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Firm>(entity =>
        {
            entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Phone).HasMaxLength(20);
            entity.Property(x => x.Address).HasMaxLength(500);
        });

        modelBuilder.Entity<CompanyClient>(entity =>
        {
            entity.Property(x => x.CompanyName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.CIN).HasMaxLength(21);
            entity.Property(x => x.PAN).HasMaxLength(10);
            entity.Property(x => x.RegisteredOfficeAddress).HasMaxLength(500);
            entity.Property(x => x.CompanyType).HasMaxLength(100);
            entity.Property(x => x.Status).HasMaxLength(100);
            entity.Property(x => x.Email).HasMaxLength(150);
            entity.Property(x => x.Phone).HasMaxLength(20);
            entity.Property(x => x.ContactPersonName).HasMaxLength(150);

            entity.HasOne(x => x.Firm)
                .WithMany(x => x.CompanyClients)
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.Property(x => x.FullName).IsRequired().HasMaxLength(150);
            entity.Property(x => x.DIN).HasMaxLength(20);
            entity.Property(x => x.PAN).HasMaxLength(10);
            entity.Property(x => x.Email).HasMaxLength(150);
            entity.Property(x => x.Phone).HasMaxLength(20);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.Directors)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ComplianceTask>(entity =>
        {
            entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.ComplianceType).HasMaxLength(100);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Priority).IsRequired().HasMaxLength(50);
            entity.Property(x => x.AssignedTo).HasMaxLength(150);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Firm)
                .WithMany(x => x.ComplianceTasks)
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.ComplianceTasks)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DocumentRecord>(entity =>
        {
            entity.Property(x => x.DocumentName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.DocumentType).HasMaxLength(100);
            entity.Property(x => x.FilePath).HasMaxLength(300);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Firm)
                .WithMany(x => x.DocumentRecords)
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.DocumentRecords)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.ComplianceTask)
                .WithMany(x => x.DocumentRecords)
                .HasForeignKey(x => x.ComplianceTaskId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Amount).HasPrecision(18, 2);
            entity.Property(x => x.TaxAmount).HasPrecision(18, 2);
            entity.Property(x => x.TotalAmount).HasPrecision(18, 2);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Firm)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(x => x.Amount).HasPrecision(18, 2);
            entity.Property(x => x.PaymentMode).HasMaxLength(50);
            entity.Property(x => x.ReferenceNumber).HasMaxLength(100);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Invoice)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
