using Carter;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Ordering.API;
using Ordering.Application;
using Ordering.Application.Commands;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<CreateOrderHandler>();
    cfg.Lifetime = ServiceLifetime.Scoped;
});
builder.Services.AddCarter();
builder.Services.AddOpenApi();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCarter();
// await app.MigrateDatabase();

app.UseHttpsRedirection();



app.Run();
