using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenHandler
    {
        //1 step Make Interface 2 step implement interface in services 3 step cofigure in program.cs
        //We're going to specify that we want to return a string and we're going to call our method create token,
        //and we're going to pass in as an argument the app user and call it user.
        String CreateToken(AppUser appUser);
    }
}