using Training.NETCoreMVC.DrinkAndGo.Data.Mocks;
using Training.NETCoreMVC.DrinkAndGo.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IDrinkRepository, MockDrinkRepository>();
builder.Services.AddTransient<ICategoryRepository, MockCategoryRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseDeveloperExceptionPage();

app.UseStatusCodePages();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
