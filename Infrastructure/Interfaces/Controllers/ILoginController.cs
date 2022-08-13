using Infrastructure.Models.Login;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Interfaces.Controllers
{
    public interface ILoginController
    {
        ObjectResult Login(UserLogin loginModel);
    }
}