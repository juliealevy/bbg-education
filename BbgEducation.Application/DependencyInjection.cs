using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application;
public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
       // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
       // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;

    }

}
