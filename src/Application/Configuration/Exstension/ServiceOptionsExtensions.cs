using Defender.Common.Exstension;
using Defender.GeneralTestingService.Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.GeneralTestingService.Application.Configuration.Exstension;

public static class ServiceOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PortalApiOptions>(configuration.GetSection(nameof(PortalApiOptions)));

        return services;
    }
}