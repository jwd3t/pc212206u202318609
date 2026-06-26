using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;
using PC212206.U202318609.Capbase.Platform.Shared.Application.Model;
using PC212206.U202318609.Capbase.Platform.Shared.Resources;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Transform;

/// <summary>
///     Creates consistent HTTP responses for Paperwork operations.
/// </summary>
public static class PaperworkActionResultAssembler
{
    /// <summary>
    ///     Builds an HTTP response from a create covenant result.
    /// </summary>
    /// <param name="controller">The controller generating the response.</param>
    /// <param name="result">The create covenant result.</param>
    /// <param name="commonLocalizer">The shared common message localizer.</param>
    /// <param name="successAction">The action to execute for successful results.</param>
    /// <returns>The HTTP action result.</returns>
    public static IActionResult ToActionResultFromCreateCovenantResult(
        ControllerBase controller,
        Result<Covenant> result,
        IStringLocalizer<CommonMessages> commonLocalizer,
        Func<Covenant, IActionResult> successAction)
    {
        if (result.IsSuccess)
            return successAction(result.Value!);

        var statusCode = ToStatusCode((PaperworkError)result.Error!);
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = commonLocalizer[ToTitleKey(statusCode)],
            Detail = result.Message,
            Instance = controller.HttpContext.Request.Path
        };

        return controller.StatusCode(statusCode, problemDetails);
    }

    private static int ToStatusCode(PaperworkError error)
    {
        return error switch
        {
            PaperworkError.InvalidDocumentIdentifier => StatusCodes.Status400BadRequest,
            PaperworkError.InvalidClientId => StatusCodes.Status400BadRequest,
            PaperworkError.InvalidLegalityPeriod => StatusCodes.Status400BadRequest,
            PaperworkError.InvalidMonetaryAmountValue => StatusCodes.Status400BadRequest,
            PaperworkError.InvalidMonetaryAmountCurrency => StatusCodes.Status400BadRequest,
            PaperworkError.InvalidCovenantStatus => StatusCodes.Status400BadRequest,
            PaperworkError.DuplicateDocumentIdentifier => StatusCodes.Status409Conflict,
            PaperworkError.OperationCancelled => StatusCodes.Status409Conflict,
            PaperworkError.DatabaseError => StatusCodes.Status500InternalServerError,
            PaperworkError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string ToTitleKey(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "BadRequest",
            StatusCodes.Status404NotFound => "NotFound",
            StatusCodes.Status409Conflict => "Conflict",
            _ => "InternalServerError"
        };
    }
}
