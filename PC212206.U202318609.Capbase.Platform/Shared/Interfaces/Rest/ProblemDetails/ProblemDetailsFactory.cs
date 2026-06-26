using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PC212206.U202318609.Capbase.Platform.Shared.Resources;
using PC212206.U202318609.Capbase.Platform.Shared.Resources.Errors;

namespace PC212206.U202318609.Capbase.Platform.Shared.Interfaces.Rest.ProblemDetails;

/// <summary>
///     Creates consistent API problem details responses.
/// </summary>
public class ProblemDetailsFactory
{
    private readonly Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory _aspNetCoreProblemDetailsFactory;
    private readonly IStringLocalizer<CommonMessages> _commonLocalizer;
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer;

    public ProblemDetailsFactory(
        IStringLocalizer<ErrorMessages> errorLocalizer,
        IStringLocalizer<CommonMessages> commonLocalizer,
        Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory aspNetCoreProblemDetailsFactory)
    {
        _errorLocalizer = errorLocalizer;
        _commonLocalizer = commonLocalizer;
        _aspNetCoreProblemDetailsFactory = aspNetCoreProblemDetailsFactory;
    }

    /// <summary>
    ///     Creates a problem details response using a domain error enum as the response title.
    /// </summary>
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        Enum? errorEnum,
        string detailMessage)
    {
        var problemDetails = _aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            ResolveTitle(errorEnum),
            detail: detailMessage
        );

        problemDetails.Title = ResolveTitle(errorEnum);
        problemDetails.Detail = detailMessage;
        problemDetails.Instance = controller.HttpContext.Request.Path;

        return controller.StatusCode(statusCode, problemDetails);
    }

    /// <summary>
    ///     Creates a problem details response using explicit localization keys.
    /// </summary>
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        string titleKey,
        string detailKey,
        params object[] detailArgs)
    {
        var problemDetails = _aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            _commonLocalizer[titleKey],
            detail: _errorLocalizer[detailKey, detailArgs],
            instance: controller.HttpContext.Request.Path
        );
        return controller.StatusCode(statusCode, problemDetails);
    }

    private string ResolveTitle(Enum? errorEnum)
    {
        if (errorEnum is null)
            return _commonLocalizer["InternalServerError"];

        var localizedTitle = _errorLocalizer[errorEnum.ToString()];
        return localizedTitle.ResourceNotFound
            ? errorEnum.ToString().Humanize(LetterCasing.Title)
            : localizedTitle.Value;
    }
}
