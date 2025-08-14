using HrSystem.Application.Dtos.LeaveRequests;
using HrSystem.Application.Features.LeaveRequests.Commands.ApproveLeaveRequest;
using HrSystem.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using HrSystem.Application.Features.LeaveRequests.Commands.RejectLeaveRequest;
using HrSystem.Application.Features.LeaveRequests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LeaveRequestsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("manager/{managerId:int}")]
    public async Task<ActionResult<IReadOnlyList<LeaveRequestDto>>> ForManager(
        int managerId,
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

    [HttpPost("{id:int}/approve")]
    public async Task<IActionResult> Approve(int id, CancellationToken ct)
    {
        await _mediator.Send(new ApproveLeaveRequestCommand(id), ct);
        return NoContent();
    }

    [HttpPost("{id:int}/reject")]
    public async Task<IActionResult> Reject(int id, CancellationToken ct)
    {
        await _mediator.Send(new RejectLeaveRequestCommand(id), ct);
        return NoContent();
    }
}
