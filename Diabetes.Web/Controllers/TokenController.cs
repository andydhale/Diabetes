using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Web.Controllers
{
    public class TokenController : Controller
    {
        // todo - get key secrect from configuration
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a secret that needs to be at least 16 characters long"));

        [Route("/login")]
        public IActionResult Login(string username, string password)
        {
            // todo - check with identity context
            if (!string.IsNullOrEmpty(username))
            {
                return new ObjectResult(GenerateToken(username));
            }

            return Ok();
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Email, "test@test.com")
            };

            var token = new JwtSecurityToken(
                issuer: "Diabetes.Web",
                audience: "Diabetes.Client",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
