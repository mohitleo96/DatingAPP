using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class AddAuthButtonSwaggerServiceExtension
    {
     public static IServiceCollection AddAuthButtonSwaggerServices(this IServiceCollection services,
      IConfiguration config)
     {
       
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

       //For Showing the Authorize button on the top right of Swagger(!2Step)
       services.AddSwaggerGen(sw=>
    {
    sw.SwaggerDoc("v1", new OpenApiInfo{Title="dot6JWTAuthentication",Version="v1"});
    });

    services.AddSwaggerGen(s=>

       s.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
      {
      In=ParameterLocation.Header,
      Description="Insert JWT Token",
      Name="Authorization",
      Type=SecuritySchemeType.Http,
      BearerFormat="JWT",
      Scheme="bearer"
      }));

     services.AddSwaggerGen(w=>
     w.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
        {
            new  OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
               },
            new String[]{}
            }
          }
       ));
       return services;
     }
  }
}