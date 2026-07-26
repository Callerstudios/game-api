using GameApi.Data;
using GameApi.Repositories.Implementations;
using GameApi.Repositories.Interfaces;
using GameApi.Services.Implementations;
using GameApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();

builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=game.db");
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
