using HrSystem.Application.Dtos.LeaveRequests;
using HrSystem.Application.LeaveRequests.Commands;
using HrSystem.Application.LeaveRequests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LeaveRequestsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("manager/{managerId:guid}")]
    public async Task<ActionResult<IReadOnlyList<LeaveRequestDto>>> ForManager(
        Guid managerId,
        CancellationToken ct
    )
    {
        var result = await _mediator.Send(new GetLeaveRequestsForManagerQuery(managerId), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create(
        [FromBody] CreateLeaveRequestCommand command,
        CancellationToken ct
    )
    {
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(ForManager), new { managerId = command.EmployeeId }, new { id });
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<IActionResult> Approve(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new ApproveLeaveRequestCommand(id), ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new RejectLeaveRequestCommand(id), ct);
        return NoContent();
    }
}
