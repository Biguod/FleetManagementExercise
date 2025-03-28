using FleetManagement.Infraestructure.Context;
using FleetManagement.Web.Configuration;
using Microsoft.EntityFrameworkCore;
using FleetManagement.Infraestructure.Extensions;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.
builder.Services.AddServiceDependency();
builder.Services.AddRepositoryDependency();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FleetManagementDbContext>(options =>
{
    options.UseInMemoryDatabase("FleetManagementDb");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FleetManagementDbContext>();
    var seeder = new DataSeeding(context);
    seeder.SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddEndpoints();

app.UseHttpsRedirection();

app.Run();
