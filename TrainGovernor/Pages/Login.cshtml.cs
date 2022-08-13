using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models.Login;
using Infrastructure.Interfaces.Controllers;
using System.ComponentModel.DataAnnotations;

namespace TrainGovernor.Pages
{
    public class LoginModel : PageModel
    {
        public string APPLICATION_NAME;
        public string APPLICATION_INTERNAL_NAME;

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        private ILoginController _loginController;

        public LoginModel(ILoginController controller, IConfiguration configuration)
        {
            _loginController = controller;
            APPLICATION_NAME = configuration.GetSection("ApplicationInfo").GetSection("ApplicationName").Value;
            APPLICATION_INTERNAL_NAME = configuration.GetSection("ApplicationInfo").GetSection("InternalAppName").Value;
        }

        public RedirectResult OnPost(string UserName, string Password)
        {
            var modelToSend = new UserLogin(UserName, Password);
            var token = _loginController.Login(modelToSend);

            if (token.StatusCode == StatusCodes.Status200OK)
            {
                return Redirect("/Home");
            }

            return Redirect("/Login");
        }
    }
}
