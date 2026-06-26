using Microsoft.EntityFrameworkCore;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Repositories;
using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Implements covenant persistence using Entity Framework Core.
/// </summary>
public class CovenantRepository(AppDbContext context)
    : BaseRepository<Covenant>(context), ICovenantRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByDocumentIdentifierAsync(Guid documentIdentifier, CancellationToken cancellationToken)
    {
        return await Context.Set<Covenant>()
            .AnyAsync(c => c.DocumentIdentifier.Value == documentIdentifier, cancellationToken);
    }
}
