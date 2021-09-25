using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SetResponseHeaders;
using SetResponseHeaders.Controllers;
using SetResponseHeaders.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var sqlServerConnectionString = "Server=localhost;Database=Application;user id=sa;password=MyP@assword;";
builder.Services.AddDbContextPool<ApplicationContext>(options =>
{
    options.EnableThreadSafetyChecks(false);
    options.UseSqlServer(sqlServerConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(90));
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SetResponseHeaders", Version = "v1" });
});

builder.Services.AddScoped<IDataService, DataService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SetResponseHeaders v1"));
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
