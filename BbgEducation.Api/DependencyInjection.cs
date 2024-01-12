﻿using BbgEducation.Api.Common.Routes;
using BbgEducation.Api.Common.Routes.CustomAttributes;
using BbgEducation.Api.Hal.Links;
using BbgEducation.Api.Hal.Resources;
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
        services.AddScoped<IRepresentationFactory, RepresentationFactory>();
        services.AddControllersWithViews(opts =>
        {
            opts.Conventions.Add(new RoutePrefixConvention());
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
