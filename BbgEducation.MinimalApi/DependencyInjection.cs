using BbgEducation.Api.Common.JsonConverters;
using BbgEducation.Api.Minimal.Hal;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.Minimal;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) {  
       // ConfigureJsonOptions(services);
        services.AddScoped<IMinimalApiLinkGenerator, MinimalApiLinkGenerator>();
        return services;
    }

    private static IServiceCollection ConfigureJsonOptions(this IServiceCollection services) {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        });

        return services;
    }
}
