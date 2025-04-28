
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using GestorViajes.Repositories.Users;
using GestorViajes.Services.User;
using GestorViajes.Models.EFCore.Rove;
using GestorViajes.Services.Vehicle;
using GestorViajes.Services.Trip;
using GestorViajes.Services.User.GestorViajes.Services.User;
using GestorViajes.Repositories.Vehicles;
using GestorViajes.Repositories.Trips;

namespace GestorViajes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{env}.json", true, true)
                            .Build();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddNewtonsoftJson();
            builder.Services.AddSession();

            //Autenticacion
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();



            //Conexion a la base de datos
            builder.Services.AddDbContextFactory<RoveDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Rove"))
            );


            //Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<ITripRepository, TripRepository>();  

            // Servicios
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITripService, TripService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddAutoMapper(typeof(Program));

            //builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();


            var app = builder.Build();


            /*//autenticacion, modificar
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(50);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
            });*/                     


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
            //Importante el orden!
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
