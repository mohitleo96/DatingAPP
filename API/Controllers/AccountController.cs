using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController:BaseAPIController
    {
        private readonly AppUserDb _appUserDb;
        private readonly ITokenHandler _tokenservice;
     private async Task<bool> userExits(string username)
        {
          return await _appUserDb.AppUsers.AnyAsync(x=>x.UserName==username.ToLower());
        }
       public AccountController(AppUserDb appUserDb,ITokenHandler tokenservice)
      {
       _appUserDb=appUserDb;
       _tokenservice=tokenservice;

      }

      [HttpPost("Register")] //api/account/register
      //instead of returning the AppUser we return UserDTO
      // public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
       public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
      {
        if(await userExits(registerDto.username))
        {
          return BadRequest("Username already taken");
        }
        //we use using keyword because it dispose the method in Garbage collector after work is done
        using var hmac=new HMACSHA512();
        var user=new AppUser
        {
            UserName=registerDto.username.ToLower(),
            passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
            passwordSalt=hmac.Key

        };
        _appUserDb.AppUsers.Add(user);
        await _appUserDb.SaveChangesAsync();
        return new UserDto
        {
       UserName=user.UserName,
       Token=_tokenservice.CreateToken(user)
        };
      }
  [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDTO logindto)
    {
      //1.We need to get the user
      var user=await _appUserDb.AppUsers.SingleOrDefaultAsync(x=>
       x.UserName==logindto.UserName);

      //2.check is the user exicts or not
      if(user == null)
      {
        return Unauthorized("Invalid UserName");
      }

      //3.check if password is correct or not
      //converting passwordSalt to passwordHash for original password then check
      using var Hmac= new HMACSHA512(user.passwordSalt);
      var computedHash=Hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));


      //4.checking all possible passwords
      for(int i=0; i<computedHash.Length; i++)
      {
        if(computedHash[i]!=user.passwordHash[i])
        {
          return Unauthorized("Invalid Password");
        }
      }
       return new UserDto
        {
       UserName=user.UserName,
       Token=_tokenservice.CreateToken(user)
        };
      //4.

    }

    }
}