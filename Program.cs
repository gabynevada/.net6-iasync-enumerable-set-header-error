using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SetResponseHeaders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDataService, DataService>();


var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
