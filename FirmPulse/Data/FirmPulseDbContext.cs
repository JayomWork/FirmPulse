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
    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
    public DbSet<ServiceMaster> ServiceMasters => Set<ServiceMaster>();
    public DbSet<McaFormMaster> McaFormMasters => Set<McaFormMaster>();
    public DbSet<ComplianceTemplate> ComplianceTemplates => Set<ComplianceTemplate>();
    public DbSet<ComplianceTemplateItem> ComplianceTemplateItems => Set<ComplianceTemplateItem>();
    public DbSet<CompanyCompliancePlan> CompanyCompliancePlans => Set<CompanyCompliancePlan>();
    public DbSet<WorkItem> WorkItems => Set<WorkItem>();
    public DbSet<WorkItemStatusHistory> WorkItemStatusHistories => Set<WorkItemStatusHistory>();
    public DbSet<FilingRecord> FilingRecords => Set<FilingRecord>();
    public DbSet<DocumentChecklistTemplate> DocumentChecklistTemplates => Set<DocumentChecklistTemplate>();
    public DbSet<WorkItemDocument> WorkItemDocuments => Set<WorkItemDocument>();
    public DbSet<ClientFollowUp> ClientFollowUps => Set<ClientFollowUp>();
    public DbSet<CompanyMeeting> CompanyMeetings => Set<CompanyMeeting>();
    public DbSet<DocumentTemplate> DocumentTemplates => Set<DocumentTemplate>();
    public DbSet<DocumentTemplateField> DocumentTemplateFields => Set<DocumentTemplateField>();
    public DbSet<GeneratedDocument> GeneratedDocuments => Set<GeneratedDocument>();
    public DbSet<GeneratedDocumentValue> GeneratedDocumentValues => Set<GeneratedDocumentValue>();

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
            entity.Property(x => x.CIN).IsRequired().HasMaxLength(21);
            entity.Property(x => x.PAN).IsRequired().HasMaxLength(10);
            entity.Property(x => x.RegisteredOfficeAddress).IsRequired().HasMaxLength(500);
            entity.Property(x => x.CompanyType).IsRequired().HasMaxLength(100);
            entity.Property(x => x.RegistrationNumber).HasMaxLength(100);
            entity.Property(x => x.City).HasMaxLength(100);
            entity.Property(x => x.State).HasMaxLength(100);
            entity.Property(x => x.PinCode).HasMaxLength(20);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            entity.Property(x => x.ContactPersonName).IsRequired().HasMaxLength(150);
            entity.Property(x => x.CompanyClass).HasMaxLength(100);
            entity.Property(x => x.AuthorizedCapital).HasPrecision(18, 2);
            entity.Property(x => x.PaidUpCapital).HasPrecision(18, 2);

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
            entity.Property(x => x.Address).HasMaxLength(500);
            entity.Property(x => x.Designation).HasMaxLength(100);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.Directors)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ComplianceTask>(entity =>
        {
            entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.ComplianceType).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Priority).IsRequired().HasMaxLength(50);
            entity.Property(x => x.AssignedTo).IsRequired().HasMaxLength(150);
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
            entity.Property(x => x.Status).HasMaxLength(50);

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

            entity.HasOne(x => x.WorkItem)
                .WithMany(x => x.DocumentRecords)
                .HasForeignKey(x => x.WorkItemId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.FilingRecord)
                .WithMany(x => x.DocumentRecords)
                .HasForeignKey(x => x.FilingRecordId)
                .OnDelete(DeleteBehavior.Restrict);
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

        modelBuilder.Entity<ServiceCategory>(entity =>
        {
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<ServiceMaster>(entity =>
        {
            entity.Property(x => x.ServiceCode).IsRequired().HasMaxLength(30);
            entity.Property(x => x.ServiceName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.DefaultFee).HasPrecision(18, 2);
            entity.Property(x => x.RecurringType).HasMaxLength(50);

            entity.HasOne(x => x.ServiceCategory)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<McaFormMaster>(entity =>
        {
            entity.Property(x => x.FormCode).IsRequired().HasMaxLength(30);
            entity.Property(x => x.FormName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.ApplicableTo).HasMaxLength(100);
            entity.Property(x => x.DefaultDueRule).HasMaxLength(250);
        });

        modelBuilder.Entity<ComplianceTemplate>(entity =>
        {
            entity.Property(x => x.TemplateName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.CompanyType).IsRequired().HasMaxLength(100);

            entity.HasOne(x => x.Firm)
                .WithMany()
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ComplianceTemplateItem>(entity =>
        {
            entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.DueDateBasedOn).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Priority).IsRequired().HasMaxLength(50);

            entity.HasOne(x => x.ComplianceTemplate)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ComplianceTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.ServiceMaster)
                .WithMany(x => x.ComplianceTemplateItems)
                .HasForeignKey(x => x.ServiceMasterId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.McaFormMaster)
                .WithMany(x => x.ComplianceTemplateItems)
                .HasForeignKey(x => x.McaFormMasterId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CompanyCompliancePlan>(entity =>
        {
            entity.Property(x => x.FinancialYear).IsRequired().HasMaxLength(20);
            entity.Property(x => x.PlanName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);

            entity.HasOne(x => x.Firm)
                .WithMany()
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.CompanyCompliancePlans)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ComplianceTemplate)
                .WithMany(x => x.CompanyCompliancePlans)
                .HasForeignKey(x => x.ComplianceTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<WorkItem>(entity =>
        {
            entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.FinancialYear).HasMaxLength(20);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Priority).IsRequired().HasMaxLength(50);
            entity.Property(x => x.AssignedTo).HasMaxLength(150);
            entity.Property(x => x.ClientDocumentsStatus).IsRequired().HasMaxLength(50);
            entity.Property(x => x.FilingStatus).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Firm)
                .WithMany()
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.WorkItems)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyCompliancePlan)
                .WithMany(x => x.WorkItems)
                .HasForeignKey(x => x.CompanyCompliancePlanId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ServiceMaster)
                .WithMany(x => x.WorkItems)
                .HasForeignKey(x => x.ServiceMasterId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.McaFormMaster)
                .WithMany(x => x.WorkItems)
                .HasForeignKey(x => x.McaFormMasterId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<WorkItemStatusHistory>(entity =>
        {
            entity.Property(x => x.OldStatus).HasMaxLength(50);
            entity.Property(x => x.NewStatus).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Remarks).HasMaxLength(1000);
            entity.Property(x => x.ChangedBy).HasMaxLength(150);

            entity.HasOne(x => x.WorkItem)
                .WithMany(x => x.StatusHistory)
                .HasForeignKey(x => x.WorkItemId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FilingRecord>(entity =>
        {
            entity.Property(x => x.FinancialYear).HasMaxLength(20);
            entity.Property(x => x.SRN).HasMaxLength(50);
            entity.Property(x => x.ChallanNumber).HasMaxLength(50);
            entity.Property(x => x.ChallanAmount).HasPrecision(18, 2);
            entity.Property(x => x.McaStatus).IsRequired().HasMaxLength(50);
            entity.Property(x => x.AcknowledgementFilePath).HasMaxLength(300);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Firm)
                .WithMany()
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.FilingRecords)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.WorkItem)
                .WithMany(x => x.FilingRecords)
                .HasForeignKey(x => x.WorkItemId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.McaFormMaster)
                .WithMany(x => x.FilingRecords)
                .HasForeignKey(x => x.McaFormMasterId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DocumentChecklistTemplate>(entity =>
        {
            entity.Property(x => x.DocumentName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Description).HasMaxLength(500);

            entity.HasOne(x => x.ServiceMaster)
                .WithMany(x => x.DocumentChecklistTemplates)
                .HasForeignKey(x => x.ServiceMasterId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ComplianceTemplateItem)
                .WithMany(x => x.DocumentChecklistTemplates)
                .HasForeignKey(x => x.ComplianceTemplateItemId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<WorkItemDocument>(entity =>
        {
            entity.Property(x => x.DocumentName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.DocumentType).HasMaxLength(100);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);
            entity.Property(x => x.FilePath).HasMaxLength(300);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Firm)
                .WithMany()
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.WorkItemDocuments)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.WorkItem)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.WorkItemId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ClientFollowUp>(entity =>
        {
            entity.Property(x => x.FollowUpType).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Subject).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Notes).HasMaxLength(1000);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);

            entity.HasOne(x => x.Firm)
                .WithMany()
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.ClientFollowUps)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.WorkItem)
                .WithMany(x => x.FollowUps)
                .HasForeignKey(x => x.WorkItemId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CompanyMeeting>(entity =>
        {
            entity.Property(x => x.MeetingType).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
            entity.Property(x => x.VenueOrMode).HasMaxLength(200);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.HasOne(x => x.Firm)
                .WithMany()
                .HasForeignKey(x => x.FirmId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.CompanyMeetings)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CompanyCompliancePlan)
                .WithMany(x => x.Meetings)
                .HasForeignKey(x => x.CompanyCompliancePlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DocumentTemplate>(entity =>
        {
            entity.Property(x => x.TemplateName).IsRequired().HasMaxLength(200);
            entity.Property(x => x.TemplateCategory).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Description).HasMaxLength(1000);
            entity.Property(x => x.TemplateContent).IsRequired();
        });

        modelBuilder.Entity<DocumentTemplateField>(entity =>
        {
            entity.Property(x => x.FieldKey).IsRequired().HasMaxLength(100);
            entity.Property(x => x.FieldLabel).IsRequired().HasMaxLength(150);
            entity.Property(x => x.FieldType).IsRequired().HasMaxLength(50);
            entity.Property(x => x.DataSource).HasMaxLength(100);
            entity.Property(x => x.DefaultValue).HasMaxLength(500);

            entity.HasOne(x => x.DocumentTemplate)
                .WithMany(x => x.Fields)
                .HasForeignKey(x => x.DocumentTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GeneratedDocument>(entity =>
        {
            entity.Property(x => x.DocumentTitle).IsRequired().HasMaxLength(250);
            entity.Property(x => x.GeneratedContent).IsRequired();
            entity.Property(x => x.WordFilePath).HasMaxLength(500);
            entity.Property(x => x.PdfFilePath).HasMaxLength(500);
            entity.Property(x => x.GeneratedBy).HasMaxLength(150);

            entity.HasOne(x => x.CompanyClient)
                .WithMany(x => x.GeneratedDocuments)
                .HasForeignKey(x => x.CompanyClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.DocumentTemplate)
                .WithMany(x => x.GeneratedDocuments)
                .HasForeignKey(x => x.DocumentTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GeneratedDocumentValue>(entity =>
        {
            entity.Property(x => x.FieldKey).IsRequired().HasMaxLength(100);

            entity.HasOne(x => x.GeneratedDocument)
                .WithMany(x => x.Values)
                .HasForeignKey(x => x.GeneratedDocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
