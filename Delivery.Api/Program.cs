using Delivery.Api.Middleware;
using Delivery.Application.Behaviors;
using Delivery.Application.Commands.CourierCommands.Commands;
using Delivery.Application.Commands.CourierCommands.Handlers;
using Delivery.Application.Commands.CourierCommands.Validator;
using Delivery.Application.Commands.DeliveryCommands.Command;
using Delivery.Application.Commands.DeliveryCommands.Handler;
using Delivery.Application.Services;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Domain.Interfaces.Services;
using Delivery.Infrastructure.Mongo;
using Delivery.Infrastructure.Mongo.Config;
using Delivery.Infrastructure.Mongo.UoW;
using Delivery.Infrastructure.Repository;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var aspConn = builder.Configuration.GetConnectionString("DeliveryDb") ?? 
    builder.Configuration.GetConnectionString("mongodb");

builder.AddServiceDefaults();

builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));
if (!string.IsNullOrEmpty(aspConn))
{
    builder.Services.Configure<MongoDbSettings>(options =>
    {
        options.ConnectionString = aspConn;
        options.DatabaseName = "DeliveryDb";
        options.MaxConnectionPoolSize = 100;
        options.MinConnectionPoolSize = 5;
        options.ConnectTimeoutSeconds = 10;
        options.SocketTimeoutSeconds = 10;
    });
}




// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IUnitOfWork>(provider =>
{
    var context = provider.GetRequiredService<MongoDbContext>();
    return new UnitOfWork(context.Database);
});

builder.Services.AddScoped<ICourierRepository, CourierRepository>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();

builder.Services.AddScoped<ICourierService, CourierService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(CreateCourierCommandHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(AssignCourierToDeliveryCommandHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(AssignDeliveryWindowCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ExceptionHandlingBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateCourierCommandValidator).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseMiddleware<GlobalExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
