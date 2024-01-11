using BbgEducation.Application.Common.Logger;
using BbgEducation.Application.Common.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BbgEducation.Application;
public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(),
            includeInternalTypes: true);         

        return services;

    }

}
