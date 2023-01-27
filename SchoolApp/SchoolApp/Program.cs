using Microsoft.EntityFrameworkCore;
using SchoolApp.Database;

var builder = WebApplication.CreateBuilder(args);

//aici facem comanda de legare la modificarea appsetting.Development.json
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();    

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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