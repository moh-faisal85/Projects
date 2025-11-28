using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.Mappings;
using Training.DotNetCore.Project.API.Middlewares;
using Training.DotNetCore.Project.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Add / Inject Serilog Configurations
//Inject LoggerConfiguration
//var logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File(formatter: new Serilog.Formatting.Json.JsonFormatter(renderMessage: true),
//                "Logs/NzLogs_Log.json", 
//                rollingInterval: RollingInterval.Minute, 
//                shared: true)
//    //.MinimumLevel.Information() 
//    //.MinimumLevel.Warning() //debug, information log will be skipped to log
//    .MinimumLevel.Error()
//    /*
// Serilog follows a strict hierarchy of log levels: If a log level is specified, any levels below it in the hierarchy will not appear in the log output.
//    Level	    Severity	Description
//    Verbose	    Lowest	    Logs everything (detailed internal information). Rare in production.
//    Debug	    Low	        Diagnostic information useful for developers during debugging.
//    Information	Normal	    Successful events, app flow, business process updates.
//    Warning	    Medium	    Something unexpected happened, but app continues working.
//    Error	    High	    A failure occurred; unable to process a request.
//    Fatal	    Highest	    Application crash, critical failure, system shutdown.
// */
//    .CreateLogger();

//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

// Read Serilog from appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("UserName", Environment.UserName)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

#endregion

builder.Services.AddControllers();

//ASP.NET Core Web API Versioning
builder.Services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);
//AssumeDefaultVersionWhenUnspecified is assumes it will go to first version 1.0
//
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Enable Authorize header
builder.Services.AddSwaggerGen(Options =>
{
    Options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZ Walks API", Version = "v1" });
    Options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    Options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
#endregion

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
#region Inject Interface and Repostory Classes
//Map and Inject the interface with repository
//We are telling when object created for IRegionRepository, create it with SQLRegionRepository implementation.
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

//Inject Jwt Repository  class object
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//Switch from SQLRegionRepository to InMemoryRegionRepository
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();

//Inject image repository
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

#endregion

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

//Register Custom Middle
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
//Jwt Authentication
app.UseAuthentication();

app.UseAuthorization();

//Add below middleware to enable the access of static file using browser
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Logs")),
    RequestPath = "/Logs",
    ServeUnknownFileTypes = true // allows .txt, .log etc.
}
);

// Directory browsing (to list all files)
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Logs")),
    RequestPath = "/Logs"
});

app.MapControllers();

app.Run();
