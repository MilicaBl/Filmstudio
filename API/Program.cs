using API;
using API.Interfaces.IRepositories;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(opt=> opt.UseInMemoryDatabase("filmstudio"));
builder.Services.AddCors(options=>
    {
        options.AddDefaultPolicy(policy=>
        {
        policy.WithOrigins("http://127.0.0.1:5501")
        .AllowAnyHeader()
        .AllowAnyMethod();
        });
    });
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IFilmCopyRepository, FilmCopyRepository>();
builder.Services.AddScoped<IFilmStudioRepository, FilmStudioRepository>();
builder.Services.AddIdentity<FilmStudio, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
