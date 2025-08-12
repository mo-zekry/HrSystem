using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.OrgTypes;
using HrSystem.Application.OrgTypes.Commands;
using HrSystem.Application.OrgTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrgTypesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrgTypeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetOrgTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<OrgTypeDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? searchTerm = null,
        CancellationToken ct = default
    )
    {
        var result = await _mediator.Send(
            new GetOrgTypesPagedQuery(page, pageSize, searchTerm),
            ct
        );
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create(
        [FromBody] CreateOrgTypeCommand command,
        CancellationToken ct
    )
    {
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateOrgTypeCommand command,
        CancellationToken ct
    )
    {
        var cmd = command with { Id = id };
        await _mediator.Send(cmd, ct);
        return NoContent();
    }
}
