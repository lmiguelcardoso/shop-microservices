
using BuildingBlocks.Behaviors;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container

builder.Services.AddCarter(null, config =>
{
    var modules = typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();
    config.WithModules(modules);
});

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.RegisterServicesFromAssembly(assembly);
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
app.Run();
