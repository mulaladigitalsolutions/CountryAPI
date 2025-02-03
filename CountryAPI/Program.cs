using System;
using AutoMapper;
using Country.Infrastructure.DBContext;
using Country.Infrastructure.Repository;
using Country.Infrastructure.Repository.Interface;
using Country.Services.AutoMapper;
using Country.Services.Services;
using Country.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext with Scoped lifetime (default)
builder.Services.AddDbContext<CountryDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);  // Use Scoped instead of Singleton


// Add Swagger/OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories and services
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();

// AutoMapper configuration
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Description = "This API exposes a list of countries and their details",
        Contact = new OpenApiContact
        {
            Name = "Livhuwani Rambuda",
            Email = "livhuwanir@llrcorp.co.za",
            Url = new Uri("https://www.llrcorp.co.za/"),
        },
        Title = "Country API", 
        Version = "v1"
    });
});

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
