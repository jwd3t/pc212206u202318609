using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Commands;
using PC212206.U202318609.Capbase.Platform.Shared.Application.Model;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Application.CommandServices;

/// <summary>
///     Exposes covenant write use cases.
/// </summary>
public interface ICovenantCommandService
{
    /// <summary>
    ///     Creates a new covenant.
    /// </summary>
    /// <param name="command">The command data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The operation result containing the created covenant.</returns>
    Task<Result<Covenant>> Handle(CreateCovenantCommand command, CancellationToken cancellationToken);
}
