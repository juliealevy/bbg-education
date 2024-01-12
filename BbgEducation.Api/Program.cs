
using BbgEducation.Infrastructure;
using BbgEducation.Application;
using Serilog;
using BbgEducation.Api.Common;

namespace BbgEducation.Api;
public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services
                .AddHttpContextAccessor()
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
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();                
                app.MapControllers();
                app.UseSerilogRequestLogging();
                
                

                app.Run();
            }
        }
    }





}
