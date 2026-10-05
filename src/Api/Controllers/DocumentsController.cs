using Microsoft.AspNetCore.Mvc;
using RapidCMS.Application.Documents;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Contracts.Queries;

namespace RapidCMS.Api.Controllers;

[ApiController]
[Route("api/documents")]
public sealed class DocumentsController : ControllerBase
{
    private readonly CreateDocumentHandler _createHandler;
    private readonly DeleteDocumentHandler _deleteHandler;
    private readonly GetDocumentByIdHandler _getByIdHandler;
    private readonly GetDocumentsHandler _getAllHandler;

    private readonly AddPageToDocumentHandler _addPageHandler;
    private readonly RemovePageFromDocumentHandler _removePageHandler;

    private readonly AddAssetToDocumentHandler _addAssetHandler;
    private readonly RemoveAssetFromDocumentHandler _removeAssetHandler;

    private readonly AddVariableToDocumentHandler _addVariableHandler;
    private readonly RemoveVariableFromDocumentHandler _removeVariableHandler;

    private readonly AddPrototypeToDocumentHandler _addPrototypeHandler;
    private readonly RemovePrototypeFromDocumentHandler _removePrototypeHandler;

    private readonly AddComponentToDocumentHandler _addComponentHandler;
    private readonly RemoveComponentFromDocumentHandler _removeComponentHandler;

    public DocumentsController(
        CreateDocumentHandler createHandler,
        DeleteDocumentHandler deleteHandler,
        GetDocumentByIdHandler getByIdHandler,
        GetDocumentsHandler getAllHandler,
        AddPageToDocumentHandler addPageHandler,
        RemovePageFromDocumentHandler removePageHandler,
        AddAssetToDocumentHandler addAssetHandler,
        RemoveAssetFromDocumentHandler removeAssetHandler,
        AddVariableToDocumentHandler addVariableHandler,
        RemoveVariableFromDocumentHandler removeVariableHandler,
        AddPrototypeToDocumentHandler addPrototypeHandler,
        RemovePrototypeFromDocumentHandler removePrototypeHandler,
        AddComponentToDocumentHandler addComponentHandler,
        RemoveComponentFromDocumentHandler removeComponentHandler)
    {
        _createHandler = createHandler;
        _deleteHandler = deleteHandler;
        _getByIdHandler = getByIdHandler;
        _getAllHandler = getAllHandler;

        _addPageHandler = addPageHandler;
        _removePageHandler = removePageHandler;

        _addAssetHandler = addAssetHandler;
        _removeAssetHandler = removeAssetHandler;

        _addVariableHandler = addVariableHandler;
        _removeVariableHandler = removeVariableHandler;

        _addPrototypeHandler = addPrototypeHandler;
        _removePrototypeHandler = removePrototypeHandler;

        _addComponentHandler = addComponentHandler;
        _removeComponentHandler = removeComponentHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _getAllHandler.HandleAsync(
            new GetDocumentsQuery(),
            cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(result.Data);
    }

    [HttpGet("{documentId:guid}")]
    public async Task<IActionResult> GetById(
        Guid documentId,
        CancellationToken cancellationToken)
    {
        var result = await _getByIdHandler.HandleAsync(
            new GetDocumentByIdQuery(documentId),
            cancellationToken);

        if (!result.Succeeded)
        {
            if (result.Errors.Any(x =>
                    x.Contains("was not found", StringComparison.OrdinalIgnoreCase)))
            {
                return NotFound(result.Errors);
            }

            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDocumentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _createHandler.HandleAsync(
            command,
            cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return CreatedAtAction(
            nameof(GetById),
            new { documentId = command.DocumentId },
            result.Data);
    }

    [HttpDelete("{documentId:guid}")]
    public async Task<IActionResult> Delete(
        Guid documentId,
        CancellationToken cancellationToken)
    {
        var result = await _deleteHandler.HandleAsync(
            new DeleteDocumentCommand(documentId),
            cancellationToken);

        if (!result.Succeeded)
        {
            if (result.Errors.Any(x =>
                    x.Contains("was not found", StringComparison.OrdinalIgnoreCase)))
            {
                return NotFound(result.Errors);
            }

            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("{documentId:guid}/pages/{pageId:guid}")]
    public async Task<IActionResult> AddPage(
        Guid documentId,
        Guid pageId,
        CancellationToken cancellationToken)
    {
        var result = await _addPageHandler.HandleAsync(
            new AddPageToDocumentCommand(documentId, pageId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpDelete("{documentId:guid}/pages/{pageId:guid}")]
    public async Task<IActionResult> RemovePage(
        Guid documentId,
        Guid pageId,
        CancellationToken cancellationToken)
    {
        var result = await _removePageHandler.HandleAsync(
            new RemovePageFromDocumentCommand(documentId, pageId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpPost("{documentId:guid}/assets/{assetId:guid}")]
    public async Task<IActionResult> AddAsset(
        Guid documentId,
        Guid assetId,
        CancellationToken cancellationToken)
    {
        var result = await _addAssetHandler.HandleAsync(
            new AddAssetToDocumentCommand(documentId, assetId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpDelete("{documentId:guid}/assets/{assetId:guid}")]
    public async Task<IActionResult> RemoveAsset(
        Guid documentId,
        Guid assetId,
        CancellationToken cancellationToken)
    {
        var result = await _removeAssetHandler.HandleAsync(
            new RemoveAssetFromDocumentCommand(documentId, assetId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpPost("{documentId:guid}/variables/{variableId:guid}")]
    public async Task<IActionResult> AddVariable(
        Guid documentId,
        Guid variableId,
        CancellationToken cancellationToken)
    {
        var result = await _addVariableHandler.HandleAsync(
            new AddVariableToDocumentCommand(documentId, variableId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpDelete("{documentId:guid}/variables/{variableId:guid}")]
    public async Task<IActionResult> RemoveVariable(
        Guid documentId,
        Guid variableId,
        CancellationToken cancellationToken)
    {
        var result = await _removeVariableHandler.HandleAsync(
            new RemoveVariableFromDocumentCommand(documentId, variableId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpPost("{documentId:guid}/prototypes/{prototypeId:guid}")]
    public async Task<IActionResult> AddPrototype(
        Guid documentId,
        Guid prototypeId,
        CancellationToken cancellationToken)
    {
        var result = await _addPrototypeHandler.HandleAsync(
            new AddPrototypeToDocumentCommand(documentId, prototypeId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpDelete("{documentId:guid}/prototypes/{prototypeId:guid}")]
    public async Task<IActionResult> RemovePrototype(
        Guid documentId,
        Guid prototypeId,
        CancellationToken cancellationToken)
    {
        var result = await _removePrototypeHandler.HandleAsync(
            new RemovePrototypeFromDocumentCommand(documentId, prototypeId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpPost("{documentId:guid}/components/{componentId:guid}")]
    public async Task<IActionResult> AddComponent(
        Guid documentId,
        Guid componentId,
        CancellationToken cancellationToken)
    {
        var result = await _addComponentHandler.HandleAsync(
            new AddComponentToDocumentCommand(documentId, componentId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpDelete("{documentId:guid}/components/{componentId:guid}")]
    public async Task<IActionResult> RemoveComponent(
        Guid documentId,
        Guid componentId,
        CancellationToken cancellationToken)
    {
        var result = await _removeComponentHandler.HandleAsync(
            new RemoveComponentFromDocumentCommand(documentId, componentId),
            cancellationToken);

        return ToActionResult(result);
    }

    private IActionResult ToActionResult(CommandResult result)
    {
        if (result.Succeeded)
            return NoContent();

        if (result.Errors.Any(x =>
                x.Contains("was not found", StringComparison.OrdinalIgnoreCase)))
        {
            return NotFound(result.Errors);
        }

        return BadRequest(result.Errors);
    }
}
