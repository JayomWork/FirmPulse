using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Seed;

public static class FirmPulseSeedData
{
    public static async Task SeedAsync(FirmPulseDbContext context)
    {
        if (await context.Firms.AnyAsync())
        {
            await SeedSystemMastersAsync(context);
            await SeedDocumentTemplatesAsync(context);
            return;
        }

        var now = DateTime.UtcNow;
        var today = DateOnly.FromDateTime(DateTime.Today);

        var firm = new Firm
        {
            Name = "Apex Secretarial Services",
            Email = "admin@apexsecretarial.in",
            Phone = "+91 98765 43210",
            Address = "Banjara Hills, Hyderabad, Telangana",
            CreatedAt = now,
            UpdatedAt = now
        };

        var annualCompliance = new ServiceCategory { Name = "Annual Compliance", Description = "Recurring annual secretarial and filing work.", DisplayOrder = 1, CreatedAt = now, UpdatedAt = now };
        var mcaFilings = new ServiceCategory { Name = "MCA Filing", Description = "Statutory forms filed on MCA portal.", DisplayOrder = 2, CreatedAt = now, UpdatedAt = now };
        var meetingDocs = new ServiceCategory { Name = "Meeting Documentation", Description = "Board, AGM, EGM and committee meeting work.", DisplayOrder = 3, CreatedAt = now, UpdatedAt = now };
        var incorporation = new ServiceCategory { Name = "Incorporation", Description = "Entity setup and registration support.", DisplayOrder = 4, CreatedAt = now, UpdatedAt = now };
        var eventBased = new ServiceCategory { Name = "Event Based Compliance", Description = "One-time event filings and changes.", DisplayOrder = 5, CreatedAt = now, UpdatedAt = now };
        var llpCompliance = new ServiceCategory { Name = "LLP Compliance", Description = "LLP annual and event-based compliance.", DisplayOrder = 6, CreatedAt = now, UpdatedAt = now };

        var serviceCategories = new[]
        {
            annualCompliance, mcaFilings, meetingDocs, incorporation, eventBased, llpCompliance
        };

        var svcAnnual = new ServiceMaster { ServiceCategory = annualCompliance, ServiceCode = "ANNUAL-001", ServiceName = "Annual Filing", Description = "End-to-end annual filing support.", DefaultFee = 25000m, DefaultDueDays = 180, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now };
        var svcAoc4 = new ServiceMaster { ServiceCategory = mcaFilings, ServiceCode = "MCA-AOC4", ServiceName = "AOC-4 Filing", Description = "Preparation and filing of AOC-4.", DefaultFee = 8500m, DefaultDueDays = 30, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now };
        var svcMgt7 = new ServiceMaster { ServiceCategory = mcaFilings, ServiceCode = "MCA-MGT7", ServiceName = "MGT-7 / MGT-7A Filing", Description = "Annual return filing support.", DefaultFee = 8500m, DefaultDueDays = 60, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now };
        var svcDir3 = new ServiceMaster { ServiceCategory = mcaFilings, ServiceCode = "MCA-DIR3", ServiceName = "DIR-3 KYC", Description = "Annual DIR-3 KYC filing.", DefaultFee = 3000m, DefaultDueDays = 30, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now };
        var svcAdt1 = new ServiceMaster { ServiceCategory = mcaFilings, ServiceCode = "MCA-ADT1", ServiceName = "ADT-1 Auditor Appointment", Description = "Auditor appointment filing.", DefaultFee = 5000m, DefaultDueDays = 15, IsRecurring = false, RecurringType = ServiceRecurringTypes.EventBased, CreatedAt = now, UpdatedAt = now };
        var svcBoard = new ServiceMaster { ServiceCategory = meetingDocs, ServiceCode = "MEET-BOARD", ServiceName = "Board Meeting Documentation", Description = "Board meeting notices, agenda, resolutions and minutes.", DefaultFee = 7000m, DefaultDueDays = 7, IsRecurring = true, RecurringType = ServiceRecurringTypes.Quarterly, CreatedAt = now, UpdatedAt = now };
        var svcAgm = new ServiceMaster { ServiceCategory = meetingDocs, ServiceCode = "MEET-AGM", ServiceName = "AGM Documentation", Description = "AGM planning, notices and minutes.", DefaultFee = 9000m, DefaultDueDays = 30, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now };
        var svcRegisters = new ServiceMaster { ServiceCategory = annualCompliance, ServiceCode = "STAT-REG", ServiceName = "Statutory Register Maintenance", Description = "Maintain statutory registers and records.", DefaultFee = 12000m, DefaultDueDays = 365, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now };
        var svcLlpAnnual = new ServiceMaster { ServiceCategory = llpCompliance, ServiceCode = "LLP-ANNUAL", ServiceName = "LLP Annual Filing", Description = "LLP Form 8 and Form 11 annual support.", DefaultFee = 18000m, DefaultDueDays = 180, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now };
        var svcEvent = new ServiceMaster { ServiceCategory = eventBased, ServiceCode = "EVENT-ROC", ServiceName = "Event Based Filing", Description = "ROC filings for corporate changes.", DefaultFee = 15000m, DefaultDueDays = 15, IsRecurring = false, RecurringType = ServiceRecurringTypes.EventBased, CreatedAt = now, UpdatedAt = now };

        var services = new[]
        {
            svcAnnual, svcAoc4, svcMgt7, svcDir3, svcAdt1, svcBoard, svcAgm, svcRegisters, svcLlpAnnual, svcEvent
        };

        var formAoc4 = new McaFormMaster { FormCode = "AOC-4", FormName = "AOC-4 Filing", Description = "Financial statement filing.", ApplicableTo = "Company", DefaultDueRule = "Within 30 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };
        var formAoc4Xbrl = new McaFormMaster { FormCode = "AOC-4 XBRL", FormName = "AOC-4 XBRL Filing", Description = "XBRL financial statement filing.", ApplicableTo = "Specified Company", DefaultDueRule = "Within 30 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };
        var formMgt7 = new McaFormMaster { FormCode = "MGT-7", FormName = "MGT-7 Annual Return", Description = "Annual return filing.", ApplicableTo = "Company", DefaultDueRule = "Within 60 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };
        var formMgt7A = new McaFormMaster { FormCode = "MGT-7A", FormName = "MGT-7A Annual Return", Description = "Annual return for small companies.", ApplicableTo = "Small Company / OPC", DefaultDueRule = "Within 60 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };
        var formDir3 = new McaFormMaster { FormCode = "DIR-3 KYC", FormName = "DIR-3 KYC", Description = "Director KYC filing.", ApplicableTo = "Director", DefaultDueRule = "On or before 30 September", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };
        var formAdt1 = new McaFormMaster { FormCode = "ADT-1", FormName = "ADT-1", Description = "Auditor appointment filing.", ApplicableTo = "Company", DefaultDueRule = "Within 15 days of AGM / appointment", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now };
        var formDpt3 = new McaFormMaster { FormCode = "DPT-3", FormName = "DPT-3", Description = "Return of deposits / exempted amounts.", ApplicableTo = "Company", DefaultDueRule = "On or before 30 June", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };
        var formMsme1 = new McaFormMaster { FormCode = "MSME-1", FormName = "MSME-1", Description = "Half-yearly MSME return.", ApplicableTo = "Company", DefaultDueRule = "Half-yearly", IsAnnualForm = false, IsEventBasedForm = false, CreatedAt = now, UpdatedAt = now };
        var formPas3 = new McaFormMaster { FormCode = "PAS-3", FormName = "PAS-3", Description = "Return of allotment.", ApplicableTo = "Company", DefaultDueRule = "Within 15 days of allotment", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now };
        var formMgt14 = new McaFormMaster { FormCode = "MGT-14", FormName = "MGT-14", Description = "Board/shareholder resolution filing.", ApplicableTo = "Company", DefaultDueRule = "Within 30 days of resolution", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now };
        var formSh7 = new McaFormMaster { FormCode = "SH-7", FormName = "SH-7", Description = "Alteration of share capital.", ApplicableTo = "Company", DefaultDueRule = "Within 30 days of change", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now };
        var formInc20A = new McaFormMaster { FormCode = "INC-20A", FormName = "INC-20A", Description = "Declaration for commencement of business.", ApplicableTo = "Company", DefaultDueRule = "Within 180 days of incorporation", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now };
        var formLlp8 = new McaFormMaster { FormCode = "LLP Form 8", FormName = "LLP Form 8", Description = "Statement of account & solvency.", ApplicableTo = "LLP", DefaultDueRule = "On or before 30 October", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };
        var formLlp11 = new McaFormMaster { FormCode = "LLP Form 11", FormName = "LLP Form 11", Description = "Annual return for LLP.", ApplicableTo = "LLP", DefaultDueRule = "On or before 30 May", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now };

        var forms = new[]
        {
            formAoc4, formAoc4Xbrl, formMgt7, formMgt7A, formDir3, formAdt1, formDpt3, formMsme1, formPas3, formMgt14, formSh7, formInc20A, formLlp8, formLlp11
        };

        var annualTemplate = new ComplianceTemplate
        {
            TemplateName = "Private Limited Annual Compliance",
            Description = "Default annual compliance workflow for private limited companies.",
            CompanyType = CompanyClientTypes.PrivateLimited,
            FinancialYearBased = true,
            IsSystemTemplate = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        var directorKycTemplate = new ComplianceTemplate
        {
            TemplateName = "Director KYC Compliance",
            Description = "Collect KYC and complete annual DIR-3 KYC filing.",
            CompanyType = CompanyClientTypes.PrivateLimited,
            FinancialYearBased = false,
            IsSystemTemplate = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        var auditorTemplate = new ComplianceTemplate
        {
            TemplateName = "Auditor Appointment",
            Description = "Prepare resolutions and file ADT-1 for appointment.",
            CompanyType = CompanyClientTypes.PrivateLimited,
            FinancialYearBased = false,
            IsSystemTemplate = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        var templates = new[] { annualTemplate, directorKycTemplate, auditorTemplate };

        var annualItems = new[]
        {
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcAnnual, Title = "Prepare financial statements", Description = "Coordinate and finalize financial statements.", SequenceNo = 1, DefaultDueOffsetDays = 10, DueDateBasedOn = DueDateBasedOnTypes.FinancialYearEnd, RequiresDocuments = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcAnnual, Title = "Prepare board report", Description = "Draft board report for annual closure.", SequenceNo = 2, DefaultDueOffsetDays = 25, DueDateBasedOn = DueDateBasedOnTypes.FinancialYearEnd, RequiresDocuments = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcBoard, Title = "Conduct board meeting for financial approval", Description = "Complete notice, agenda, attendance and minutes.", SequenceNo = 3, DefaultDueOffsetDays = 60, DueDateBasedOn = DueDateBasedOnTypes.FinancialYearEnd, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcAgm, Title = "Prepare AGM notice", Description = "Prepare and circulate AGM notice.", SequenceNo = 4, DefaultDueOffsetDays = -7, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcAgm, Title = "Conduct AGM", Description = "Hold AGM and complete attendance and minutes.", SequenceNo = 5, DefaultDueOffsetDays = 0, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.Critical },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcAoc4, McaFormMaster = formAoc4, Title = "File AOC-4", Description = "Prepare and file AOC-4 after AGM.", SequenceNo = 6, DefaultDueOffsetDays = 30, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcMgt7, McaFormMaster = formMgt7, Title = "File MGT-7 / MGT-7A", Description = "File annual return after AGM.", SequenceNo = 7, DefaultDueOffsetDays = 60, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcAnnual, Title = "Store SRN and challan", Description = "Update filing references and archive proof.", SequenceNo = 8, DefaultDueOffsetDays = 65, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = false, Priority = WorkItemPriorities.Medium },
            new ComplianceTemplateItem { ComplianceTemplate = annualTemplate, ServiceMaster = svcRegisters, Title = "Close annual compliance", Description = "Review completion and close annual cycle.", SequenceNo = 9, DefaultDueOffsetDays = 75, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = false, Priority = WorkItemPriorities.Medium }
        };

        var directorKycItems = new[]
        {
            new ComplianceTemplateItem { ComplianceTemplate = directorKycTemplate, ServiceMaster = svcDir3, Title = "Collect director KYC details", Description = "Collect PAN, Aadhaar, mobile and email details.", SequenceNo = 1, DefaultDueOffsetDays = 0, DueDateBasedOn = DueDateBasedOnTypes.Manual, RequiresDocuments = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = directorKycTemplate, ServiceMaster = svcDir3, Title = "Verify DIN/PAN", Description = "Review director records and previous year reference.", SequenceNo = 2, DefaultDueOffsetDays = 5, DueDateBasedOn = DueDateBasedOnTypes.Manual, RequiresDocuments = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = directorKycTemplate, ServiceMaster = svcDir3, McaFormMaster = formDir3, Title = "File DIR-3 KYC", Description = "Complete DIR-3 KYC filing.", SequenceNo = 3, DefaultDueOffsetDays = 15, DueDateBasedOn = DueDateBasedOnTypes.Manual, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
            new ComplianceTemplateItem { ComplianceTemplate = directorKycTemplate, ServiceMaster = svcDir3, Title = "Store SRN/challan", Description = "Record filing proof and challan references.", SequenceNo = 4, DefaultDueOffsetDays = 20, DueDateBasedOn = DueDateBasedOnTypes.Manual, Priority = WorkItemPriorities.Medium }
        };

        var auditorItems = new[]
        {
            new ComplianceTemplateItem { ComplianceTemplate = auditorTemplate, ServiceMaster = svcAdt1, Title = "Prepare board resolution", Description = "Draft and approve appointment resolution.", SequenceNo = 1, DefaultDueOffsetDays = 0, DueDateBasedOn = DueDateBasedOnTypes.EventDate, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = auditorTemplate, ServiceMaster = svcAdt1, Title = "Prepare ADT-1 details", Description = "Compile appointment details and attachments.", SequenceNo = 2, DefaultDueOffsetDays = 5, DueDateBasedOn = DueDateBasedOnTypes.EventDate, RequiresDocuments = true, Priority = WorkItemPriorities.High },
            new ComplianceTemplateItem { ComplianceTemplate = auditorTemplate, ServiceMaster = svcAdt1, McaFormMaster = formAdt1, Title = "File ADT-1", Description = "File ADT-1 within due date.", SequenceNo = 3, DefaultDueOffsetDays = 15, DueDateBasedOn = DueDateBasedOnTypes.EventDate, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
            new ComplianceTemplateItem { ComplianceTemplate = auditorTemplate, ServiceMaster = svcAdt1, Title = "Store SRN/challan", Description = "Record filing proof and close activity.", SequenceNo = 4, DefaultDueOffsetDays = 20, DueDateBasedOn = DueDateBasedOnTypes.EventDate, Priority = WorkItemPriorities.Medium }
        };

        var allTemplateItems = annualItems.Concat(directorKycItems).Concat(auditorItems).ToList();

        var checklistTemplates = new[]
        {
            new DocumentChecklistTemplate { ServiceMaster = svcAnnual, DocumentName = "Audited financial statement", Description = "Signed or final audited financials.", IsMandatory = true, DisplayOrder = 1 },
            new DocumentChecklistTemplate { ServiceMaster = svcAnnual, DocumentName = "Board report", Description = "Board report approved for circulation.", IsMandatory = true, DisplayOrder = 2 },
            new DocumentChecklistTemplate { ServiceMaster = svcAnnual, DocumentName = "Auditor report", Description = "Final auditor report copy.", IsMandatory = true, DisplayOrder = 3 },
            new DocumentChecklistTemplate { ServiceMaster = svcAgm, DocumentName = "AGM notice", Description = "Signed AGM notice and annexures.", IsMandatory = true, DisplayOrder = 4 },
            new DocumentChecklistTemplate { ServiceMaster = svcAgm, DocumentName = "AGM minutes", Description = "Final AGM minutes copy.", IsMandatory = true, DisplayOrder = 5 },
            new DocumentChecklistTemplate { ServiceMaster = svcAnnual, DocumentName = "List of shareholders", Description = "Updated shareholder list for annual return.", IsMandatory = true, DisplayOrder = 6 },
            new DocumentChecklistTemplate { ServiceMaster = svcAnnual, DocumentName = "Director details", Description = "Director KYC and DIN details.", IsMandatory = true, DisplayOrder = 7 },
            new DocumentChecklistTemplate { ServiceMaster = svcAoc4, DocumentName = "DSC confirmation", Description = "Valid DSC details and access confirmation.", IsMandatory = true, DisplayOrder = 8 },
            new DocumentChecklistTemplate { ServiceMaster = svcMgt7, DocumentName = "Previous year filing record", Description = "Last year SRN and challan reference.", IsMandatory = false, DisplayOrder = 9 }
        };

        var companies = new List<CompanyClient>
        {
            new()
            {
                Firm = firm,
                CompanyName = "Bluewave Technologies Private Limited",
                CIN = "U72900TG2020PTC123456",
                PAN = "AAECB1234F",
                IncorporationDate = new DateOnly(2020, 4, 12),
                RegisteredOfficeAddress = "Madhapur, Hyderabad, Telangana",
                CompanyType = CompanyClientTypes.PrivateLimited,
                RegistrationNumber = "123456",
                City = "Hyderabad",
                State = "Telangana",
                PinCode = "500081",
                CompanyClass = "Private Company Limited by Shares",
                Status = CompanyClientStatuses.Active,
                Email = "compliance@bluewave.in",
                Phone = "+91 90000 11111",
                ContactPersonName = "Rohit Sharma",
                FinancialYearEndMonth = 3,
                FinancialYearEndDay = 31,
                FinancialYearStart = new DateOnly(today.Month >= 4 ? today.Year : today.Year - 1, 4, 1),
                FinancialYearEnd = new DateOnly(today.Month >= 4 ? today.Year + 1 : today.Year, 3, 31),
                AuthorizedCapital = 1000000m,
                PaidUpCapital = 500000m,
                LastAGMDate = today.AddDays(-210),
                HasShareCapital = true,
                IsListed = false
            },
            new()
            {
                Firm = firm,
                CompanyName = "Greenleaf Ventures LLP",
                CIN = "AAX-7890",
                PAN = "AALFG2345K",
                IncorporationDate = new DateOnly(2021, 8, 2),
                RegisteredOfficeAddress = "Andheri East, Mumbai, Maharashtra",
                CompanyType = CompanyClientTypes.Llp,
                RegistrationNumber = "7890",
                City = "Mumbai",
                State = "Maharashtra",
                PinCode = "400093",
                CompanyClass = "Limited Liability Partnership",
                Status = CompanyClientStatuses.Active,
                Email = "accounts@greenleaf.in",
                Phone = "+91 90123 45678",
                ContactPersonName = "Nisha Patel",
                FinancialYearEndMonth = 3,
                FinancialYearEndDay = 31,
                FinancialYearStart = new DateOnly(today.Month >= 4 ? today.Year : today.Year - 1, 4, 1),
                FinancialYearEnd = new DateOnly(today.Month >= 4 ? today.Year + 1 : today.Year, 3, 31),
                HasShareCapital = false,
                IsListed = false
            },
            new()
            {
                Firm = firm,
                CompanyName = "Vertex Retail Limited",
                CIN = "L52100KA2018PLC654321",
                PAN = "AACCV3456L",
                IncorporationDate = new DateOnly(2018, 11, 19),
                RegisteredOfficeAddress = "Indiranagar, Bengaluru, Karnataka",
                CompanyType = CompanyClientTypes.PublicLimited,
                RegistrationNumber = "654321",
                City = "Bengaluru",
                State = "Karnataka",
                PinCode = "560038",
                CompanyClass = "Listed Public Company",
                Status = CompanyClientStatuses.Active,
                Email = "legal@vertexretail.in",
                Phone = "+91 99887 66554",
                ContactPersonName = "Meera Iyer",
                FinancialYearEndMonth = 3,
                FinancialYearEndDay = 31,
                FinancialYearStart = new DateOnly(today.Month >= 4 ? today.Year : today.Year - 1, 4, 1),
                FinancialYearEnd = new DateOnly(today.Month >= 4 ? today.Year + 1 : today.Year, 3, 31),
                AuthorizedCapital = 50000000m,
                PaidUpCapital = 25000000m,
                LastAGMDate = today.AddDays(-280),
                HasShareCapital = true,
                IsListed = true
            }
        };

        var directors = new List<Director>
        {
            new() { CompanyClient = companies[0], FullName = "Amit Verma", DIN = "01234567", PAN = "ABCPV1234D", Email = "amit.verma@bluewave.in", Phone = "+91 90001 11111", Address = "Madhapur, Hyderabad", Designation = "Director", AppointmentDate = new DateOnly(2020, 4, 12) },
            new() { CompanyClient = companies[0], FullName = "Neha Joshi", DIN = "07654321", PAN = "ADEPJ5678R", Email = "neha.joshi@bluewave.in", Phone = "+91 90002 22222", Address = "Jubilee Hills, Hyderabad", Designation = "Director", AppointmentDate = new DateOnly(2021, 3, 10) },
            new() { CompanyClient = companies[1], FullName = "Karan Mehta", DIN = "04561234", PAN = "AFCPM4567Q", Email = "karan@greenleaf.in", Phone = "+91 90123 98765", Address = "Andheri East, Mumbai", Designation = "Designated Partner", AppointmentDate = new DateOnly(2021, 8, 2) },
            new() { CompanyClient = companies[2], FullName = "Sonal Rao", DIN = "09876123", PAN = "ACDPR6789N", Email = "sonal@vertexretail.in", Phone = "+91 99887 33445", Address = "Indiranagar, Bengaluru", Designation = "Director", AppointmentDate = new DateOnly(2018, 11, 19) }
        };

        var tasks = new List<ComplianceTask>
        {
            new() { Firm = firm, CompanyClient = companies[0], Title = "Board Meeting Minutes Filing", Description = "Prepare and file minutes for Q1 board meeting.", ComplianceType = "Board Meeting", DueDate = today.AddDays(7), Status = ComplianceTaskStatuses.Pending, Priority = ComplianceTaskPriorities.High, AssignedTo = "Priya" },
            new() { Firm = firm, CompanyClient = companies[0], Title = "DIR-3 KYC", Description = "Complete annual director KYC submission.", ComplianceType = "MCA Filing", DueDate = today.AddDays(-3), Status = ComplianceTaskStatuses.InProgress, Priority = ComplianceTaskPriorities.Critical, AssignedTo = "Ravi" },
            new() { Firm = firm, CompanyClient = companies[1], Title = "LLP Form 11 Filing", Description = "Annual return filing for LLP.", ComplianceType = "Annual Filing", DueDate = today.AddDays(18), Status = ComplianceTaskStatuses.WaitingClient, Priority = ComplianceTaskPriorities.Medium, AssignedTo = "Anita" },
            new() { Firm = firm, CompanyClient = companies[2], Title = "MSME Half-Year Return", Description = "Check vendor ageing and submit return.", ComplianceType = "ROC Compliance", DueDate = today.AddDays(28), Status = ComplianceTaskStatuses.Pending, Priority = ComplianceTaskPriorities.Low, AssignedTo = "Suresh" },
            new() { Firm = firm, CompanyClient = companies[2], Title = "Annual General Meeting Filing", Description = "Upload AGM resolutions and related forms.", ComplianceType = "AGM", DueDate = today.AddDays(-12), Status = ComplianceTaskStatuses.Completed, Priority = ComplianceTaskPriorities.High, AssignedTo = "Priya", CompletedDate = today.AddDays(-10), Remarks = "Filed successfully." }
        };

        var planBluewave = new CompanyCompliancePlan
        {
            Firm = firm,
            CompanyClient = companies[0],
            ComplianceTemplate = annualTemplate,
            FinancialYear = "2025-26",
            PlanName = "FY 2025-26 Annual Compliance",
            StartDate = new DateOnly(2025, 4, 1),
            EndDate = new DateOnly(2026, 3, 31),
            AGMDate = today.AddDays(25),
            Status = CompanyCompliancePlanStatuses.InProgress,
            CreatedAt = now,
            UpdatedAt = now
        };

        var planVertex = new CompanyCompliancePlan
        {
            Firm = firm,
            CompanyClient = companies[2],
            ComplianceTemplate = directorKycTemplate,
            FinancialYear = "2025-26",
            PlanName = "Director KYC 2025-26",
            StartDate = today.AddDays(-15),
            EndDate = today.AddDays(45),
            Status = CompanyCompliancePlanStatuses.Active,
            CreatedAt = now,
            UpdatedAt = now
        };

        var bluewaveWorkItems = new List<WorkItem>
        {
            new() { Firm = firm, CompanyClient = companies[0], CompanyCompliancePlan = planBluewave, ServiceMaster = svcAnnual, Title = "Prepare financial statements", Description = "Coordinate trial balance, schedules and final signed statements.", FinancialYear = "2025-26", DueDate = today.AddDays(4), Status = WorkItemStatuses.InProgress, Priority = WorkItemPriorities.High, AssignedTo = "Priya", RequiresClientDocuments = true, ClientDocumentsStatus = WorkItemClientDocumentStatuses.PartiallyReceived, RequiresMcaFiling = false, FilingStatus = WorkItemFilingStatuses.NotRequired, Remarks = "Awaiting signed schedules from client.", CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], CompanyCompliancePlan = planBluewave, ServiceMaster = svcBoard, Title = "Conduct board meeting for financial approval", Description = "Notice and minutes for board approval.", FinancialYear = "2025-26", DueDate = today.AddDays(11), Status = WorkItemStatuses.WaitingClient, Priority = WorkItemPriorities.High, AssignedTo = "Ravi", RequiresClientDocuments = true, ClientDocumentsStatus = WorkItemClientDocumentStatuses.Pending, RequiresMcaFiling = false, FilingStatus = WorkItemFilingStatuses.NotRequired, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], CompanyCompliancePlan = planBluewave, ServiceMaster = svcAoc4, McaFormMaster = formAoc4, Title = "File AOC-4", Description = "Prepare and upload AOC-4 after AGM.", FinancialYear = "2025-26", DueDate = today.AddDays(55), Status = WorkItemStatuses.NotStarted, Priority = WorkItemPriorities.Critical, AssignedTo = "Anita", RequiresClientDocuments = true, ClientDocumentsStatus = WorkItemClientDocumentStatuses.Pending, RequiresMcaFiling = true, FilingStatus = WorkItemFilingStatuses.Pending, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], CompanyCompliancePlan = planBluewave, ServiceMaster = svcMgt7, McaFormMaster = formMgt7, Title = "File MGT-7 / MGT-7A", Description = "Prepare annual return and filing pack.", FinancialYear = "2025-26", DueDate = today.AddDays(82), Status = WorkItemStatuses.NotStarted, Priority = WorkItemPriorities.Critical, AssignedTo = "Anita", RequiresClientDocuments = true, ClientDocumentsStatus = WorkItemClientDocumentStatuses.Pending, RequiresMcaFiling = true, FilingStatus = WorkItemFilingStatuses.Pending, CreatedAt = now, UpdatedAt = now }
        };

        var vertexWorkItem = new WorkItem
        {
            Firm = firm,
            CompanyClient = companies[2],
            CompanyCompliancePlan = planVertex,
            ServiceMaster = svcDir3,
            McaFormMaster = formDir3,
            Title = "File DIR-3 KYC",
            Description = "Annual KYC for directors.",
            FinancialYear = "2025-26",
            DueDate = today.AddDays(-2),
            Status = WorkItemStatuses.Overdue,
            Priority = WorkItemPriorities.Critical,
            AssignedTo = "Suresh",
            RequiresClientDocuments = true,
            ClientDocumentsStatus = WorkItemClientDocumentStatuses.Pending,
            RequiresMcaFiling = true,
            FilingStatus = WorkItemFilingStatuses.Pending,
            CreatedAt = now,
            UpdatedAt = now
        };

        var workItems = bluewaveWorkItems.Append(vertexWorkItem).ToList();

        var workItemDocuments = new List<WorkItemDocument>
        {
            new() { Firm = firm, CompanyClient = companies[0], WorkItem = bluewaveWorkItems[0], DocumentName = "Audited financial statement", DocumentType = "Annual Compliance", IsMandatory = true, Status = WorkItemDocumentStatuses.Received, ReceivedDate = today.AddDays(-1), CreatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], WorkItem = bluewaveWorkItems[0], DocumentName = "Board report", DocumentType = "Annual Compliance", IsMandatory = true, Status = WorkItemDocumentStatuses.PendingFromClient, CreatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], WorkItem = bluewaveWorkItems[2], DocumentName = "DSC confirmation", DocumentType = "AOC-4", IsMandatory = true, Status = WorkItemDocumentStatuses.PendingFromClient, CreatedAt = now },
            new() { Firm = firm, CompanyClient = companies[2], WorkItem = vertexWorkItem, DocumentName = "Director KYC details", DocumentType = "DIR-3 KYC", IsMandatory = true, Status = WorkItemDocumentStatuses.PendingFromClient, CreatedAt = now }
        };

        var filings = new List<FilingRecord>
        {
            new() { Firm = firm, CompanyClient = companies[0], WorkItem = bluewaveWorkItems[2], McaFormMaster = formAoc4, FinancialYear = "2025-26", DueDate = today.AddDays(55), McaStatus = FilingRecordStatuses.NotStarted, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], WorkItem = bluewaveWorkItems[3], McaFormMaster = formMgt7, FinancialYear = "2025-26", DueDate = today.AddDays(82), McaStatus = FilingRecordStatuses.NotStarted, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[2], WorkItem = vertexWorkItem, McaFormMaster = formDir3, FinancialYear = "2025-26", DueDate = today.AddDays(-2), McaStatus = FilingRecordStatuses.Prepared, CreatedAt = now, UpdatedAt = now }
        };

        var meetings = new List<CompanyMeeting>
        {
            new() { Firm = firm, CompanyClient = companies[0], CompanyCompliancePlan = planBluewave, MeetingType = CompanyMeetingTypes.BoardMeeting, Title = "Board Meeting for Accounts Approval", MeetingDate = today.AddDays(10), DueDate = today.AddDays(10), VenueOrMode = "VC", NoticePrepared = true, AgendaPrepared = true, Status = CompanyMeetingStatuses.NoticePrepared, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], CompanyCompliancePlan = planBluewave, MeetingType = CompanyMeetingTypes.AGM, Title = "Annual General Meeting FY 2025-26", MeetingDate = today.AddDays(25), DueDate = today.AddDays(25), VenueOrMode = "Registered Office", NoticePrepared = false, AgendaPrepared = false, Status = CompanyMeetingStatuses.Planned, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[2], CompanyCompliancePlan = planVertex, MeetingType = CompanyMeetingTypes.BoardMeeting, Title = "Director KYC Review Meeting", MeetingDate = today.AddDays(5), DueDate = today.AddDays(5), VenueOrMode = "Google Meet", NoticePrepared = true, AgendaPrepared = true, Status = CompanyMeetingStatuses.Held, CreatedAt = now, UpdatedAt = now }
        };

        var followUps = new List<ClientFollowUp>
        {
            new() { Firm = firm, CompanyClient = companies[0], WorkItem = bluewaveWorkItems[0], FollowUpDate = today.AddDays(-1), FollowUpType = FollowUpTypes.WhatsApp, Subject = "Signed schedules follow-up", Notes = "Client promised to share signed schedules by evening.", NextFollowUpDate = today.AddDays(1), Status = ClientFollowUpStatuses.WaitingClient, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[0], WorkItem = bluewaveWorkItems[1], FollowUpDate = today.AddDays(-2), FollowUpType = FollowUpTypes.Email, Subject = "Board meeting date confirmation", Notes = "Requested board availability for approval meeting.", NextFollowUpDate = today.AddDays(2), Status = ClientFollowUpStatuses.Open, CreatedAt = now, UpdatedAt = now },
            new() { Firm = firm, CompanyClient = companies[2], WorkItem = vertexWorkItem, FollowUpDate = today.AddDays(-3), FollowUpType = FollowUpTypes.Call, Subject = "DIR-3 KYC documents pending", Notes = "No response on DSC confirmation and OTP coordination.", NextFollowUpDate = today, Status = ClientFollowUpStatuses.NoResponse, CreatedAt = now, UpdatedAt = now }
        };

        var statusHistory = new List<WorkItemStatusHistory>
        {
            new() { WorkItem = bluewaveWorkItems[0], OldStatus = WorkItemStatuses.NotStarted, NewStatus = WorkItemStatuses.InProgress, Remarks = "Work started after receipt of provisional financials.", ChangedBy = "Priya", ChangedAt = now.AddDays(-3) },
            new() { WorkItem = bluewaveWorkItems[1], OldStatus = WorkItemStatuses.NotStarted, NewStatus = WorkItemStatuses.WaitingClient, Remarks = "Waiting on board confirmation.", ChangedBy = "Ravi", ChangedAt = now.AddDays(-1) },
            new() { WorkItem = vertexWorkItem, OldStatus = WorkItemStatuses.InProgress, NewStatus = WorkItemStatuses.Overdue, Remarks = "Filing slipped due to missing OTP and DSC.", ChangedBy = "Suresh", ChangedAt = now.AddHours(-12) }
        };

        var invoices = new List<Invoice>
        {
            new() { Firm = firm, CompanyClient = companies[0], InvoiceNumber = "FP-2026-001", InvoiceDate = today.AddDays(-20), DueDate = today.AddDays(10), Amount = 15000m, TaxAmount = 2700m, TotalAmount = 17700m, Status = InvoiceStatuses.Sent, Remarks = "Quarterly secretarial support" },
            new() { Firm = firm, CompanyClient = companies[1], InvoiceNumber = "FP-2026-002", InvoiceDate = today.AddDays(-40), DueDate = today.AddDays(-5), Amount = 22000m, TaxAmount = 3960m, TotalAmount = 25960m, Status = InvoiceStatuses.Overdue, Remarks = "Annual LLP compliance package" },
            new() { Firm = firm, CompanyClient = companies[2], InvoiceNumber = "FP-2026-003", InvoiceDate = today.AddDays(-15), DueDate = today.AddDays(15), Amount = 32000m, TaxAmount = 5760m, TotalAmount = 37760m, Status = InvoiceStatuses.Paid, Remarks = "AGM and secretarial retainer" }
        };

        var payments = new List<Payment>
        {
            new() { Invoice = invoices[0], PaymentDate = today.AddDays(-5), Amount = 5000m, PaymentMode = "Bank Transfer", ReferenceNumber = "UTR10001", Remarks = "Advance received" },
            new() { Invoice = invoices[2], PaymentDate = today.AddDays(-3), Amount = 37760m, PaymentMode = "NEFT", ReferenceNumber = "UTR10002", Remarks = "Paid in full" }
        };

        context.Add(firm);
        context.AddRange(serviceCategories);
        context.AddRange(services);
        context.AddRange(forms);
        context.AddRange(templates);
        context.AddRange(allTemplateItems);
        context.AddRange(checklistTemplates);
        context.AddRange(companies);
        context.AddRange(directors);
        context.AddRange(tasks);
        context.AddRange(planBluewave, planVertex);
        context.AddRange(workItems);
        context.AddRange(workItemDocuments);
        context.AddRange(filings);
        context.AddRange(meetings);
        context.AddRange(followUps);
        context.AddRange(statusHistory);
        context.AddRange(invoices);
        context.AddRange(payments);

        await context.SaveChangesAsync();
        await SeedDocumentTemplatesAsync(context);
    }

    private static async Task SeedSystemMastersAsync(FirmPulseDbContext context)
    {
        var now = DateTime.UtcNow;

        if (!await context.ServiceCategories.AnyAsync())
        {
            var categories = new[]
            {
                new ServiceCategory { Name = "Annual Compliance", Description = "Recurring annual secretarial and filing work.", DisplayOrder = 1, CreatedAt = now, UpdatedAt = now },
                new ServiceCategory { Name = "MCA Filing", Description = "Statutory forms filed on MCA portal.", DisplayOrder = 2, CreatedAt = now, UpdatedAt = now },
                new ServiceCategory { Name = "Meeting Documentation", Description = "Board, AGM, EGM and committee meeting work.", DisplayOrder = 3, CreatedAt = now, UpdatedAt = now },
                new ServiceCategory { Name = "Incorporation", Description = "Entity setup and registration support.", DisplayOrder = 4, CreatedAt = now, UpdatedAt = now },
                new ServiceCategory { Name = "Event Based Compliance", Description = "One-time event filings and changes.", DisplayOrder = 5, CreatedAt = now, UpdatedAt = now },
                new ServiceCategory { Name = "LLP Compliance", Description = "LLP annual and event-based compliance.", DisplayOrder = 6, CreatedAt = now, UpdatedAt = now }
            };
            context.ServiceCategories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.ServiceMasters.AnyAsync())
        {
            var categories = await context.ServiceCategories.OrderBy(x => x.DisplayOrder).ToListAsync();
            var annual = categories.First(x => x.Name == "Annual Compliance");
            var filing = categories.First(x => x.Name == "MCA Filing");
            var meetings = categories.First(x => x.Name == "Meeting Documentation");
            var events = categories.First(x => x.Name == "Event Based Compliance");
            var llp = categories.First(x => x.Name == "LLP Compliance");

            context.ServiceMasters.AddRange(
                new ServiceMaster { ServiceCategoryId = annual.Id, ServiceCode = "ANNUAL-001", ServiceName = "Annual Filing", Description = "End-to-end annual filing support.", DefaultFee = 25000m, DefaultDueDays = 180, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = filing.Id, ServiceCode = "MCA-AOC4", ServiceName = "AOC-4 Filing", Description = "Preparation and filing of AOC-4.", DefaultFee = 8500m, DefaultDueDays = 30, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = filing.Id, ServiceCode = "MCA-MGT7", ServiceName = "MGT-7 / MGT-7A Filing", Description = "Annual return filing support.", DefaultFee = 8500m, DefaultDueDays = 60, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = filing.Id, ServiceCode = "MCA-DIR3", ServiceName = "DIR-3 KYC", Description = "Annual DIR-3 KYC filing.", DefaultFee = 3000m, DefaultDueDays = 30, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = filing.Id, ServiceCode = "MCA-ADT1", ServiceName = "ADT-1 Auditor Appointment", Description = "Auditor appointment filing.", DefaultFee = 5000m, DefaultDueDays = 15, IsRecurring = false, RecurringType = ServiceRecurringTypes.EventBased, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = meetings.Id, ServiceCode = "MEET-BOARD", ServiceName = "Board Meeting Documentation", Description = "Board meeting notices, agenda, resolutions and minutes.", DefaultFee = 7000m, DefaultDueDays = 7, IsRecurring = true, RecurringType = ServiceRecurringTypes.Quarterly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = meetings.Id, ServiceCode = "MEET-AGM", ServiceName = "AGM Documentation", Description = "AGM planning, notices and minutes.", DefaultFee = 9000m, DefaultDueDays = 30, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = annual.Id, ServiceCode = "STAT-REG", ServiceName = "Statutory Register Maintenance", Description = "Maintain statutory registers and records.", DefaultFee = 12000m, DefaultDueDays = 365, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = llp.Id, ServiceCode = "LLP-ANNUAL", ServiceName = "LLP Annual Filing", Description = "LLP Form 8 and Form 11 annual support.", DefaultFee = 18000m, DefaultDueDays = 180, IsRecurring = true, RecurringType = ServiceRecurringTypes.Yearly, CreatedAt = now, UpdatedAt = now },
                new ServiceMaster { ServiceCategoryId = events.Id, ServiceCode = "EVENT-ROC", ServiceName = "Event Based Filing", Description = "ROC filings for corporate changes.", DefaultFee = 15000m, DefaultDueDays = 15, IsRecurring = false, RecurringType = ServiceRecurringTypes.EventBased, CreatedAt = now, UpdatedAt = now }
            );
            await context.SaveChangesAsync();
        }

        if (!await context.McaFormMasters.AnyAsync())
        {
            context.McaFormMasters.AddRange(
                new McaFormMaster { FormCode = "AOC-4", FormName = "AOC-4 Filing", Description = "Financial statement filing.", ApplicableTo = "Company", DefaultDueRule = "Within 30 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "AOC-4 XBRL", FormName = "AOC-4 XBRL Filing", Description = "XBRL financial statement filing.", ApplicableTo = "Specified Company", DefaultDueRule = "Within 30 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "MGT-7", FormName = "MGT-7 Annual Return", Description = "Annual return filing.", ApplicableTo = "Company", DefaultDueRule = "Within 60 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "MGT-7A", FormName = "MGT-7A Annual Return", Description = "Annual return for small companies.", ApplicableTo = "Small Company / OPC", DefaultDueRule = "Within 60 days of AGM", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "DIR-3 KYC", FormName = "DIR-3 KYC", Description = "Director KYC filing.", ApplicableTo = "Director", DefaultDueRule = "On or before 30 September", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "ADT-1", FormName = "ADT-1", Description = "Auditor appointment filing.", ApplicableTo = "Company", DefaultDueRule = "Within 15 days of AGM / appointment", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "DPT-3", FormName = "DPT-3", Description = "Return of deposits / exempted amounts.", ApplicableTo = "Company", DefaultDueRule = "On or before 30 June", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "MSME-1", FormName = "MSME-1", Description = "Half-yearly MSME return.", ApplicableTo = "Company", DefaultDueRule = "Half-yearly", CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "PAS-3", FormName = "PAS-3", Description = "Return of allotment.", ApplicableTo = "Company", DefaultDueRule = "Within 15 days of allotment", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "MGT-14", FormName = "MGT-14", Description = "Board/shareholder resolution filing.", ApplicableTo = "Company", DefaultDueRule = "Within 30 days of resolution", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "SH-7", FormName = "SH-7", Description = "Alteration of share capital.", ApplicableTo = "Company", DefaultDueRule = "Within 30 days of change", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "INC-20A", FormName = "INC-20A", Description = "Declaration for commencement of business.", ApplicableTo = "Company", DefaultDueRule = "Within 180 days of incorporation", IsEventBasedForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "LLP Form 8", FormName = "LLP Form 8", Description = "Statement of account & solvency.", ApplicableTo = "LLP", DefaultDueRule = "On or before 30 October", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now },
                new McaFormMaster { FormCode = "LLP Form 11", FormName = "LLP Form 11", Description = "Annual return for LLP.", ApplicableTo = "LLP", DefaultDueRule = "On or before 30 May", IsAnnualForm = true, CreatedAt = now, UpdatedAt = now }
            );
            await context.SaveChangesAsync();
        }

        if (!await context.ComplianceTemplates.AnyAsync())
        {
            var services = await context.ServiceMasters.ToListAsync();
            var forms = await context.McaFormMasters.ToListAsync();

            var annualTemplate = new ComplianceTemplate { TemplateName = "Private Limited Annual Compliance", Description = "Default annual compliance workflow for private limited companies.", CompanyType = CompanyClientTypes.PrivateLimited, FinancialYearBased = true, IsSystemTemplate = true, CreatedAt = now, UpdatedAt = now };
            var directorKycTemplate = new ComplianceTemplate { TemplateName = "Director KYC Compliance", Description = "Collect KYC and complete annual DIR-3 KYC filing.", CompanyType = CompanyClientTypes.PrivateLimited, FinancialYearBased = false, IsSystemTemplate = true, CreatedAt = now, UpdatedAt = now };
            var auditorTemplate = new ComplianceTemplate { TemplateName = "Auditor Appointment", Description = "Prepare resolutions and file ADT-1 for appointment.", CompanyType = CompanyClientTypes.PrivateLimited, FinancialYearBased = false, IsSystemTemplate = true, CreatedAt = now, UpdatedAt = now };

            context.ComplianceTemplates.AddRange(annualTemplate, directorKycTemplate, auditorTemplate);
            await context.SaveChangesAsync();

            var svcAnnual = services.First(x => x.ServiceCode == "ANNUAL-001");
            var svcAoc4 = services.First(x => x.ServiceCode == "MCA-AOC4");
            var svcMgt7 = services.First(x => x.ServiceCode == "MCA-MGT7");
            var svcDir3 = services.First(x => x.ServiceCode == "MCA-DIR3");
            var svcAdt1 = services.First(x => x.ServiceCode == "MCA-ADT1");
            var svcBoard = services.First(x => x.ServiceCode == "MEET-BOARD");
            var svcAgm = services.First(x => x.ServiceCode == "MEET-AGM");
            var svcRegisters = services.First(x => x.ServiceCode == "STAT-REG");

            var formAoc4 = forms.First(x => x.FormCode == "AOC-4");
            var formMgt7 = forms.First(x => x.FormCode == "MGT-7");
            var formDir3 = forms.First(x => x.FormCode == "DIR-3 KYC");
            var formAdt1 = forms.First(x => x.FormCode == "ADT-1");

            context.ComplianceTemplateItems.AddRange(
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcAnnual.Id, Title = "Prepare financial statements", SequenceNo = 1, DefaultDueOffsetDays = 10, DueDateBasedOn = DueDateBasedOnTypes.FinancialYearEnd, RequiresDocuments = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcAnnual.Id, Title = "Prepare board report", SequenceNo = 2, DefaultDueOffsetDays = 25, DueDateBasedOn = DueDateBasedOnTypes.FinancialYearEnd, RequiresDocuments = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcBoard.Id, Title = "Conduct board meeting for financial approval", SequenceNo = 3, DefaultDueOffsetDays = 60, DueDateBasedOn = DueDateBasedOnTypes.FinancialYearEnd, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcAgm.Id, Title = "Prepare AGM notice", SequenceNo = 4, DefaultDueOffsetDays = -7, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcAgm.Id, Title = "Conduct AGM", SequenceNo = 5, DefaultDueOffsetDays = 0, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.Critical },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcAoc4.Id, McaFormMasterId = formAoc4.Id, Title = "File AOC-4", SequenceNo = 6, DefaultDueOffsetDays = 30, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcMgt7.Id, McaFormMasterId = formMgt7.Id, Title = "File MGT-7 / MGT-7A", SequenceNo = 7, DefaultDueOffsetDays = 60, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcAnnual.Id, Title = "Store SRN and challan", SequenceNo = 8, DefaultDueOffsetDays = 65, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, Priority = WorkItemPriorities.Medium },
                new ComplianceTemplateItem { ComplianceTemplateId = annualTemplate.Id, ServiceMasterId = svcRegisters.Id, Title = "Close annual compliance", SequenceNo = 9, DefaultDueOffsetDays = 75, DueDateBasedOn = DueDateBasedOnTypes.AGMDate, Priority = WorkItemPriorities.Medium },
                new ComplianceTemplateItem { ComplianceTemplateId = directorKycTemplate.Id, ServiceMasterId = svcDir3.Id, Title = "Collect director KYC details", SequenceNo = 1, DefaultDueOffsetDays = 0, DueDateBasedOn = DueDateBasedOnTypes.Manual, RequiresDocuments = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = directorKycTemplate.Id, ServiceMasterId = svcDir3.Id, Title = "Verify DIN/PAN", SequenceNo = 2, DefaultDueOffsetDays = 5, DueDateBasedOn = DueDateBasedOnTypes.Manual, RequiresDocuments = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = directorKycTemplate.Id, ServiceMasterId = svcDir3.Id, McaFormMasterId = formDir3.Id, Title = "File DIR-3 KYC", SequenceNo = 3, DefaultDueOffsetDays = 15, DueDateBasedOn = DueDateBasedOnTypes.Manual, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
                new ComplianceTemplateItem { ComplianceTemplateId = directorKycTemplate.Id, ServiceMasterId = svcDir3.Id, Title = "Store SRN/challan", SequenceNo = 4, DefaultDueOffsetDays = 20, DueDateBasedOn = DueDateBasedOnTypes.Manual, Priority = WorkItemPriorities.Medium },
                new ComplianceTemplateItem { ComplianceTemplateId = auditorTemplate.Id, ServiceMasterId = svcAdt1.Id, Title = "Prepare board resolution", SequenceNo = 1, DefaultDueOffsetDays = 0, DueDateBasedOn = DueDateBasedOnTypes.EventDate, RequiresDocuments = true, RequiresMeeting = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = auditorTemplate.Id, ServiceMasterId = svcAdt1.Id, Title = "Prepare ADT-1 details", SequenceNo = 2, DefaultDueOffsetDays = 5, DueDateBasedOn = DueDateBasedOnTypes.EventDate, RequiresDocuments = true, Priority = WorkItemPriorities.High },
                new ComplianceTemplateItem { ComplianceTemplateId = auditorTemplate.Id, ServiceMasterId = svcAdt1.Id, McaFormMasterId = formAdt1.Id, Title = "File ADT-1", SequenceNo = 3, DefaultDueOffsetDays = 15, DueDateBasedOn = DueDateBasedOnTypes.EventDate, RequiresDocuments = true, RequiresFiling = true, Priority = WorkItemPriorities.Critical },
                new ComplianceTemplateItem { ComplianceTemplateId = auditorTemplate.Id, ServiceMasterId = svcAdt1.Id, Title = "Store SRN/challan", SequenceNo = 4, DefaultDueOffsetDays = 20, DueDateBasedOn = DueDateBasedOnTypes.EventDate, Priority = WorkItemPriorities.Medium }
            );
            await context.SaveChangesAsync();

            if (!await context.DocumentChecklistTemplates.AnyAsync())
            {
                context.DocumentChecklistTemplates.AddRange(
                    new DocumentChecklistTemplate { ServiceMasterId = svcAnnual.Id, DocumentName = "Audited financial statement", Description = "Signed or final audited financials.", IsMandatory = true, DisplayOrder = 1 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcAnnual.Id, DocumentName = "Board report", Description = "Board report approved for circulation.", IsMandatory = true, DisplayOrder = 2 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcAnnual.Id, DocumentName = "Auditor report", Description = "Final auditor report copy.", IsMandatory = true, DisplayOrder = 3 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcAgm.Id, DocumentName = "AGM notice", Description = "Signed AGM notice and annexures.", IsMandatory = true, DisplayOrder = 4 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcAgm.Id, DocumentName = "AGM minutes", Description = "Final AGM minutes copy.", IsMandatory = true, DisplayOrder = 5 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcAnnual.Id, DocumentName = "List of shareholders", Description = "Updated shareholder list for annual return.", IsMandatory = true, DisplayOrder = 6 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcAnnual.Id, DocumentName = "Director details", Description = "Director KYC and DIN details.", IsMandatory = true, DisplayOrder = 7 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcAoc4.Id, DocumentName = "DSC confirmation", Description = "Valid DSC details and access confirmation.", IsMandatory = true, DisplayOrder = 8 },
                    new DocumentChecklistTemplate { ServiceMasterId = svcMgt7.Id, DocumentName = "Previous year filing record", Description = "Last year SRN and challan reference.", IsMandatory = false, DisplayOrder = 9 }
                );
                await context.SaveChangesAsync();
            }
        }

        await SeedDocumentTemplatesAsync(context);
    }

    private static async Task SeedDocumentTemplatesAsync(FirmPulseDbContext context)
    {
        if (!await DocumentTemplateTableExistsAsync(context))
        {
            return;
        }

        if (await context.DocumentTemplates.AnyAsync())
        {
            return;
        }

        var now = DateTime.UtcNow;
        var templates = new[]
        {
            CreateDocumentTemplate(
                "Board Resolution - Appointment of Director",
                "Board Documents",
                "Resolution draft for appointment of a director.",
                """
                CERTIFIED TRUE COPY OF THE RESOLUTION PASSED AT THE MEETING OF THE BOARD OF DIRECTORS OF {{CompanyName}} HELD ON {{MeetingDate}} AT {{RegisteredOfficeAddress}}

                "RESOLVED THAT Mr./Ms. {{DirectorName}}, having DIN {{DIN}}, be and is hereby appointed as Director of the Company with effect from {{AppointmentDate}}."

                RESOLVED FURTHER THAT any Director of the Company be and is hereby authorized to do all such acts, deeds and things as may be necessary to give effect to this resolution.
                """,
                now),
            CreateDocumentTemplate(
                "Board Meeting Notice",
                "Board Documents",
                "Notice draft for calling a board meeting.",
                """
                NOTICE is hereby given that a meeting of the Board of Directors of {{CompanyName}} having CIN {{CIN}} will be held on {{MeetingDate}} at {{RegisteredOfficeAddress}}.

                The agenda for the meeting includes consideration of routine business and such other matters as may be placed before the Board.

                For {{CompanyName}}
                {{DirectorName}}
                {{Designation}}
                """,
                now),
            CreateDocumentTemplate(
                "DIR-2 Consent Draft",
                "Director Documents",
                "Consent draft for a person proposed to be appointed as director.",
                """
                To,
                The Board of Directors
                {{CompanyName}}
                {{RegisteredOfficeAddress}}

                Subject: Consent to act as Director

                I, {{DirectorName}}, having DIN {{DIN}}, hereby give my consent to act as Director of {{CompanyName}} with effect from {{AppointmentDate}}.

                Name: {{DirectorName}}
                DIN: {{DIN}}
                Address: {{DirectorAddress}}
                Date: {{ConsentDate}}
                """,
                now)
        };

        context.DocumentTemplates.AddRange(templates);
        await context.SaveChangesAsync();
    }

    private static DocumentTemplate CreateDocumentTemplate(string name, string category, string description, string content, DateTime now)
    {
        var template = new DocumentTemplate
        {
            TemplateName = name,
            TemplateCategory = category,
            Description = description,
            TemplateContent = content,
            IsActive = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        var fields = ExtractTemplateFields(content);
        for (var index = 0; index < fields.Count; index++)
        {
            var field = fields[index];
            template.Fields.Add(new DocumentTemplateField
            {
                FieldKey = field,
                FieldLabel = SplitFieldLabel(field),
                FieldType = field.Contains("Date", StringComparison.OrdinalIgnoreCase) ? "Date" : "Text",
                DataSource = GetFieldDataSource(field),
                IsRequired = true,
                DisplayOrder = index + 1
            });
        }

        return template;
    }

    private static List<string> ExtractTemplateFields(string content)
    {
        var matches = System.Text.RegularExpressions.Regex.Matches(content, @"\{\{\s*(?<key>[A-Za-z0-9_]+)\s*\}\}");
        return matches
            .Select(match => match.Groups["key"].Value.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static string SplitFieldLabel(string key)
    {
        return System.Text.RegularExpressions.Regex.Replace(key, "([a-z])([A-Z])", "$1 $2");
    }

    private static string? GetFieldDataSource(string key)
    {
        var companyFields = new[] { "CompanyName", "CIN", "RegisteredOfficeAddress" };
        var directorFields = new[] { "DirectorName", "DIN", "DirectorAddress", "Designation", "AppointmentDate" };

        if (companyFields.Contains(key, StringComparer.OrdinalIgnoreCase))
        {
            return "Company";
        }

        if (directorFields.Contains(key, StringComparer.OrdinalIgnoreCase))
        {
            return "Director";
        }

        return null;
    }

    private static async Task<bool> DocumentTemplateTableExistsAsync(FirmPulseDbContext context)
    {
        var connection = context.Database.GetDbConnection();
        var shouldClose = connection.State == System.Data.ConnectionState.Closed;

        if (shouldClose)
        {
            await connection.OpenAsync();
        }

        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = """
                select exists (
                    select 1
                    from information_schema.tables
                    where table_schema = 'public'
                      and table_name = 'DocumentTemplates'
                )
                """;

            var result = await command.ExecuteScalarAsync();
            return result is bool exists && exists;
        }
        finally
        {
            if (shouldClose)
            {
                await connection.CloseAsync();
            }
        }
    }
}
