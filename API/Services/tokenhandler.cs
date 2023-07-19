using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class tokenhandler : ITokenHandler
    {//2nd step 1step interface
        //And we're going to store our super secret key that we're going to use to sign our token.
         //We're going to store that in our configuration.
         //And in order to get our configuration and inject it into the service,
         //then we need to inject IConfiguration

 //There's two types of keys really used in cryptography.There's a symmetric key where the same key is used to encrypt the data as is used to decrypt the data.
// And since our server is responsible for both signing the token and decrypting the token or the token.
// The other type of key is asymmetric, and that's when your server needs to encrypt something.
// And the client also needs to decrypt something.
// And on that basis, we have a public and a private key.
// In this case, we can just use a symmetric security key because this is going to stay on the server
// and never go to the client because the client does not need to decrypt this key.
           private readonly SymmetricSecurityKey _key;
         public tokenhandler(IConfiguration config)
         {
          _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
         }
         //1 list of claims
        public string CreateToken(AppUser appUser)
        {
           var claims = new List<Claim>
           {
            new Claim(JwtRegisteredClaimNames.NameId , appUser.UserName)
           };

           //we need some signing creadinals
           var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

           //Next part is to describe the token that we're going to return.
           var tokenDescriptor=new SecurityTokenDescriptor
           {
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime.Now.AddDays(7),
            SigningCredentials=creds
           };
           var tokenHandler= new JwtSecurityTokenHandler();
           var token=tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }
    }
}
//next we create new DTO to return username and Token only, not passwork hash or salt