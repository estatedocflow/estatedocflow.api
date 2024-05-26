using AutoMapper;
using estatedocflow.api.AutoMapper;
using estatedocflow.api.Extensions;
using estatedocflow.api.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Register the DbContext
builder.Services.AddDbContext<RealEstateDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
