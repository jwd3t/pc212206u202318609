using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.ValueObjects;
using PC212206.U202318609.Capbase.Platform.Shared.Domain.Model.ValueObjects;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;

/// <summary>
///     Represents the Covenant aggregate root in the Paperwork bounded context.
/// </summary>
public partial class Covenant
{
    private Covenant()
    {
        DocumentIdentifier = null!;
        ClientId = null!;
        Period = null!;
        TotalValue = null!;
        Footnotes = null;
    }

    /// <summary>
    ///     Initializes a new covenant aggregate.
    /// </summary>
    /// <param name="documentIdentifier">The covenant document identifier.</param>
    /// <param name="clientId">The covenant client identifier.</param>
    /// <param name="period">The covenant legal period.</param>
    /// <param name="totalValue">The covenant monetary amount.</param>
    /// <param name="status">The covenant status.</param>
    /// <param name="footnotes">Optional covenant footnotes.</param>
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

    /// <summary>
    ///     Gets the aggregate identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the covenant document identifier.
    /// </summary>
    public CapbaseIdentifier DocumentIdentifier { get; private set; }

    /// <summary>
    ///     Gets the covenant client identifier.
    /// </summary>
    public PartyId ClientId { get; private set; }

    /// <summary>
    ///     Gets the covenant legal period.
    /// </summary>
    public LegalityPeriod Period { get; private set; }

    /// <summary>
    ///     Gets the covenant total monetary value.
    /// </summary>
    public MonetaryAmount TotalValue { get; private set; }

    /// <summary>
    ///     Gets the covenant status.
    /// </summary>
    public CovenantStatus Status { get; private set; }

    /// <summary>
    ///     Gets optional covenant footnotes.
    /// </summary>
    public string? Footnotes { get; private set; }
}
