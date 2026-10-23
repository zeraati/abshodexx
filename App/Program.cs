global using Common;
global using FastEndpoints;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

using App;
using App.Feature;
using Bale;
using Common.HttpClient;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMemoryCache()
    .AddFastEndpoints()
    .RegisterServices()
    .AddSwaggerConfig(true)
    .AddHttpContextAccessor()
    .AddDbConfig<AppDbContext>(AppConfig.Connection)
    .AddAuthConfig(AppConfig.Issuer, AppConfig.Audience)
    .AddBaleConfig()
    .AddHttpClient<HttpClientService>();

builder.Services.AddHostedService<PollingService>();
builder.Services.AddHostedService<GoldPriceFetchService>();

var app = builder.Build();
app.UseAuthentication().UseAuthorization()
   .UseDefaultExceptionHandler(useGenericReason: app.Environment.IsDevelopment()==false)
   .UseFastEndpoints(x => { x.Endpoints.RoutePrefix = "api"; })
   .UseSwaggerGen();
app.Run();
