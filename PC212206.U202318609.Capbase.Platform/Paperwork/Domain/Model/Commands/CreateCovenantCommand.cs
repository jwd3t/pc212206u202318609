namespace PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Commands;

/// <summary>
///     Represents the data required to create a covenant.
/// </summary>
/// <param name="DocumentId">The external covenant document identifier.</param>
/// <param name="ClientId">The external client identifier.</param>
/// <param name="PeriodStartDate">The covenant period start date.</param>
/// <param name="PeriodEndDate">The covenant period end date.</param>
/// <param name="MonetaryAmountValue">The covenant total monetary value.</param>
/// <param name="MonetaryAmountCurrency">The covenant currency.</param>
/// <param name="Status">The optional covenant status string.</param>
/// <param name="Footnotes">Optional covenant footnotes.</param>
public record CreateCovenantCommand(
    string DocumentId,
    string ClientId,
    DateOnly PeriodStartDate,
    DateOnly PeriodEndDate,
    decimal MonetaryAmountValue,
    string MonetaryAmountCurrency,
    string? Status,
    string? Footnotes);
