using GestorViajes.Models.EFCore.GestionTurnos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using GestorViajes.Repositories.Viajes;
//using GestorViajes.Repositories.Vehiculos;
//using GestorViajes.Repositories.Users;
//using GestorViajes.Services.Roadtrip;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            builder.Services.AddControllersWithViews();
            //Conexion a la base de datos MySQL
            var connectionString = configuration.GetConnectionString("GestionTurnos");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            // DbContext (gestionturnosContext) para inyeccion de dependencias
            builder.Services.AddDbContextFactory<gestionturnosContext>(options =>
                options.UseMySql(connectionString, serverVersion)
            );
            // Repositories
            //builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IViajeRepository, ViajeRepository>();  

            // Servicios
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IViajeService, ViajeService>();
            //builder.Services.AddScoped<IVehiculoService, VehiculoService>();
            builder.Services.AddAutoMapper(typeof(Program));



            var app = builder.Build();

            
            /*// autenticacion, modificar
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
            });

            builder.Services.AddAutoMapper(typeof(Program)); 
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            */





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
