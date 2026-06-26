namespace PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Resources;

/// <summary>
///     Represents the request body used to create a covenant.
/// </summary>
/// <param name="DocumentId">The covenant document identifier.</param>
/// <param name="ClientId">The covenant client identifier.</param>
/// <param name="PeriodStartDate">The covenant period start date.</param>
/// <param name="PeriodEndDate">The covenant period end date.</param>
/// <param name="MonetaryAmountValue">The covenant total monetary value.</param>
/// <param name="MonetaryAmountCurrency">The covenant monetary currency.</param>
/// <param name="Status">The optional covenant status.</param>
/// <param name="Footnotes">Optional covenant footnotes.</param>
public record CreateCovenantResource(
    string DocumentId,
    string ClientId,
    DateOnly PeriodStartDate,
    DateOnly PeriodEndDate,
    decimal MonetaryAmountValue,
    string MonetaryAmountCurrency,
    string? Status,
    string? Footnotes);
