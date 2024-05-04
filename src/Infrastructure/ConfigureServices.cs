using System.Reflection;
using Defender.Common.Clients.Portal;
using Defender.GeneralTestingService.Application.Common.Interfaces;
using Defender.GeneralTestingService.Application.Common.Interfaces.Repositories;
using Defender.GeneralTestingService.Application.Configuration.Options;
using Defender.GeneralTestingService.Infrastructure.Clients.Portal;
using Defender.GeneralTestingService.Infrastructure.Repositories.DomainModels;
using Defender.GeneralTestingService.Infrastructure.Services;
using Defender.GeneralTestingService.Infrastructure.Steps;
using Defender.GeneralTestingService.Infrastructure.Steps.Sets;
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
            .RegisterServices()
            .RegisterRepositories()
            .RegisterApiClients(configuration)
            .RegisterClientWrappers()
            .RegisterSteps();

        return services;
    }

    private static IServiceCollection RegisterClientWrappers(this IServiceCollection services)
    {
        services.AddTransient<IPortalWrapper, PortalWrapper>();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITestStartingService, TestStartingService>();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IDomainModelRepository, DomainModelRepository>();

        return services;
    }

    private static IServiceCollection RegisterSteps(this IServiceCollection services)
    {
        services.AddSingleton<LoginStep>();
        services.AddSingleton<VerifyWalletStep>();
        services.AddSingleton<TransferMoneyStep>();

        services.AddSingleton<RegressionSet>();

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
