using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
//Application Service
builder.Services.AddCarter(null, config =>
{
    var modules = typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();
    config.WithModules(modules);
});

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.RegisterServicesFromAssembly(assembly);
});

//Data Services
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
    config.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Basket";
});

//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options => 
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

//Cross-Cutting Services
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
