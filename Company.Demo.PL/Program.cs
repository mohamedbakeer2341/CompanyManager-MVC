using Company.Demo.BLL.Interfaces;
using Company.Demo.BLL.Repositories;
using Company.Demo.DAL.Data.Contexts;
using Company.Demo.DAL.Models;
using Company.Demo.PL.Mapping.Departments;
using Company.Demo.PL.Mapping.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<ApplicationDbContext>(); //// Allow dependency injection for ApplicationDbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Scoped and can take service lifetime as a parameter
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores < ApplicationDbContext>();
            var app = builder.Build();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=SignIn}/{id?}"
                );


            app.Run();
        }
    }
}
