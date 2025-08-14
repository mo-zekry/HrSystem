using MediatR;

namespace HrSystem.Application.Features.Employees.Commands.DeactivateEmployee;

public sealed record DeactivateEmployeeCommand(int Id) : IRequest;