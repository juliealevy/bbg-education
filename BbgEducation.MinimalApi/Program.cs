using BbgEducation.Api.Minimal;
using BbgEducation.Api.Common;
using BbgEducation.Infrastructure;
using BbgEducation.Application;
using Serilog;

namespace BbgEducation.MinimalApi;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services
                .AddHttpContextAccessor()
                .AddEndpointsApiExplorer()
                .AddPresentation()
                .AddCommonServices()
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

            var host = builder.Host;
            host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
            );

            var app = builder.Build();
            {
               // app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();                
                app.MapControllers();
                app.UseSerilogRequestLogging();
                app.UseRouting();
                app.UsePathBase(new PathString("/api"));


                app.Run();
            }
        }       
    }
}
