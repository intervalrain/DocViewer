using DocViewer.Application.Common.Interfaces;
using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Infrastructure.Common.Persistence;
using DocViewer.Infrastructure.Security;

using Microsoft.Extensions.DependencyInjection;

namespace DocViewer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPersistence();
        services.AddAuthorization();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IBoardRepository, BoardRepository>();
        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();
        return services;
    }
}

