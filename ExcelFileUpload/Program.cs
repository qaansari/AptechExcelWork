using AutoMapper;
using ExcelFileUpload.AppDbContext;
using ExcelFileUpload.Mapper;
using ExcelFileUpload.Services;
using ExcelFileUpload.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ExcelFileUpload
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSession();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //CS
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            //Http Service Injection
            builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            //Cookie Injection
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            //DI of Services
            builder.Services.AddScoped<IUserService, UserService>();
            //Mapper Configuration
            var config = new MapperConfiguration(config =>
            {
                config.AllowNullCollections = true;
                config.AddProfile(new MappingProfile());
            });
            builder.Services.AddSingleton(config.CreateMapper());
            //App-Build
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Index}/{id?}");

            app.Run();
        }
    }
}