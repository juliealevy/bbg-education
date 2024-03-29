﻿using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Api.Common.Routes;
using BbgEducation.Api.Common.Versioning;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BbgEducation.Api.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services) {

        services.AddSingleton<IApiRouteService, ApiRouteService>();       
        services.AddSingleton<IRepresentationFactory, RepresentationFactory>();
        services.AddMappings(); 
        services.AddSingleton<IVersionProvider, VersionProvider>();
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
