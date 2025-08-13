
using Microsoft.Extensions.DependencyInjection;

namespace Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddHttpClient<IData, Data>();
        return services;
    }
}
