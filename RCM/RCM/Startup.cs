using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data.Infrastructure;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;
using RCM.Auth;
using RCM.Data;
using RCM.CenterHubs;
using RCM.Identity;
using RCM.JWT;
using RCM.Mapster;
using RCM.Model;
using RCM.Service;
using static RCM.Helpers.String;
using Microsoft.AspNetCore.Http;
using RCM.Data.Repositories;
using NSwag;

namespace RCM
{


    public class Startup
    {
        private readonly SymmetricSecurityKey _signingKey = JwtSecurityKey.Create();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RCMContext>();
            #region DI Solutions
            //add for data
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Customer
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();


            //Notification
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationService, NotificationService>();

            //HubUserConnection
            services.AddTransient<IHubUserConnectionRepository, HubUserConnectionRepository>();
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();


            //Mail 
            services.AddTransient<IEmailService, EmailService>();
            #endregion

            #region Auth
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                            ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                            IssuerSigningKey = _signingKey
                        };
                    });

            // api user claim policy
            services.AddAuthorization();
            services.AddScoped<RoleManager<IdentityRole>>();
            // add identity
            var authBuilder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            authBuilder = new IdentityBuilder(authBuilder.UserType, typeof(IdentityRole), authBuilder.Services);
            authBuilder.AddEntityFrameworkStores<RCMContext>().AddDefaultTokenProviders();
            #endregion

            // add cors
            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()
            ));

            // other
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;

            });
            // Register the Swagger services
            services.AddSwaggerDocument(c =>
            {
                c.DocumentName = "RCM-Api-Docs";
                c.Title = "RCM-API";
                c.Version = "v3";
                c.Description = "The RCM API documentation description.";
                c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copy 'Bearer ' + valid JWT token into field",
                    In = SwaggerSecurityApiKeyLocation.Header

                }));
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
            });
            services.AddHangfire(x => x.UseSqlServerStorage(@"Server=202.78.227.91;Database=rcm-hangfire;user id=rcm;password=zaq@123;Trusted_Connection=True;Integrated Security=false;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares



            app.UseSwagger(settings =>
            {
                new NSwag.SwaggerGeneration.SwaggerJsonSchemaGenerator(new NJsonSchema.Generation.JsonSchemaGeneratorSettings());
                //settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("apiKey", new NSwag.SwaggerSecurityScheme()
                //{
                //    Type = NSwag.SwaggerSecuritySchemeType.ApiKey,
                //    Name = "Authorization",
                //    In = NSwag.SwaggerSecurityApiKeyLocation.Header,
                //    Description = "Bearer token"
                //}));
            });
            app.UseSwaggerUi3();

            app.UseCors("AllowAll");
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSignalR(routes =>
            {
                routes.MapHub<CenterHub>("/centerHub");


            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            MapsterConfig map = new MapsterConfig();
            map.Run();
            new InitIdentity().CreateRoles(serviceProvider, Configuration).Wait();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
