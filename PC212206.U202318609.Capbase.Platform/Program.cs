using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;
using PC212206.U202318609.Capbase.Platform.Paperwork.Application.CommandServices;
using PC212206.U202318609.Capbase.Platform.Paperwork.Application.Internal.CommandServices;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Repositories;
using PC212206.U202318609.Capbase.Platform.Paperwork.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using PC212206.U202318609.Capbase.Platform.Shared.Domain.Repositories;
using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Capbase Platform API",
        Version = "v1",
        Description = "REST API for covenant management."
    });
    options.EnableAnnotations();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Database connection string is not configured.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString);

    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    }
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICovenantRepository, CovenantRepository>();
builder.Services.AddScoped<ICovenantCommandService, CovenantCommandService>();

var app = builder.Build();

var supportedCultures = new[] { "en", "en-US", "es", "es-PE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
