namespace BbgEducation.Api.Minimal.ApiRoot;

public static class ApiRootResource
{ 
    
    public static WebApplication AddApiRootEndPoints(this WebApplication app) {
        app.MapGet("/api", () => GetAll)
            .WithName(nameof(GetAll)).AllowAnonymous();
        return app;
    }

    public async static Task<IResult> GetAll() {
        //  var representation = representationFactory.NewRepresentation(context)
        //.WithProperty("version", "0.0.1");
        await Task.CompletedTask;

        return Results.NoContent();
    }

}
