using HrSystem.Application.Repositories;
using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Features.Employees.Commands.UpdateEmployee;

public sealed record DeactivateEmployeeCommand(int Id) : IRequest;