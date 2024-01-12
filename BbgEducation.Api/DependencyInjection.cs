using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.JsonConverters;
using BbgEducation.Api.Common.Routes.CustomAttributes;
using BbgEducation.Api.Hal;

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
        
        services.AddControllersWithViews(opts =>
        {
            opts.Conventions.Add(new RoutePrefixConvention());
        });

        services.AddScoped<IBbgLinkGenerator, BbgLinkGenerator>();
        return services;
    } 

}
