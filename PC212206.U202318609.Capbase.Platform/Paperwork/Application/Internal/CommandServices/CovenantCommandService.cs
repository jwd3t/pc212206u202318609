using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PC212206.U202318609.Capbase.Platform.Paperwork.Application.CommandServices;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Commands;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.ValueObjects;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Repositories;
using PC212206.U202318609.Capbase.Platform.Paperwork.Resources;
using PC212206.U202318609.Capbase.Platform.Shared.Application.Model;
using PC212206.U202318609.Capbase.Platform.Shared.Domain.Repositories;
using PC212206.U202318609.Capbase.Platform.Shared.Domain.Model.ValueObjects;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Application.Internal.CommandServices;

/// <summary>
///     Handles covenant write operations.
/// </summary>
public class CovenantCommandService(
    ICovenantRepository covenantRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<PaperworkMessages> localizer) : ICovenantCommandService
{
    /// <inheritdoc />
    public async Task<Result<Covenant>> Handle(CreateCovenantCommand command, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(command.DocumentId, out var documentIdentifier))
            return Result<Covenant>.Failure(
                PaperworkError.InvalidDocumentIdentifier,
                localizer[nameof(PaperworkError.InvalidDocumentIdentifier)]);

        if (!Guid.TryParse(command.ClientId, out var clientId))
            return Result<Covenant>.Failure(
                PaperworkError.InvalidClientId,
                localizer[nameof(PaperworkError.InvalidClientId)]);

        if (command.PeriodEndDate <= command.PeriodStartDate)
            return Result<Covenant>.Failure(
                PaperworkError.InvalidLegalityPeriod,
                localizer[nameof(PaperworkError.InvalidLegalityPeriod)]);

        if (command.MonetaryAmountValue < decimal.Zero)
            return Result<Covenant>.Failure(
                PaperworkError.InvalidMonetaryAmountValue,
                localizer[nameof(PaperworkError.InvalidMonetaryAmountValue)]);

        if (string.IsNullOrWhiteSpace(command.MonetaryAmountCurrency))
            return Result<Covenant>.Failure(
                PaperworkError.InvalidMonetaryAmountCurrency,
                localizer[nameof(PaperworkError.InvalidMonetaryAmountCurrency)]);

        var status = CovenantStatus.Draft;
        if (!string.IsNullOrWhiteSpace(command.Status) &&
            !Enum.TryParse(command.Status, true, out status))
            return Result<Covenant>.Failure(
                PaperworkError.InvalidCovenantStatus,
                localizer[nameof(PaperworkError.InvalidCovenantStatus)]);

        if (await covenantRepository.ExistsByDocumentIdentifierAsync(documentIdentifier, cancellationToken))
            return Result<Covenant>.Failure(
                PaperworkError.DuplicateDocumentIdentifier,
                localizer[nameof(PaperworkError.DuplicateDocumentIdentifier)]);

        var covenant = new Covenant(
            new CapbaseIdentifier(documentIdentifier),
            new PartyId(clientId),
            new LegalityPeriod(command.PeriodStartDate, command.PeriodEndDate),
            new MonetaryAmount(command.MonetaryAmountValue, command.MonetaryAmountCurrency),
            status,
            command.Footnotes);

        try
        {
            await covenantRepository.AddAsync(covenant, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Covenant>.Success(covenant);
        }
        catch (OperationCanceledException)
        {
            return Result<Covenant>.Failure(
                PaperworkError.OperationCancelled,
                localizer[nameof(PaperworkError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Covenant>.Failure(
                PaperworkError.DatabaseError,
                localizer[nameof(PaperworkError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Covenant>.Failure(
                PaperworkError.InternalServerError,
                localizer[nameof(PaperworkError.InternalServerError)]);
        }
    }
}
