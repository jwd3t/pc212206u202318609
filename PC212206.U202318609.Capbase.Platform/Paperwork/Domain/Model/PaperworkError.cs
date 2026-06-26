namespace PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model;

/// <summary>
///     Represents the application errors that may occur in the Paperwork bounded context.
/// </summary>
public enum PaperworkError
{
    InvalidDocumentIdentifier,
    InvalidClientId,
    InvalidLegalityPeriod,
    InvalidMonetaryAmountValue,
    InvalidMonetaryAmountCurrency,
    InvalidCovenantStatus,
    DuplicateDocumentIdentifier,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
