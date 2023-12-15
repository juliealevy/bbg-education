
using BbgEducation.Infrastructure;
using BbgEducation.Application;

namespace BbgEducation.Api;
public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        {
            // Add services to the container.            
            builder.Services      
                .AddPresentation()
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

            var app = builder.Build();
            {
               // app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                //app.UseAuthentication();
                //app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
        }
    }





}
