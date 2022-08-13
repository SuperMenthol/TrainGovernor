using Infrastructure.Interfaces.Controllers;
using Infrastructure.Models.Login;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class LoginController : ILoginController
    {
        [HttpPost]
        [Route("Login")]
        public JsonResult Login(UserLogin loginModel)
        {
            return new JsonResult("dupa");
        }
    }
}
