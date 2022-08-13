using Infrastructure.Models.Login;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ILoginController
    {
        JsonResult Login(UserLogin loginModel);
    }
}