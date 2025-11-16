using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

//Map DB Context Class, Map connection strings
builder.Services.AddDbContext<NZWalksAuthDbContext>
    (
        dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer
        (
            builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")//Read value from AppSettings
        )
    );

//Map and Inject the interface with repository
//We are telling when object created for IRegionRepository, create it with SQLRegionRepository implementation.
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

//Inject Jwt Repository  class object
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//Switch from SQLRegionRepository to InMemoryRegionRepository
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();

//Scan Automapper Profiles
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
//Jwt Role Inject - Setup identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


//Jwt Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//Jwt Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
