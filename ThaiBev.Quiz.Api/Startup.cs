using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ThaiBev.Quiz.Api.Extensions;
using ThaiBev.Quiz.Api.Models;
using ThaiBev.Quiz.Api.Shared.Data;
using ThaiBev.Quiz.Api.Shared.Models;

namespace ThaiBev.Quiz.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<AppSettings>(Configuration);
            var appSettings = new AppSettings
            {
                CredentialSetting = Configuration.GetSection("Credential").Get<CredentialSetting>() ?? new CredentialSetting()
            };
            var swagger = appSettings.Swagger ?? new SwaggerSettings();

            #region Dependency Injection
            services.AddSingleton(appSettings.CredentialSetting);
            services.ConfigureScopeFacades(appSettings);
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = string.IsNullOrWhiteSpace(swagger.Title) ? "ThaiBev.Quiz.Api" : swagger.Title,
                    Version = string.IsNullOrWhiteSpace(swagger.Version) ? "v1" : swagger.Version,
                    Contact = new OpenApiContact
                    {
                        Name = swagger.Name,
                        Email = swagger.Email
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("AllowAngular");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThaiBev.Quiz.Api v1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}