using Microsoft.Extensions.DependencyInjection;

namespace TrippinApi.Services;

public static class PeopleServiceConfigurationExtensions
{
    public static IServiceCollection ConfigurePeopleServices(this IServiceCollection services)
    {
        services.AddSingleton<IPeopleService, PeopleService>();
        return services;
    }
}
