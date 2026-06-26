using PC212206.U202318609.Capbase.Platform.Shared.Domain.Model.Entities;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;

/// <summary>
///     Adds persistence-managed audit timestamps to the covenant aggregate.
/// </summary>
public partial class Covenant : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
