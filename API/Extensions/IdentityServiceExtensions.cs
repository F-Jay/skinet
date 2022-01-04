using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {

         public static IServiceCollection AddIdentityServices(this IServiceCollection services,
         IConfiguration config)
        {
            // Services - Ordering is not important.
           
           var _identityBuilder = services.AddIdentityCore<AppUser>();

           _identityBuilder = new IdentityBuilder(_identityBuilder.UserType, _identityBuilder.Services);

           _identityBuilder.AddEntityFrameworkStores<AppIdentityDbContext>();

           _identityBuilder.AddSignInManager<SignInManager<AppUser>>();

           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Configure Authentication Service
           .AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                   ValidIssuer = config["Token:Issuer"],
                   ValidateIssuer = true,
                   ValidateAudience = false
               };
           });

            return services;
        }
    }
}