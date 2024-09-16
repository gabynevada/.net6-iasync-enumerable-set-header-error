using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SetResponseHeaders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDataService, DataService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

// This works as expected
app.MapGet(
    "/minimal-api",
    async (
        [FromServices] IDataService dataService,
        HttpContext httpContext,
        CancellationToken cancellationToken
    ) =>
    {
        var results = await dataService.GetData();

        httpContext.Response.Headers.Append("Test", results.Name);
        return results.Data;
    }
);

app.Run();
