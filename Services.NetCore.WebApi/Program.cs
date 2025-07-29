using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using Services.NetCore.WebApi.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Database Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "EfCore_dbo");
        sqlOptions.MigrationsAssembly("Services.NetCore.WebApi");
    }));

// 2. Dependency Injection (Container personalizado)
new Container(builder.Services);

// 3. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 4. Controllers + NewtonsoftJson
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddRazorPages().AddNewtonsoftJson();

// 5. Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "Services.NetCore.WebApi",
        Description = "This application is using .NET Core version 8",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Shayne Boyer",
            Email = "",
            Url = new Uri("https://twitter.com/spboyer")
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license")
        }
    });
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers(); // Mapea todos los controladores

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Services NetCore WebApi");
    c.RoutePrefix = string.Empty; // Swagger en la raíz: https://localhost:5001
});

// Run the app
app.Run();