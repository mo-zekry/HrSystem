using HrSystem.Domain.Enums;

namespace HrSystem.Application.Dtos.LeaveRequests;

public sealed record LeaveRequestDto(
    Guid Id,
    Guid EmployeeId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Reason,
    LeaveRequestStatus Status,
    DateTime CreatedDate,
    DateTime? UpdatedDate
);
