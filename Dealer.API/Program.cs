
using AutoMapper;
using Dealer.API.Middleware;
using Dealer.API.Swagger.Filters;
using Dealer.Application.Interfaces;
using Dealer.Application.Mapping;
using Dealer.Application.Services;
using Dealer.Infrastructure.Data;
using Dealer.Infrastructure.Extensions;
using Dealer.Infrastructure.Services ;
using LoofNex.API.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog modern yapýlandýrma
builder.Host.UseSerilog((context, services, configuration) => configuration
	.ReadFrom.Configuration(context.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dealer API", Version = "v1" });
	c.OperationFilter<AuthorizeCheckOperationFilter>();
});

// Infrastructure DI (db, repos)
builder.Services.AddInfrastructure(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


// Application services DI
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();


// JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseMiddleware<JwtValidationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Seed data  eklenebilir
await app.Services.SeedDataAsync();

app.Run();
