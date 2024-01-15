using BbgEducation.Api.Minimal;
using BbgEducation.Api.Common;
using BbgEducation.Infrastructure;
using BbgEducation.Application;
using Serilog;
using BbgEducation.Api.Minimal.ApiRoot;

namespace BbgEducation.MinimalApi;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();
        app.AddApiRootEndPoints();
        app.UseHttpsRedirection();
        app.Run();

        //var builder = WebApplication.CreateBuilder(args);
        //{
        //    builder.Services
        //        .AddHttpContextAccessor()
        //        .AddEndpointsApiExplorer();              
        //        //.AddAuthorization()               
        //        //.AddPresentation()
        //        //.AddCommonServices()
        //        //.AddApplication()
        //        //.AddInfrastructure(builder.Configuration);

        //    var host = builder.Host;
        //    //host.UseSerilog((context, configuration) =>
        //    //    configuration.ReadFrom.Configuration(context.Configuration)
        //    //);

        //    var app = builder.Build();
        //    {
        //        // app.UseExceptionHandler("/error");                              
        //     //   app.UseAuthentication();
        //     //   app.UseAuthorization();                              
        //        app.UseRouting();
        //        app.AddApiRootEndPoints();
        //        app.UseHttpsRedirection();
        //       // app.UseSerilogRequestLogging();
        //        //  app.UsePathBase(new PathString("/api"));


        //        app.Run();
        //    }
        //}       
    }
}
