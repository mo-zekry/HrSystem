using HrSystem.Application.Dtos.OrgUnits;
using HrSystem.Application.OrgUnits.Commands;
using HrSystem.Application.OrgUnits.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public sealed class OrgUnitsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrgUnitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("to-root/{orgUnitId:int}")]
    public async Task<ActionResult<IReadOnlyList<OrgUnitDto>>> GetToRoot(
        int orgUnitId,
        CancellationToken ct
    )
    {
        var result = await _mediator.Send(new GetOrgUnitToRootQuery(orgUnitId), ct);
        return Ok(result);
    }

    [HttpGet("hierarchy")]
    public async Task<ActionResult<IReadOnlyList<OrgUnitNodeDto>>> GetHierarchy(
        [FromQuery] int? rootId,
        CancellationToken ct
    )
    {
        var result = await _mediator.Send(new GetOrgUnitsHierarchyQuery(rootId), ct);
        // In future, enrich result with OrgTypeName, manager details, employee count, etc.
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateOrgUnitCommand command,
        CancellationToken ct
    )
    {
        var cmd = command with { Id = id };
        await _mediator.Send(cmd, ct);
        return NoContent();
    }
}
