using BbgEducation.Api.Common.Routes;
using BbgEducation.Api.Hal;
using BbgEducation.Api.JsonConverters;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BbgEducation.Api;

public static class DependencyInjection
{

    public static IServiceCollection AddPresentation(this IServiceCollection services) {

        services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.DefaultIgnoreCondition = 
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });
        services.AddMappings();
        services.AddSingleton<IApiRouteService, ApiRouteService>();
        services.AddScoped<IBbgLinkGenerator, BbgLinkGenerator>();
        services.AddControllersWithViews(opts =>
        {
            opts.Conventions.Add(new RoutePrefixConvention());
        });
        services.AddResonseBuilders();

        return services;
    }

    private static IServiceCollection AddResonseBuilders(this IServiceCollection services) {
        Assembly.GetExecutingAssembly()
           .GetTypes()
           .Where(item => item.GetInterfaces()
           .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IBbgResponseBuilder<,>)) && !item.IsAbstract && !item.IsInterface)
           .ToList()
           .ForEach(assignedTypes =>
           {
               var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IBbgResponseBuilder<,>));
               services.AddScoped(serviceType, assignedTypes);
           });

        return services;

    }

    private static IServiceCollection AddMappings(this IServiceCollection services) {

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

}
