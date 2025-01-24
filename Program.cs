using EFAPIProject.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add context
var connectionString = builder.Configuration.GetConnectionString("ConnectionDefault");
var mysqlVersion = Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql");

builder.Services.AddDbContext<DesafioidatadbContext>(options =>
{
    options.UseMySql(connectionString, mysqlVersion);
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
