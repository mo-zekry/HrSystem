using System.Reflection;
using FluentValidation;
using HrSystem.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HrSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registers MediatR handlers, requests, and behaviors from this assembly
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
        );

        // Registers all FluentValidation validators in this assembly
        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly(),
            includeInternalTypes: true
        );

        // Add validation pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
