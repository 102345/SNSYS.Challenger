
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
using System.Globalization;
using SNSYS.Challenger.Api.Validators;
using Microsoft.OpenApi.Models;

namespace SNSYS.Challenger.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            var environmentName = Environment.GetEnvironmentVariable("CONSOLENETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                                 .Build();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIContagem", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

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


            builder.Services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddFluentValidation(conf =>
            {
                conf.RegisterValidatorsFromAssemblyContaining(typeof(CustomerSupplierValidator));
                conf.AutomaticValidationEnabled = false;
                conf.ValidatorOptions.LanguageManager.Culture = new CultureInfo("en-US");
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseRouting();

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
