using PC212206.U202318609.Capbase.Platform.Shared.Domain.Repositories;
using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Unit of work for the application.
/// </summary>
/// <remarks>
///     This class is used to save changes to the database context.
///     It implements the IUnitOfWork interface.
/// </remarks>
/// <param name="context">
///     The database context for the application
/// </param>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    // inheritedDoc
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}