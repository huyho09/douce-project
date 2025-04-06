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

            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;//true if a cookie must not be accessible by client-side script; otherwise, false.

                // Use Lax instead of None for SameSite when working with HTTP
                //options.Cookie.SameSite = SameSiteMode.None;//select None to work with Limo.
                options.Cookie.SameSite = SameSiteMode.Lax;//select None to work with Limo.

                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;//only https, None for both https and http
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;//only https, None for both https and http

                string st = builder.Configuration["SessionTimeOutInMinute"] ?? string.Empty;
                if (string.IsNullOrEmpty(st) == false)
                {
                    double SessionTimeout = Convert.ToDouble(st);
                    options.IdleTimeout = TimeSpan.FromMinutes(SessionTimeout);
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(SessionTimeout);

                    //options.Cookie.IsEssential = true;//Indicates if this cookie is essential for the application to function correctly. If true then consent policy checks may be bypassed. The default value is false.
                }

                var IsChangeSessionId = builder.Configuration.GetValue<bool>("ChangeSessionId");
                if (IsChangeSessionId)
                {
                    //Session Fixation, cookies session will change once session created
                    string prefix = builder.Configuration["CookieSessionPrefix"] ?? string.Empty;
                    options.Cookie.Name = prefix + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
                }
            });

            #region JWT

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                // Load JWT settings from configuration
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                var secretKey = jwtSettings["SecretKey"];
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];

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

            //builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            builder.Services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/");
                    options.AccessDeniedPath = new PathString("/system-logout");
                }
            );

            // Initialize Dependency Injection Helper after all services are registered
            DependencyInjectionHelper.Initialize(builder.Services.BuildServiceProvider());

            // Configure SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCookiePolicy();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always, // Change to None for HTTPS
                //Secure = CookieSecurePolicy.None,  // Change to None for HTTP
                MinimumSameSitePolicy = SameSiteMode.None,
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession(); // Add this line to use session

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCors("AllowAll");
            app.MapControllers();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            // Map endpoints
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

            // Add AutoMapper
            services.AddAutoMapper(typeof(Program));
        }
    }
}
