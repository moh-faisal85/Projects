using Microsoft.EntityFrameworkCore;
using Training.DotNetCore.Project.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Map DB Context Class, Map connection strings
builder.Services.AddDbContext<NZWalksDbContext>
    (
        dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer
        (
            builder.Configuration.GetConnectionString("NZWalksConnectionString")//Read value from AppSettings
        )
    );

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
