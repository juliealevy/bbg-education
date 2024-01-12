using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Api.Common.Routes;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BbgEducation.Api.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services) {

        services.AddSingleton<IApiRouteService, ApiRouteService>();
        services.AddScoped<IBbgLinkGenerator, BbgLinkGenerator>();
        services.AddScoped<IRepresentationFactory, RepresentationFactory>();
        services.AddMappings(); 
        return services;

    }

    //private static IServiceCollection ConfigureJsonOptions(this IServiceCollection services) {
    //    services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    //    {
    //        options.SerializerOptions.PropertyNameCaseInsensitive = false;
    //        options.SerializerOptions.PropertyNamingPolicy = null;
    //        options.SerializerOptions.WriteIndented = true;
    //    });

    //    return services;
    //}
    private static IServiceCollection AddMappings(this IServiceCollection services) {

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
