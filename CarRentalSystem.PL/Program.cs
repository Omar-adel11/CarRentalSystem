using CarRentalSystem.BLL.Interfaces;
using CarRentalSystem.BLL.Repos;
using CarRentalSystem.DAL.Data.Contexts;
using CarRentalSystem.DAL.Models;
using CarRentalSystem.PL.DTO.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //allow DI for CarRepo
            builder.Services.AddScoped<ICarRepo, CarRepo>();
            //allow DI for DbContext
            builder.Services.AddDbContext<CarDbContexts>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<CarDbContexts>()
                .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(m=>m.AddProfile(new UserProfile()));

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
                config.LogoutPath = "/Account/SignOut";
                config.AccessDeniedPath = "/Account/AccessedDenied";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

            
        }
    }
}
