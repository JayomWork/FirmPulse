using FirmPulse.Components;
using FirmPulse.Data;
using FirmPulse.Seed;
using FirmPulse.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<FirmPulseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICompanyClientService, CompanyClientService>();
builder.Services.AddScoped<IComplianceTaskService, ComplianceTaskService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IServiceMasterService, ServiceMasterService>();
builder.Services.AddScoped<IComplianceTemplateService, ComplianceTemplateService>();
builder.Services.AddScoped<ICompanyCompliancePlanService, CompanyCompliancePlanService>();
builder.Services.AddScoped<IWorkItemService, WorkItemService>();
builder.Services.AddScoped<IFilingRecordService, FilingRecordService>();
builder.Services.AddScoped<IWorkItemDocumentService, WorkItemDocumentService>();
builder.Services.AddScoped<IClientFollowUpService, ClientFollowUpService>();
builder.Services.AddScoped<ICompanyMeetingService, CompanyMeetingService>();
builder.Services.AddScoped<IDocumentTemplateService, DocumentTemplateService>();
builder.Services.AddScoped<IDocumentGenerationService, DocumentGenerationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FirmPulseDbContext>();
    await context.Database.MigrateAsync();
    await FirmPulseSeedData.SeedAsync(context);
}

app.Run();
