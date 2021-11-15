using EmailService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SportDataAuth.DbContext;
using SportDataAuth.Services;
using SportDataAuth.Services.AdminServices;
using SportDataAuth.Services.ApiExceptionsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService;

namespace SportDataAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Connection Server
            services.AddDbContext<AuthDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            
            // Identity
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                // Password
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequiredLength = 8;
                config.Password.RequiredUniqueChars = 8;

                // User
                config.User.RequireUniqueEmail = true;

                // SignIn
                config.SignIn.RequireConfirmedEmail = true;
                //config.SignIn.RequireConfirmedPhoneNumber = true;
                //config.SignIn.RequireConfirmedAccount = true;

                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);

            }).AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            // Authentication
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                // pass token on URL
                config.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Query.ContainsKey("access_token"))
                        {
                            context.Token = context.Request.Query["access_token"];
                        }
                        return Task.CompletedTask;
                    }
                };

                config.TokenValidationParameters = new TokenValidationParameters
                {               
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthTokenSettings:Key"])),
                    ValidIssuer = Configuration["AuthTokenSettings:Issuer"],
                    ValidAudience = Configuration["AuthTokenSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            // Controllers
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthServer", Version = "v1" });
            });

            // Services
            services.AddScoped<IMyAuthenticationService, MyAuthenticationService>();
            services.AddScoped<IMyAuthorizationService, MyAuthorizationService>();
            services.AddScoped<IApiExceptionsServices, ApiExceptionsServices>();

            // Token Service
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<TokenGenerator>();
            services.AddScoped<AccessTokenGenerator>();
            services.AddScoped<RefreshTokenGenerator>();
            services.AddScoped<RefreshTokenValidator>();
            services.AddScoped<UserValidator>();

            // Email
            var emailSettings = Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddTransient<IEmailSender>(_ => new EmailSender(emailSettings));

            // Files
            services.Configure<FormOptions>(config =>
            {
                config.ValueLengthLimit = int.MaxValue;
                config.MultipartBodyLengthLimit = int.MaxValue;
                config.MemoryBufferThreshold = int.MaxValue;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthServer v1"));
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
