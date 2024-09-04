using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.Core.Services;
using BE.NET.As.LMS.Infrastructures;
using BE.NET.As.LMS.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllers()
                    .AddJsonOptions(options =>
             options.JsonSerializerOptions.Converters.Add(new CustomConverter()));
            services.AddOptions();
            var mailsettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSetting>(mailsettings);
            services.AddIdentity<User, Role>()
                      .AddEntityFrameworkStores<LMSDataContext>()
                      .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.AddAuthentication()
               .AddGoogle(googleOptions =>
               {
                   IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                   googleOptions.ClientId = googleAuthNSection["ClientId"];
                   googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
               })
             .AddFacebook(facebookOptions => {
                  IConfigurationSection facebookAuthNSection = Configuration.GetSection("Authentication:Facebook");
                  facebookOptions.AppId = facebookAuthNSection["AppId"];
                  facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
              });
            services.AddScoped(typeof(IRepository<>), typeof(ERepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient<UserManager<User>, UserManager<User>>();
            services.AddTransient<SignInManager<User>, SignInManager<User>>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IAuthenticationServices, AuthenticationServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ICourseServices, CourseServices>();
            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IHomeServices, HomeServices>();
            services.AddTransient<IAdminServices, AdminServices>();
            services.AddTransient<ISectionServices, SectionServices>();
            services.AddTransient<ILessonServices, LessonServices>();
            services.AddTransient<IFileStorageServices, FileStorageServices>();
            services.AddTransient<ICommentServices, CommentServices>();
            services.AddTransient<IAssignmentServices, AssignmentServices>();
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IOrderDetailServices, OrderDetailServices>();
            services.AddTransient<INoteServices, NoteServices>();
            services.AddTransient<IDescriptionDetailServices, DescriptionDetailServices>();
            services.AddTransient<IQuizServices, QuizServices>();
            services.AddTransient<IQuizUserServices, QuizUserServices>();
            services.AddTransient<INotificationServices, NotificationServices>();
            services.AddTransient<INotificationUserService, NotificationUserService>();
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BE.NET.As.LMS", Version = "v1" });
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
            });
            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:SecretKey");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = issuer,
                       ValidateAudience = true,
                       ValidAudience = issuer,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ClockSkew = TimeSpan.Zero,
                       IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                   };
               });
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<LMSDataContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
                });
            }
            else
            {
                // production or staging
                services.AddDbContext<LMSDataContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
                });
            }
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

            }
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true).AllowCredentials());
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BE.NET.As.LMS v1"));
            app.UseStaticFiles();
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
