using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.Business.Tools.JWTTool
{
    public class JwtService : IJwtService
    {
        public JwtToken GenerateJwt(AppUser appUser)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("adminadminadmin1"));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //issuer server audience client ip expires token süresi
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: "http://localhost:5000", audience: "http://localhost:27254", claims: SetClaims(appUser), notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(30), signingCredentials: signingCredentials);


            JwtToken jwtToken = new JwtToken();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            jwtToken.Token = handler.WriteToken(jwtSecurityToken);
            return jwtToken;
        }

        private List<Claim> SetClaims(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            return claims;
        }
    }
}
