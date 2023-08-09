using PreUni.StaffManagement.Core.Interfaces.Authentications;
using PreUni.StaffManagement.Infrastructure.Services.Authentications;
using PreUni.StaffManagement.Core.Services.Authentications;
using PreUni.StaffManagement.Core.Services.Users;
using PreUni.StaffManagement.Core.Constants.Systems;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Uses;
using PreUni.StaffManagement.Infrastructure.Repositories.Users;
using PreUni.StaffManagement.Infrastructure.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.JobApplications;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Infrastructure.Services.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Users;
using PreUni.StaffManagement.Core.Services.Validator;

namespace PreUni.StaffManagement.AdminWeb.DependencyInjections
{
    public static class ServiceRegistrations
    {

        public static void ConfigureAppServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                         options.UseSqlServer(configuration.GetConnectionString(SystemConstants.MainConnectionString)));

            services.AddIdentity<AppUser, AppRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();

            //builder.Services DI
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionsName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobApplicationService, PreUni.StaffManagement.Core.Services.JobApplications.JobApplicationService>();
            services.AddScoped<IJobApplicationRespository, JobApplicationRespository>();
            services.AddScoped<ITFNDeclareService, TFNDeclareService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ITFNDeclareRepository, TFNDeclareRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<FormValidationService>();

            string issuer = configuration.GetValue<string>("JwtSettings:Issuer") ?? string.Empty;
            string signingKey = configuration.GetValue<string>("JwtSettings:Secret")  ?? string.Empty;
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
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });

            services.AddCors(option =>
            {
                option.AddPolicy("_myAllowSpecificOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });


        }
    }
}
