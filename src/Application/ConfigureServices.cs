using System.Reflection;
using FluentValidation;
using Defender.Common.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Defender.GeneralTestingService.Application.Steps.Sets;
using Defender.GeneralTestingService.Application.Steps;
using Defender.GeneralTestingService.Application.Common.Interfaces;
using Defender.GeneralTestingService.Application.Services;

namespace Defender.GeneralTestingService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.RegisterServices()
            .RegisterSteps();

        return services;
    }


    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITestStartingService, TestStartingService>();

        return services;
    }

    private static IServiceCollection RegisterSteps(this IServiceCollection services)
    {
        services.AddSingleton<LoginStep>();
        services.AddSingleton<VerifyWalletStep>();
        services.AddSingleton<TransferMoneyStep>();
        services.AddSingleton<RechargeMoneyStep>();

        services.AddSingleton<RegressionSet>();

        return services;
    }
}
