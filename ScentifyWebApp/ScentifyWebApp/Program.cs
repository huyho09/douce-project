using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScentifyWebApp.DAL.DB;
using ScentifyWebApp.Helper;
using ScentifyWebApp.Services.Contracts;
using ScentifyWebApp.Services.Implementations;
using System.Text;

namespace ScentifyWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add logging for debugging startup issues
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Information);
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Configure services
            ConfigureServices(builder.Services);

            // Configure session
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;

                string st = builder.Configuration["SessionTimeOutInMinute"];
                if (!string.IsNullOrEmpty(st) && double.TryParse(st, out double sessionTimeout))
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeout);
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(sessionTimeout);
                }
                else
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30); // Default value
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                }

                var isChangeSessionId = builder.Configuration.GetValue<bool>("ChangeSessionId");
                if (isChangeSessionId)
                {
                    string prefix = builder.Configuration["CookieSessionPrefix"] ?? "ScentifySess_";
                    options.Cookie.Name = prefix + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                }
            });

            #region JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Use JWT for challenges
            })
            .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/");
                    options.AccessDeniedPath = new PathString("/system-logout");
                }
            )
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                var secretKey = jwtSettings["SecretKey"];
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];

                // Validate JWT settings
                if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 16 || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                {
                    throw new InvalidOperationException("JWT configuration is invalid. Ensure SecretKey (min 16 chars), Issuer, and Audience are set in appsettings.json.");
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
            #endregion JWT

            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();

            // Add IActionContextAccessor and IUrlHelper
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext!); // Null check handled by GetRequiredService
            });

            // Configure SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("DefaultConnection string is missing in appsettings.json")));

            var app = builder.Build();

            // Initialize DependencyInjectionHelper with the final service provider
            DependencyInjectionHelper.Initialize(app.Services);

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage(); // Better debugging in development
            }

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = app.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Lax // Align with session settings
            });

            // Initialize Dependency Injection Helper after all services are registered
            DependencyInjectionHelper.Initialize(builder.Services.BuildServiceProvider());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseSession();
            app.UseAuthentication(); // Must come before UseAuthorization
            app.UseAuthorization();

            // Map routes
            app.MapControllerRoute(
                name: "adminArea",
                pattern: "{area:regex(^Admin$)}/{controller=Login}/{action=Index}/{id?}"
            );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            app.MapControllers(); // For API controllers

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddScoped<ICartService, CartService>();
            services.AddHttpClient<IMomoService, MomoService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IBaseHttpContext, BaseHttpContext>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IAppTokenService, AppTokenService>();
            services.AddAutoMapper(typeof(Program));
        }
    }
}