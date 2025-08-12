using HrSystem.Application.Repositories;
using HrSystem.Infrastructure.Persistence;
using HrSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HrSystem.Infrastructure;

public static class DependencyInjection
{
    // Allows the host to supply its preferred EF Core provider configuration.
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> configureDb
    )
    {
        services.AddDbContext<HrSystemDbContext>(configureDb);

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        return services;
    }
}
