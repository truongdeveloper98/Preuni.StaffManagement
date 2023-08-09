using PreUni.StaffManagement.Core.Interfaces.Authentications;
using PreUni.StaffManagement.Infrastructure.Services.Authentications;
using PreUni.StaffManagement.Core.Services.Authentications;
using PreUni.StaffManagement.Core.Constants.Systems;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PreUni.StaffManagement.Infrastructure.Services.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Infrastructure.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Uses;
using PreUni.StaffManagement.Infrastructure.Repositories.Users;
using PreUni.StaffManagement.Core.Services.Users;
using PreUni.StaffManagement.Core.Interfaces.Services.Email;
using PreUni.StaffManagement.Infrastructure.Services.Email;
using PreUni.StaffManagement.Core.Configuration;
using PreUni.StaffManagement.Core.Interfaces;
using PreUni.StaffManagement.Core.Interfaces.Repositories;
using PreUni.StaffManagement.Infrastructure.Repositories;
using PreUni.StaffManagement.Infrastructure.Services;
using PreUni.StaffManagement.Core.Interfaces.Services.Users;
using Microsoft.AspNetCore.Hosting;
using PreUni.StaffManagement.Web.Models;
using FluentValidation;
using PreUni.StaffManagement.Core.Services.Validator;

namespace PreUni.StaffManagement.Web.DependencyInjections
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
            
            services.AddSingleton(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionsName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
                        
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ITFNDeclareRepository,TFNDeclareRepository>();

            services.AddScoped<ITFNDeclareService,TFNDeclareService>();

            services.AddScoped<IBankAccountService,BankAccountService>();

            services.AddScoped<IBankAccountRepository,BankAccountRepository>();

            services.AddScoped<IJobApplicationRespository, JobApplicationRespository>();

            services.AddScoped<IJobAppicationService, JobApplicationService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IParentConsentRepository, ParentConsentRepository>();
            services.AddScoped<IParentConsentService, ParentConsentService>();
            services.AddScoped<ISuperAnnuationService, SuperAnnuationService>();
            services.AddScoped<ISuperAnnuationRepository, SuperAnnuationRepository>();
            services.AddScoped<FormValidationService>();
        }
    }
}
