using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MA.Clean.Template.Infrastructure.Persistence;
using MA.Clean.Template.Application.Common.Abstractions;

namespace MA.Clean.Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString = null)
    {
        // Default to InMemory so the template runs out-of-the-box.
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("AppDb"));

        // Map DbContext -> AppDbContext so open-generic repos that depend on DbContext resolve correctly.
        services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());

        // Open-generic repositories
        services.AddScoped(typeof(IRepository<,>), typeof(Repositories.RepositoryBase<,>));
        services.AddScoped(typeof(IReadRepository<,>), typeof(Repositories.RepositoryBase<,>));

        return services;
    }
}
