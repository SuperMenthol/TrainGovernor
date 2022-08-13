using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models.Login;
using Infrastructure.Interfaces.Controllers;
using System.ComponentModel.DataAnnotations;

namespace TrainGovernor.Pages
{
    public class LoginModel : PageModel
    {
        public const string APPLICATION_NAME = "Train Governor";
        public const string APPLICATION_INTERNAL_NAME = "TrainGov";

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        private ILoginController _loginController;

        public LoginModel(ILoginController controller)
        {
            _loginController = controller;
        }

        public void OnPost(string UserName, string Password)
        {
            var modelToSend = new UserLogin(UserName, Password);
            _loginController.Login(modelToSend);
        }
    }
}
