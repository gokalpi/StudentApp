using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Data;
using StudentApp.V1.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace StudentApp.Helpers.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<StudentDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, StudentDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["JwtAuthentication:Issuer"],
                        ValidAudience = config["JwtAuthentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtAuthentication:SecretKey"])),
                        ClockSkew = TimeSpan.Zero,
                        SaveSigninToken = true
                    };
                });

            return services;
        }
    }
}