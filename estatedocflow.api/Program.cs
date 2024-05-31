using AutoMapper;
using estatedocflow.api.AutoMapper;
using estatedocflow.api.Extensions;
using estatedocflow.api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Register DbContext with connection string from environment variable
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection") ??
                               builder.Configuration["POSTGRES_CONNECTION_STRING"];
builder.Services.AddDbContext<RealEstateDbContext>(
    options => options.UseNpgsql(postgresConnectionString));

// Configure RabbitMQ connection
var rabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitConnection") ??
                               builder.Configuration["RABBITMQ_CONNECTION_STRING"];
builder.Services.AddSingleton(sp =>
{
    var factory = new ConnectionFactory
    {
        Uri = new Uri(rabbitMqConnectionString!).ToString()
    };
    return factory.CreateConnection();
});

// Start Registering and Initializing AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new HouseProfile());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.ConfigureRepository();
builder.ConfigureService();

var app = builder.Build();

// Ensure database migrations are applied
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RealEstateDbContext>();
        context.Database.Migrate();  // Apply pending migrations
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();