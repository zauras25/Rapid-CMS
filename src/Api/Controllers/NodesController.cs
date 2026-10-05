using Microsoft.AspNetCore.Mvc;
using RapidCMS.Contracts.Nodes;

namespace RapidCMS.Api.Controllers;

[ApiController]
[Route("api/documents/{documentId:guid}/pages/{pageId:guid}/nodes")]
public sealed class NodesController : ControllerBase
{
    /*
     * Page Builder node endpoints.
     *
     * Application handlers should be injected here once the
     * corresponding application commands/queries exist.
     *
     * The controller intentionally does not contain domain logic.
     */

    [HttpGet]
    public IActionResult GetTree(
        Guid documentId,
        Guid pageId)
    {
        return Ok(new
        {
            documentId,
            pageId,
            nodes = Array.Empty<NodeDto>()
        });
    }

    [HttpGet("{nodeId:guid}")]
    public IActionResult GetNode(
        Guid documentId,
        Guid pageId,
        Guid nodeId)
    {
        return Ok(new
        {
            documentId,
            pageId,
            nodeId
        });
    }

    [HttpPost]
    public IActionResult CreateNode(
        Guid documentId,
        Guid pageId,
        [FromBody] CreateNodeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Node name cannot be empty.");

        return StatusCode(
            StatusCodes.Status501NotImplemented,
            new
            {
                message =
                    "Node creation handler is not wired yet."
            });
    }

    [HttpPut("{nodeId:guid}")]
    public IActionResult UpdateNode(
        Guid documentId,
        Guid pageId,
        Guid nodeId,
        [FromBody] UpdateNodeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Node name cannot be empty.");

        return StatusCode(
            StatusCodes.Status501NotImplemented,
            new
            {
                message =
                    "Node update handler is not wired yet."
            });
    }

    [HttpPost("{nodeId:guid}/move")]
    public IActionResult MoveNode(
        Guid documentId,
        Guid pageId,
        Guid nodeId,
        [FromBody] MoveNodeRequest request)
    {
        return StatusCode(
            StatusCodes.Status501NotImplemented,
            new
            {
                message =
                    "Node move handler is not wired yet."
            });
    }

    [HttpDelete("{nodeId:guid}")]
    public IActionResult DeleteNode(
        Guid documentId,
        Guid pageId,
        Guid nodeId)
    {
        return StatusCode(
            StatusCodes.Status501NotImplemented,
            new
            {
                message =
                    "Node deletion handler is not wired yet."
            });
    }
}
