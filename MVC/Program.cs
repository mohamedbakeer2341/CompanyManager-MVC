using Company.Demo.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ApplicationDbContext>(); //// Allow dependency injection for ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); // Scoped and can take service lifetime as a parameter

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapControllerRoute(
    name : "default",
    pattern : "{controller}/{action}/{id?}"
    );

app.Run();
