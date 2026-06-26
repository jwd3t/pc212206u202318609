using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PC212206.U202318609.Capbase.Platform.Paperwork.Application.CommandServices;
using PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Resources;
using PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest.Transform;
using PC212206.U202318609.Capbase.Platform.Shared.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Interfaces.Rest;

/// <summary>
///     Exposes covenant endpoints.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available covenant endpoints")]
public class CovenantsController(
    ICovenantCommandService covenantCommandService,
    IStringLocalizer<CommonMessages> commonLocalizer) : ControllerBase
{
    /// <summary>
    ///     Registers a new covenant.
    /// </summary>
    /// <param name="resource">The covenant creation request body.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created covenant resource or an error response.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a covenant",
        Description = "Registers a new covenant in the Capbase platform.",
        OperationId = "CreateCovenant")]
    [SwaggerResponse(StatusCodes.Status201Created, "The covenant was created successfully.", typeof(CovenantResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request contains invalid covenant data.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "A covenant with the same document identifier already exists.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")]
    public async Task<IActionResult> CreateCovenant(
        [FromBody] CreateCovenantResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateCovenantCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await covenantCommandService.Handle(command, cancellationToken);

        return PaperworkActionResultAssembler.ToActionResultFromCreateCovenantResult(
            this,
            result,
            commonLocalizer,
            createdCovenant => StatusCode(
                StatusCodes.Status201Created,
                CovenantResourceFromEntityAssembler.ToResourceFromEntity(createdCovenant)));
    }
}
