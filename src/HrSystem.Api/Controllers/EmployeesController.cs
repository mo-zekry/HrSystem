using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Employees;
using HrSystem.Application.Employees.Commands;
using HrSystem.Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class EmployeesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<EmployeeDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? orgUnitId = null,
        [FromQuery] string? searchTerm = null,
        CancellationToken ct = default
    )
    {
        var result = await _mediator.Send(
            new GetEmployeesPagedQuery(page, pageSize, orgUnitId, searchTerm),
            ct
        );
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create(
        [FromBody] CreateEmployeeCommand command,
        CancellationToken ct
    )
    {
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateEmployeeCommand command,
        CancellationToken ct
    )
    {
        // Ensure route id is used
        var cmd = command with
        {
            Id = id,
        };
        await _mediator.Send(cmd, ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeactivateEmployeeCommand(id), ct);
        return NoContent();
    }
}
