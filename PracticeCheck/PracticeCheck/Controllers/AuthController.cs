using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace PracticeCheck.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet]
        [Route("api/Auth/{id}")]
        public IActionResult Get(int id)
        {
            if(id == -1)
                return Ok(GenerateJSONWebToken(""));
            if(id == 1)
                return Ok(GenerateJSONWebToken("Admin"));
            else
                return Ok(GenerateJSONWebToken("Customer"));

        }

        private string GenerateJSONWebToken(string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole),
            };

            var token = new JwtSecurityToken(
                        issuer: "mySystem",
                        audience: "myUsers",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}