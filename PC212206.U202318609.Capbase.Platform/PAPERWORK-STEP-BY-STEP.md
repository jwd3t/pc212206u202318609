# Paperwork Bounded Context Step by Step

This guide explains how the `Paperwork` bounded context was built on top of the base template that already contains the `Shared` bounded context.

## 1. Create Shared Business Value Objects

The statement places `LegalityPeriod` and `MonetaryAmount` inside the `Shared` bounded context, so they must be created before building `Paperwork`.

Files created:

- `Shared/Domain/Model/ValueObjects/LegalityPeriod.cs`
- `Shared/Domain/Model/ValueObjects/MonetaryAmount.cs`

Example:

```csharp
public class LegalityPeriod
{
    public LegalityPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (endDate <= startDate)
            throw new ArgumentException("End date must be greater than the start date.", nameof(endDate));

        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public bool IsExpired(DateOnly checkDate) => checkDate > EndDate;
}
```

## 2. Create the Paperwork Domain Model

The domain layer contains the business model and business rules.

Files created:

- `Paperwork/Domain/Model/PaperworkError.cs`
- `Paperwork/Domain/Model/ValueObjects/CapbaseIdentifier.cs`
- `Paperwork/Domain/Model/ValueObjects/PartyId.cs`
- `Paperwork/Domain/Model/ValueObjects/CovenantStatus.cs`
- `Paperwork/Domain/Model/Commands/CreateCovenantCommand.cs`
- `Paperwork/Domain/Model/Aggregates/Covenant.cs`
- `Paperwork/Domain/Model/Aggregates/CovenantAudit.cs`
- `Paperwork/Domain/Repositories/ICovenantRepository.cs`

Example:

```csharp
public partial class Covenant
{
    public Covenant(
        CapbaseIdentifier documentIdentifier,
        PartyId clientId,
        LegalityPeriod period,
        MonetaryAmount totalValue,
        CovenantStatus status = CovenantStatus.Draft,
        string? footnotes = null)
    {
        DocumentIdentifier = documentIdentifier;
        ClientId = clientId;
        Period = period;
        TotalValue = totalValue;
        Status = status;
        Footnotes = string.IsNullOrWhiteSpace(footnotes) ? null : footnotes.Trim();
    }

    public int Id { get; private set; }
    public CapbaseIdentifier DocumentIdentifier { get; private set; }
    public PartyId ClientId { get; private set; }
    public LegalityPeriod Period { get; private set; }
    public MonetaryAmount TotalValue { get; private set; }
    public CovenantStatus Status { get; private set; }
    public string? Footnotes { get; private set; }
}
```

## 3. Create the Persistence Mapping

The infrastructure layer maps the domain model to the database and implements the repository.

Files created:

- `Paperwork/Infrastructure/Persistence/EntityFrameworkCore/Configuration/Extensions/ModelBuilderExtensions.cs`
- `Paperwork/Infrastructure/Persistence/EntityFrameworkCore/Repositories/CovenantRepository.cs`

Key idea:

```csharp
builder.Entity<Covenant>(entity =>
{
    entity.HasKey(c => c.Id);
    entity.Property(c => c.Id).ValueGeneratedOnAdd();
    entity.Property(c => c.Status).HasConversion<string>().IsRequired();

    entity.OwnsOne(c => c.DocumentIdentifier, owned =>
    {
        owned.Property(p => p.Value).HasColumnName("document_identifier").IsRequired();
        owned.HasIndex(p => p.Value).IsUnique();
    });
});
```

## 4. Plug the Bounded Context into AppDbContext

After creating the persistence mapping, the bounded context must be registered in `AppDbContext`.

File updated:

- `Shared/Infrastructure/Persistence/EntityFrameworkCore/Configuration/AppDbContext.cs`

Code:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.ApplyPaperworkConfiguration();
    builder.UseSnakeCaseNamingConvention();
}
```

## 5. Create the Application Service

The application layer orchestrates validation, duplicate checks, persistence, and result creation.

Files created:

- `Paperwork/Application/CommandServices/ICovenantCommandService.cs`
- `Paperwork/Application/Internal/CommandServices/CovenantCommandService.cs`

Example:

```csharp
if (!Guid.TryParse(command.DocumentId, out var documentIdentifier))
    return Result<Covenant>.Failure(
        PaperworkError.InvalidDocumentIdentifier,
        localizer[nameof(PaperworkError.InvalidDocumentIdentifier)]);

if (await covenantRepository.ExistsByDocumentIdentifierAsync(documentIdentifier, cancellationToken))
    return Result<Covenant>.Failure(
        PaperworkError.DuplicateDocumentIdentifier,
        localizer[nameof(PaperworkError.DuplicateDocumentIdentifier)]);
```

## 6. Create REST Resources and Assemblers

The interfaces layer needs request/response resources and small mapping helpers.

Files created:

- `Paperwork/Interfaces/Rest/Resources/CreateCovenantResource.cs`
- `Paperwork/Interfaces/Rest/Resources/CovenantResource.cs`
- `Paperwork/Interfaces/Rest/Transform/CreateCovenantCommandFromResourceAssembler.cs`
- `Paperwork/Interfaces/Rest/Transform/CovenantResourceFromEntityAssembler.cs`
- `Paperwork/Interfaces/Rest/Transform/PaperworkActionResultAssembler.cs`

Example:

```csharp
public static CovenantResource ToResourceFromEntity(Covenant entity)
{
    return new CovenantResource(
        entity.Id,
        entity.DocumentIdentifier.ToString(),
        entity.ClientId.ToString(),
        entity.Period.StartDate.ToString("yyyy-MM-dd"),
        entity.Period.EndDate.ToString("yyyy-MM-dd"),
        entity.TotalValue.Value,
        entity.TotalValue.Currency,
        entity.Status.ToString(),
        entity.Footnotes);
}
```

## 7. Create the Controller

The controller exposes the only required operation: `POST /api/v1/covenants`.

File created:

- `Paperwork/Interfaces/Rest/CovenantsController.cs`

Example:

```csharp
[HttpPost]
public async Task<IActionResult> CreateCovenant(
    [FromBody] CreateCovenantResource resource,
    CancellationToken cancellationToken)
{
    var command = CreateCovenantCommandFromResourceAssembler.ToCommandFromResource(resource);
    var result = await covenantCommandService.Handle(command, cancellationToken);

    return PaperworkActionResultAssembler.ToActionResultFromCreateCovenantResult(
        this,
        result,
        commonLocalizer,
        created => StatusCode(StatusCodes.Status201Created, CovenantResourceFromEntityAssembler.ToResourceFromEntity(created)));
}
```

## 8. Add Localization Resources

Localized messages are stored in:

- `Paperwork/Resources/PaperworkMessages.resx`
- `Paperwork/Resources/PaperworkMessages.es.resx`

These messages are used by the application service when validation or persistence errors happen.

## 9. Register Everything in Program.cs

The final step is wiring the bounded context into the application startup.

Important registrations:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICovenantRepository, CovenantRepository>();
builder.Services.AddScoped<ICovenantCommandService, CovenantCommandService>();
```

The project also enables:

- Swagger/OpenAPI
- Request localization
- Global exception handling
- Kebab-case routes

## 10. Next Steps

After this bounded context is in place, the next recommended steps are:

1. Create the first EF Core migration.
2. Run the API locally.
3. Test `POST /api/v1/covenants` from Swagger.
4. Adjust `README.md` and XML documentation remarks with your full name if required by the exam.
