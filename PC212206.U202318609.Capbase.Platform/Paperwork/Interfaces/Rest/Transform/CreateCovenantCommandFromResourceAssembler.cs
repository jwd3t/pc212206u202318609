using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Commands;
using PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Resources;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Transform;

/// <summary>
///     Converts API resources into application commands.
/// </summary>
public static class CreateCovenantCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds a create covenant command from an API resource.
    /// </summary>
    /// <param name="resource">The request resource.</param>
    /// <returns>The generated command.</returns>
    public static CreateCovenantCommand ToCommandFromResource(CreateCovenantResource resource)
    {
        return new CreateCovenantCommand(
            resource.DocumentId,
            resource.ClientId,
            resource.PeriodStartDate,
            resource.PeriodEndDate,
            resource.MonetaryAmountValue,
            resource.MonetaryAmountCurrency,
            resource.Status,
            resource.Footnotes);
    }
}
