using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Interfaces.Services;
using BbgEducation.Infrastructure.Persistance.Connections;
using BbgEducation.Infrastructure.Persistance.Repositories;
using BbgEducation.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
        ConfigurationManager configuration) {

        services
            //.AddAuth(configuration)
            .AddPersistance();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services) {

        services.AddSingleton<ISQLConnectionFactory, SQLConnectionFactory>();   
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IBbgProgramRepository, BbgProgramRepository>();
        //services.AddScoped<IUserRepository, UserRepository>();
        
        return services;

    }

    //public static IServiceCollection AddAuth(
    //    this IServiceCollection services,
    //    ConfigurationManager configuration) {

    //    var jwtSettings = new JwtSettings();
    //    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    //    //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));  //adds IOption interface
    //    services.AddSingleton(Options.Create(jwtSettings));
    //    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    //    services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    //        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters {
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidateLifetime = true,
    //            ValidateIssuerSigningKey = true,
    //            ValidIssuer = jwtSettings.Issuer,
    //            ValidAudience = jwtSettings.Audience,
    //            IssuerSigningKey = new SymmetricSecurityKey(
    //                Encoding.UTF8.GetBytes(jwtSettings.Secret))
    //        });

    //    return services;
    //}
}
