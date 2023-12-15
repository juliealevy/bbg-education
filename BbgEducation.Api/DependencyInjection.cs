using BbgEducation.Api.Mapping;

namespace BbgEducation.Api;

public static class DependencyInjection
{

    public static IServiceCollection AddPresentation(this IServiceCollection services) {
 
        services.AddControllers();
        services.AddMappings();
        return services;

    }

}
