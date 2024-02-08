
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SNSYS.Challenger.Domain.Repositories.CustomerSupplierRepository;
using SNSYS.Challenger.Domain.Repositories.User;
using SNSYS.Challenger.Domain.Services.CustomerSupplierService;
using SNSYS.Challenger.Domain.Services.Interfaces;
using SNSYS.Challenger.Domain.Services.User;
using SNSYS.Challenger.InfraStructure.Authorization;
using SNSYS.Challenger.InfraStructure.Data.Context;
using SNSYS.Challenger.InfraStructure.Interfaces;
using SNSYS.Challenger.InfraStructure.Repositories.CustomerSupplierRepository;
using SNSYS.Challenger.InfraStructure.Repositories.User;
using System.Text;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Api.Validators;

namespace SNSYS.Challenger.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var environmentName = Environment.GetEnvironmentVariable("CONSOLENETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                                 .Build();

            builder.Services.AddControllers().AddFluentValidation();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ChallengerSNSYSDbContext>(options =>
                             options.UseNpgsql(configuration.GetConnectionString("ChallengerDb")));


            ConfigServicesDependencyInjection(builder);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = "SNSYS.Challenger.Api",
                       ValidAudience = "SNSYS.Challenger.Api.Audience",
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtSecret")))
                   };
               });


            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto

            builder.Services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });



            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddScoped<IValidator<CustomerSupplierRequest>, CustomerSupplierValidator>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigServicesDependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthorizationJWT, AuthorizationJWT>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ICustomerSupplierRepository, CustomerSupplierRepository>();
            builder.Services.AddScoped<ICustomerSupplierService, CustomerSupplierService>();
            builder.Services.AddScoped<IUsersService, UsersService>();

        }
    }
}
