using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class AddJWTAuthenticationServiceExtension
    {
        public static IServiceCollection AddJWTAuthenticationServices(this IServiceCollection services,IConfiguration config)
        {
            //JwtBearerDefaults to run this we should add one package called Microsoft.AspNetCore.Authentication.JwtBearer
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(option=>
            {
            option.TokenValidationParameters=new TokenValidationParameters
              {
             ValidateIssuerSigningKey=true,
             IssuerSigningKey=new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(
             config["TokenKey"])),
             ValidateIssuer=false,
             ValidateAudience=false
             };
            });
              return services;
        }
    }
}