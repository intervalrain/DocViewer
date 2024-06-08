using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Infrastructure.Common.Persistence;

using Microsoft.Extensions.DependencyInjection;

namespace DocViewer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPersistence();
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IBoardRepository, BoardRepository>();
        return services;
    }
}

