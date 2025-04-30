
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

            // Configuracion de la autenticacion con cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {    // Ruta de login
                    options.LoginPath = "/Account/Login";
                    // Ruta de acceso denegado
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    // Tiempo de expiración de la cookie
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                });

            // Configuracion de la conexion a la base de datos
            builder.Services.AddDbContextFactory<RoveDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Rove"))
            );

            // Repositorios
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<ITripRepository, TripRepository>();

            // Servicios
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITripService, TripService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddAutoMapper(typeof(Program));

            // Otros servicios
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Importante el orden! Asegurarse de usar el middleware de autenticacion antes de autorizacion
            app.UseAuthentication();
            app.UseAuthorization();

            // Rutas de controladores
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=IndexUser}/{id?}");

            app.Run();
        }
    }
}
