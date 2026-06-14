using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FirmPulse.Data.Migrations;

[DbContext(typeof(FirmPulseDbContext))]
partial class FirmPulseDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "10.0.5")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("FirmPulse.Entities.CompanyClient", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("CIN")
                .HasMaxLength(21)
                .HasColumnType("character varying(21)");

            b.Property<string>("CompanyName")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");

            b.Property<string>("CompanyType")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)");

            b.Property<string>("ContactPersonName")
                .HasMaxLength(150)
                .HasColumnType("character varying(150)");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone");

            b.Property<string>("Email")
                .HasMaxLength(150)
                .HasColumnType("character varying(150)");

            b.Property<int>("FirmId")
                .HasColumnType("integer");

            b.Property<DateOnly?>("IncorporationDate")
                .HasColumnType("date");

            b.Property<bool>("IsActive")
                .HasColumnType("boolean");

            b.Property<string>("PAN")
                .HasMaxLength(10)
                .HasColumnType("character varying(10)");

            b.Property<string>("Phone")
                .HasMaxLength(20)
                .HasColumnType("character varying(20)");

            b.Property<string>("RegisteredOfficeAddress")
                .HasMaxLength(500)
                .HasColumnType("character varying(500)");

            b.Property<string>("Status")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)");

            b.Property<DateTime>("UpdatedAt")
                .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.HasIndex("FirmId");

            b.ToTable("CompanyClients");
        });

        modelBuilder.Entity("FirmPulse.Entities.ComplianceTask", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("AssignedTo")
                .HasMaxLength(150)
                .HasColumnType("character varying(150)");

            b.Property<int>("CompanyClientId")
                .HasColumnType("integer");

            b.Property<string>("ComplianceType")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)");

            b.Property<DateOnly?>("CompletedDate")
                .HasColumnType("date");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone");

            b.Property<string>("Description")
                .HasMaxLength(1000)
                .HasColumnType("character varying(1000)");

            b.Property<DateOnly>("DueDate")
                .HasColumnType("date");

            b.Property<int>("FirmId")
                .HasColumnType("integer");

            b.Property<string>("Priority")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");

            b.Property<string>("Remarks")
                .HasMaxLength(1000)
                .HasColumnType("character varying(1000)");

            b.Property<string>("Status")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");

            b.Property<string>("Title")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");

            b.Property<DateTime>("UpdatedAt")
                .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.HasIndex("CompanyClientId");

            b.HasIndex("FirmId");

            b.ToTable("ComplianceTasks");
        });

        modelBuilder.Entity("FirmPulse.Entities.Director", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<DateOnly?>("AppointmentDate")
                .HasColumnType("date");

            b.Property<int>("CompanyClientId")
                .HasColumnType("integer");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone");

            b.Property<string>("DIN")
                .HasMaxLength(20)
                .HasColumnType("character varying(20)");

            b.Property<string>("Email")
                .HasMaxLength(150)
                .HasColumnType("character varying(150)");

            b.Property<string>("FullName")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("character varying(150)");

            b.Property<bool>("IsActive")
                .HasColumnType("boolean");

            b.Property<string>("PAN")
                .HasMaxLength(10)
                .HasColumnType("character varying(10)");

            b.Property<string>("Phone")
                .HasMaxLength(20)
                .HasColumnType("character varying(20)");

            b.Property<DateOnly?>("ResignationDate")
                .HasColumnType("date");

            b.Property<DateTime>("UpdatedAt")
                .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.HasIndex("CompanyClientId");

            b.ToTable("Directors");
        });

        modelBuilder.Entity("FirmPulse.Entities.DocumentRecord", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<int?>("ComplianceTaskId")
                .HasColumnType("integer");

            b.Property<int>("CompanyClientId")
                .HasColumnType("integer");

            b.Property<string>("DocumentName")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");

            b.Property<string>("DocumentType")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)");

            b.Property<string>("FilePath")
                .HasMaxLength(300)
                .HasColumnType("character varying(300)");

            b.Property<int>("FirmId")
                .HasColumnType("integer");

            b.Property<string>("Remarks")
                .HasMaxLength(1000)
                .HasColumnType("character varying(1000)");

            b.Property<DateTime>("UploadedAt")
                .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.HasIndex("ComplianceTaskId");

            b.HasIndex("CompanyClientId");

            b.HasIndex("FirmId");

            b.ToTable("DocumentRecords");
        });

        modelBuilder.Entity("FirmPulse.Entities.Firm", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("Address")
                .HasMaxLength(500)
                .HasColumnType("character varying(500)");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone");

            b.Property<string>("Email")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("character varying(150)");

            b.Property<bool>("IsActive")
                .HasColumnType("boolean");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");

            b.Property<string>("Phone")
                .HasMaxLength(20)
                .HasColumnType("character varying(20)");

            b.Property<DateTime>("UpdatedAt")
                .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.ToTable("Firms");
        });

        modelBuilder.Entity("FirmPulse.Entities.Invoice", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<decimal>("Amount")
                .HasPrecision(18, 2)
                .HasColumnType("numeric(18,2)");

            b.Property<int>("CompanyClientId")
                .HasColumnType("integer");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone");

            b.Property<DateOnly>("DueDate")
                .HasColumnType("date");

            b.Property<int>("FirmId")
                .HasColumnType("integer");

            b.Property<DateOnly>("InvoiceDate")
                .HasColumnType("date");

            b.Property<string>("InvoiceNumber")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");

            b.Property<string>("Remarks")
                .HasMaxLength(1000)
                .HasColumnType("character varying(1000)");

            b.Property<string>("Status")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");

            b.Property<decimal>("TaxAmount")
                .HasPrecision(18, 2)
                .HasColumnType("numeric(18,2)");

            b.Property<decimal>("TotalAmount")
                .HasPrecision(18, 2)
                .HasColumnType("numeric(18,2)");

            b.Property<DateTime>("UpdatedAt")
                .HasColumnType("timestamp with time zone");

            b.HasKey("Id");

            b.HasIndex("CompanyClientId");

            b.HasIndex("FirmId");

            b.ToTable("Invoices");
        });

        modelBuilder.Entity("FirmPulse.Entities.Payment", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<decimal>("Amount")
                .HasPrecision(18, 2)
                .HasColumnType("numeric(18,2)");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone");

            b.Property<int>("InvoiceId")
                .HasColumnType("integer");

            b.Property<DateOnly>("PaymentDate")
                .HasColumnType("date");

            b.Property<string>("PaymentMode")
                .HasMaxLength(50)
                .HasColumnType("character varying(50)");

            b.Property<string>("ReferenceNumber")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)");

            b.Property<string>("Remarks")
                .HasMaxLength(1000)
                .HasColumnType("character varying(1000)");

            b.HasKey("Id");

            b.HasIndex("InvoiceId");

            b.ToTable("Payments");
        });

        modelBuilder.Entity("FirmPulse.Entities.CompanyClient", b =>
        {
            b.HasOne("FirmPulse.Entities.Firm", "Firm")
                .WithMany("CompanyClients")
                .HasForeignKey("FirmId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("Firm");
        });

        modelBuilder.Entity("FirmPulse.Entities.ComplianceTask", b =>
        {
            b.HasOne("FirmPulse.Entities.CompanyClient", "CompanyClient")
                .WithMany("ComplianceTasks")
                .HasForeignKey("CompanyClientId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("FirmPulse.Entities.Firm", "Firm")
                .WithMany("ComplianceTasks")
                .HasForeignKey("FirmId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("CompanyClient");

            b.Navigation("Firm");
        });

        modelBuilder.Entity("FirmPulse.Entities.Director", b =>
        {
            b.HasOne("FirmPulse.Entities.CompanyClient", "CompanyClient")
                .WithMany("Directors")
                .HasForeignKey("CompanyClientId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("CompanyClient");
        });

        modelBuilder.Entity("FirmPulse.Entities.DocumentRecord", b =>
        {
            b.HasOne("FirmPulse.Entities.ComplianceTask", "ComplianceTask")
                .WithMany("DocumentRecords")
                .HasForeignKey("ComplianceTaskId")
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne("FirmPulse.Entities.CompanyClient", "CompanyClient")
                .WithMany("DocumentRecords")
                .HasForeignKey("CompanyClientId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("FirmPulse.Entities.Firm", "Firm")
                .WithMany("DocumentRecords")
                .HasForeignKey("FirmId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("CompanyClient");

            b.Navigation("ComplianceTask");

            b.Navigation("Firm");
        });

        modelBuilder.Entity("FirmPulse.Entities.Invoice", b =>
        {
            b.HasOne("FirmPulse.Entities.CompanyClient", "CompanyClient")
                .WithMany("Invoices")
                .HasForeignKey("CompanyClientId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("FirmPulse.Entities.Firm", "Firm")
                .WithMany("Invoices")
                .HasForeignKey("FirmId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("CompanyClient");

            b.Navigation("Firm");
        });

        modelBuilder.Entity("FirmPulse.Entities.Payment", b =>
        {
            b.HasOne("FirmPulse.Entities.Invoice", "Invoice")
                .WithMany("Payments")
                .HasForeignKey("InvoiceId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Invoice");
        });

        modelBuilder.Entity("FirmPulse.Entities.CompanyClient", b =>
        {
            b.Navigation("ComplianceTasks");
            b.Navigation("Directors");
            b.Navigation("DocumentRecords");
            b.Navigation("Invoices");
        });

        modelBuilder.Entity("FirmPulse.Entities.ComplianceTask", b =>
        {
            b.Navigation("DocumentRecords");
        });

        modelBuilder.Entity("FirmPulse.Entities.Firm", b =>
        {
            b.Navigation("CompanyClients");
            b.Navigation("ComplianceTasks");
            b.Navigation("DocumentRecords");
            b.Navigation("Invoices");
        });

        modelBuilder.Entity("FirmPulse.Entities.Invoice", b =>
        {
            b.Navigation("Payments");
        });
    }
}
