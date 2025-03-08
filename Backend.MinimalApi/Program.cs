using Backend.MinimalApi.Endpoints;
using Backend.MinimalApi.Interfaces;
using Backend.MinimalApi.Persistence;
using Backend.MinimalApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("AppDb"));


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("ADMIN")); // Kräver att användaren har rollen "ADMIN"
});


builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityApiEndpoints<IdentityUser>();

//If this was a real service connected to a database, the recommended lifetime would be scoped, or in some cases transient
builder.Services.AddSingleton<IAnimalService, AnimalService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontendPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7296");
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("frontendPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapIdentityApi<IdentityUser>();

app.MapAnimalEndpoints();
app.MapCustomAuthEndpoints();


app.Run();

