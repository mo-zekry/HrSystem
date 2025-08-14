namespace HrSystem.Application.Features.Employees.Queries.Response;

public sealed record PagedResultDto<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int Page,
    int PageSize
);