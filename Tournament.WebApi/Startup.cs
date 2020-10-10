using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Tournament.DataAccess;
using Tournament.DataAccess.Core;
using Tournament.DataAccess.Interfaces;
using Tournament.DataAccess.Models;
using Tournament.WebApi.Policies;

namespace Tournament.WebApi
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
            services.AddControllers();

            // Add database service 
            services.AddDbContext<HollywoodDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TournamentApiDB")));

            services.AddScoped<IUnitOfWork<HollywoodDbContext>, UnitOfWork<HollywoodDbContext>>();

            services.AddScoped(typeof(IRepository<DataAccess.Models.Tournament>), typeof(EfRepository<HollywoodDbContext, DataAccess.Models.Tournament>));
            services.AddScoped(typeof(IRepository<Event>), typeof(EfRepository<HollywoodDbContext, Event>));
            services.AddScoped(typeof(IRepository<EventDetail>), typeof(EfRepository<HollywoodDbContext, EventDetail>));
            services.AddScoped(typeof(IRepository<EventDetailStatus>), typeof(EfRepository<HollywoodDbContext, EventDetailStatus>));

            // Add Identity service
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HollywoodDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Adding Authentication with Jwt Bearer  
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(UserRoles.Admin, Policy.AdminPolicy());
                config.AddPolicy(UserRoles.User, Policy.UserPolicy());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tournament REST API",
                    Description = "Used to create and update events related to a tournament"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tournament Web API v1");
            });

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
