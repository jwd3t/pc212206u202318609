namespace PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Resources;

/// <summary>
///     Represents the API resource returned for a covenant.
/// </summary>
/// <param name="Id">The covenant identifier.</param>
/// <param name="DocumentId">The covenant document identifier.</param>
/// <param name="ClientId">The covenant client identifier.</param>
/// <param name="PeriodStartDate">The covenant period start date.</param>
/// <param name="PeriodEndDate">The covenant period end date.</param>
/// <param name="MonetaryAmountValue">The covenant monetary amount value.</param>
/// <param name="MonetaryAmountCurrency">The covenant monetary amount currency.</param>
/// <param name="Status">The covenant status.</param>
/// <param name="Footnotes">The optional covenant footnotes.</param>
public record CovenantResource(
    int Id,
    string DocumentId,
    string ClientId,
    string PeriodStartDate,
    string PeriodEndDate,
    decimal MonetaryAmountValue,
    string MonetaryAmountCurrency,
    string Status,
    string? Footnotes);
