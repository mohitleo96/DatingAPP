using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    //we create class static because we no need to make instance/object of this class we directly call by its class name.
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {
           services.AddDbContext<AppUserDb>(opt =>
         {
           opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
         });
        //3rd step JWT
         services.AddScoped<ITokenHandler,tokenhandler>();
         return services;
        }

    }
}