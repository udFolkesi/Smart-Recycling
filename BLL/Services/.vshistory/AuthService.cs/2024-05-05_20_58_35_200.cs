using CORE.Models;
using DAL.Contexts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService
    {
        private SmartRecyclingDbContext dbContext;
        public AuthService(SmartRecyclingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public dynamic GetResponse(string email, string password, ClaimsIdentity identity)
        {
            var key = new SymmetricSecurityKey(
                Encoding.ASCII
                .GetBytes("mysupersecret_secretkey!@@@123456"));

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: "https://localhost:7178",
                    audience: "https://localhost:7178",
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(5)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                email = identity.Name,
            };

            return response;
        }

        public ClaimsIdentity GetIdentity(string email, string password)
        {
            User? user = dbContext.User.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
