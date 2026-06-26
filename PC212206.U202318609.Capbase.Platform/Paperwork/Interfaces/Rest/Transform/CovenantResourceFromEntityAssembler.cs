using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;
using PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Resources;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Transform;

/// <summary>
///     Converts covenant aggregates into REST resources.
/// </summary>
public static class CovenantResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds a covenant resource from a covenant aggregate.
    /// </summary>
    /// <param name="entity">The covenant aggregate.</param>
    /// <returns>The generated resource.</returns>
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
}
