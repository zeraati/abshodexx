global using Common;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

using App;
using Bale;
using App.Feature;
using Common.HttpClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .RegisterServices(typeof(BaleService).Assembly)
    .AddWorkflowServices()
    .AddDbConfig<AppDbContext>(AppConfig.Connection)
    .AddHttpClient<HttpClientService>();

var app = builder.Build();
app.Run();
