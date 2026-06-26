using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;
using PC212206.U202318609.Capbase.Platform.Shared.Domain.Repositories;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Repositories;

/// <summary>
///     Defines the contract for covenant persistence operations.
/// </summary>
public interface ICovenantRepository : IBaseRepository<Covenant>
{
    /// <summary>
    ///     Determines whether a covenant already exists for the provided document identifier.
    /// </summary>
    /// <param name="documentIdentifier">The document identifier value.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> when the document identifier already exists; otherwise <c>false</c>.</returns>
    Task<bool> ExistsByDocumentIdentifierAsync(Guid documentIdentifier, CancellationToken cancellationToken);
}
