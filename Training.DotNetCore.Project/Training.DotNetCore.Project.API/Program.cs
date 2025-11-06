using Microsoft.EntityFrameworkCore;
using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.Mappings;
using Training.DotNetCore.Project.API.Repositories;

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
//Map and Inject the interface with repository
//We are telling when object created for IRegionRepository, create it with SQLRegionRepository implementation.
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository> ();
//Switch from SQLRegionRepository to InMemoryRegionRepository
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();

//Scan Automapper Profiles
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
