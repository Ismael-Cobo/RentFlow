using Rentflow.Api.Extensions;
using Rentflow.Api.Middleware;
using RentFlow.Common.Application;
using Rentflow.Common.Infrastructure;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Modules.Fleet.Infrastructure;
using RentFlow.Modules.Identity.Infrastructure;
using RentFlow.Modules.Rentals.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

builder.Services.AddApplication([
    RentFlow.Modules.Fleet.Application.AssemblyReference.Assembly,
    RentFlow.Modules.Identity.Application.AssemblyReference.Assembly,
    RentFlow.Modules.Rentals.Application.AssemblyReference.Assembly,
]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddInfrastructure(
    [],
    databaseConnectionString);

builder.Services.AddInfrastructure(
    [RentalsModule.ConfigureConsumers],
    databaseConnectionString);

builder.Configuration.AddModuleConfiguration(["Fleet", "Identity", "Rentals"]);

builder.Services.AddVehiclesModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddRentalsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapEndpoints();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();
