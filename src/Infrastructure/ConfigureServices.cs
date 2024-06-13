using System.Reflection;
using Defender.Common.Clients.Portal;
using Defender.GeneralTestingService.Application.Clients.Portal;
using Defender.GeneralTestingService.Application.Common.Interfaces.Repositories;
using Defender.GeneralTestingService.Application.Configuration.Options;
using Defender.GeneralTestingService.Infrastructure.Clients.Portal;
using Defender.GeneralTestingService.Infrastructure.Repositories.DomainModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.GeneralTestingService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services
            .RegisterRepositories()
            .RegisterApiClients(configuration)
            .RegisterClientWrappers();

        return services;
    }

    private static IServiceCollection RegisterClientWrappers(this IServiceCollection services)
    {
        services.AddTransient<IPortalWrapper, PortalWrapper>();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IDomainModelRepository, DomainModelRepository>();

        return services;
    }

    private static IServiceCollection RegisterApiClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.RegisterPortalClient(
            (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<PortalApiOptions>>().Value.Url);
            });

        return services;
    }

}
