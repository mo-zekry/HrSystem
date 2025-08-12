using HrSystem.Domain.Enums;

namespace HrSystem.Application.Dtos.LeaveRequests;

public sealed record LeaveRequestDto(
    int Id,
    int EmployeeId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Reason,
    LeaveRequestStatus Status,
    DateTime CreatedDate,
    DateTime? UpdatedDate
);
