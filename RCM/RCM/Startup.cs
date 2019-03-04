using CRM.Data.Infrastructure;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;
using Quartz.Spi;
using RCM.Auth;
using RCM.CenterHubs;
using RCM.Data;
using RCM.Data.Repositories;
using RCM.Helpers;
using RCM.Identity;
using RCM.JWT;
using RCM.Mapster;
using RCM.Model;
using RCM.Service;
using System;

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

            //AssignedCollector
            services.AddTransient<IAssignedCollectorRepository, AssignedCollectorRepository>();
            services.AddTransient<IAssignedCollectorService, AssignedCollectorService>();

            //CollectionProgress
            services.AddTransient<ICollectionProgressRepository, CollectionProgressRepository>();
            services.AddTransient<ICollectionProgressService, CollectionProgressService>();

            //Contact
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactService, ContactService>();

            //Customer
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();

            //HubUserConnection
            services.AddTransient<IHubUserConnectionRepository, HubUserConnectionRepository>();
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();

            //Location
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<ILocationService, LocationService>();

            //Notification
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationService, NotificationService>();

            //Profile
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IProfileService, ProfileService>();

            //ProfileStage
            services.AddTransient<IProfileStageRepository, ProfileStageRepository>();
            services.AddTransient<IProfileStageService, ProfileStageService>();

            //ProfileStageAction
            services.AddTransient<IProfileStageActionRepository, ProfileStageActionRepository>();
            services.AddTransient<IProfileStageActionService, ProfileStageActionService>();

            //ProfileMessageForm
            services.AddTransient<IProfileMessageFormRepository, ProfileMessageFormRepository>();
            services.AddTransient<IProfileMessageFormService, ProfileMessageFormService>();

            //ProgressStage
            services.AddTransient<IProgressStageRepository, ProgressStageRepository>();
            services.AddTransient<IProgressStageService, ProgressStageService>();

            //ProgressStageAction
            services.AddTransient<IProgressStageActionRepository, ProgressStageActionRepository>();
            services.AddTransient<IProgressStageActionService, ProgressStageActionService>();

            //ProgressMessageForm
            services.AddTransient<IProgressMessageFormRepository, ProgressMessageFormRepository>();
            services.AddTransient<IProgressMessageFormService, ProgressMessageFormService>();

            //Receivable
            services.AddTransient<IReceivableRepository, ReceivableRepository>();
            services.AddTransient<IReceivableService, ReceivableService>();


            //Mail 
            services.AddTransient<IEmailService, EmailService>();
            #endregion

            services.AddTransient<IJobFactory, NotifyJobFactory>((provider) => new NotifyJobFactory(services.BuildServiceProvider()));
            services.AddTransient<NotifyJob>();

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
                    template: "{controller=Values}/{action=Index}/{id?}");
            });

            MapsterConfig map = new MapsterConfig();
            map.Run();
            new InitIdentity().CreateRoles(serviceProvider, Configuration).Wait();

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseQuartz((quartz) => quartz.AddJob<NotifyJob>("SendNotify", "Notify"));
        }
    }
}
