using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=medicamentos.db"));

var app = builder.Build();

app.MapControllers();

app.Run();