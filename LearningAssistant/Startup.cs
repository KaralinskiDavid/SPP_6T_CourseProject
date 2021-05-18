using AutoMapper;
using LearningAssistant.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Dto.Identity;
using Microsoft.AspNetCore.Identity;
using Dao.Impl.DaoModels.Context;
using Service.Impl.Mapping;
using Service;
using Service.Impl;
using Dao;
using Dao.Impl;
using Dao.Impl.DaoModels;
using System.Collections.Generic;
using LearningAssistant.Filters;
using Dto.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.IdentityModel.Tokens;

namespace LearningAssistant
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

            var jwtOptions = Configuration.GetSection("Auth");
            services.Configure<JwtOptions>(jwtOptions);
            var opts = jwtOptions.Get<JwtOptions>();

            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            services.AddDbContext<DaoContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:LearningAssistantConnection"], b => b.MigrationsAssembly("LearningAssistant")));
            services.AddDbContext<LearningAssistantIdentityDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:LearningAssistantIdentityConnection"]));
            
            services.AddIdentity<LearningAssistantUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<LearningAssistantIdentityDbContext>();

            services.AddControllersWithViews();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LearningAssistant API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                c.SchemaFilter<ReadOnlySchemaFilter>();
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie("CookieAuthentication", config =>
            {
                config.Cookie.Name = "NoToken)";
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.ClaimsIssuer = opts.Issuer;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = opts.Issuer,
                        ValidateAudience = true,
                        ValidAudience = opts.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = opts.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddAuthorization();

            AddServices(services);
            AddRepositories(services);
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<IBsuirIisApiService, BsuirIisApiService>();
            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<ISpecialityService, SpecialityService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IQueueService, QueueService>();
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IFacultyDao<Faculty>, FacultyDao>();
            services.AddTransient<ISpecialityDao<Speciality>, SpecialityDao>();
            services.AddTransient<IGroupDao<Group>, GroupDao>();
            services.AddTransient<IStudentDao<Student>, StudentDao>();
            services.AddTransient<IScheduleDao<Schedule>, ScheduleDao>();
            services.AddTransient<IRefreshTokenDao<RefreshToken>, RefreshTokenDao>();
            services.AddTransient<IQueueDao<Queue>, QueueDao>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //var service = new BsuirIisApiService();
            //var currentWeek = await service.GetCurrentWeek();
            //var faculties = await service.GetFaculties();
            //var specialities = await service.GetSpecialities();
            //var groups = await service.GetGroups();
            //await service.GetGroupSchedule("851004");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LearningAssistant API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            await SeedData.EnsurePopulated(app);

        }
    }
}
