using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public class UserLoginModel
    {
            public string Username { get; set; }
            public string Password { get; set; }
            public string EmailAddress { get; set; }
            public DateTime DateOfJoing { get; set; }
      
    }

    public class ReturnLogin
    {
        public string token { get; set; }
        public Claim[] claims { get; set; }
    }
}
