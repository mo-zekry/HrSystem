using HrSystem.Application.Dtos.OrgUnits;
using HrSystem.Application.OrgUnits.Commands;
using HrSystem.Application.OrgUnits.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrgUnitsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("hierarchy")]
    public async Task<ActionResult<IReadOnlyList<OrgUnitNodeDto>>> GetHierarchy(
        [FromQuery] Guid? rootId,
        CancellationToken ct
    )
    {
        var result = await _mediator.Send(new GetOrgUnitsHierarchyQuery(rootId), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create(
        [FromBody] CreateOrgUnitCommand command,
        CancellationToken ct
    )
    {
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetHierarchy), new { rootId = id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateOrgUnitCommand command,
        CancellationToken ct
    )
    {
        var cmd = command with { Id = id };
        await _mediator.Send(cmd, ct);
        return NoContent();
    }
}
