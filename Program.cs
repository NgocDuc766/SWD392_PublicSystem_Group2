using Microsoft.EntityFrameworkCore;
using SWD392_PublicService.Models;
using SWD392_PublicService.Services;

namespace SWD392_PublicService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Swd392PublicSystemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));
            builder.Services.AddScoped<ICaptchaService, CaptchaService>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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