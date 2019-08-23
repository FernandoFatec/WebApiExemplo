using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApplication2.Model;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

       
        [HttpPost]
        public JsonResult Get([FromBody]UserLoginModel login)
        {
           
            var user = AuthenticateUser(login);

            if (user != null)
            {
                 
                return Json(GenerateJSONWebToken(user));
            }
            else
            {
                return Json(Unauthorized());
            }

           
        }

        private ReturnLogin GenerateJSONWebToken(UserLoginModel userInfo)
        {
            ReturnLogin returno = new ReturnLogin();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
        new Claim(ClaimTypes.Role, userInfo.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            returno.claims = claims;
            returno.token = new JwtSecurityTokenHandler().WriteToken(token); ;
            return returno;
        }

        private UserLoginModel AuthenticateUser(UserLoginModel login)
        {
            UserLoginModel user = null;

            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            if (login.Username == "Admin")
            {
                user = new UserLoginModel { Username = "Admin" };
            }
            return user;
        }


        
    }
}