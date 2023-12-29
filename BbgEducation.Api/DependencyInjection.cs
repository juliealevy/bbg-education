﻿using BbgEducation.Api.Errors;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BbgEducation.Api;

public static class DependencyInjection
{

    public static IServiceCollection AddPresentation(this IServiceCollection services) {
 
        services.AddControllers();
        services.AddMappings();
        
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
