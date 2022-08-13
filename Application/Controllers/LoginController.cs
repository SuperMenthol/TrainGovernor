using Domain.Entity.UserAuth;
using Domain.Interfaces.Context;
using Infrastructure.Interfaces.Controllers;
using Infrastructure.Models.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class LoginController : ILoginController
    {
        private IUserAuthContext _context;
        private ILogger<LoginController> _logger;
        private IConfiguration _configuration;

        private string _applicationName;

        private List<RolesForApplications> _rolesForApplication = new List<RolesForApplications>();

        public LoginController(IUserAuthContext context, ILogger<LoginController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _applicationName = _configuration.GetSection("ApplicationInfo").GetSection("InternalAppName").Value;

            ResolveApplicationId();
        }

        [HttpPost]
        [Route("Login")]
        public ObjectResult Login(UserLogin loginModel)
        {
            var user = Authenticate(loginModel);
            if (user != null)
            {
                var token = Generate(user);
                return new OkObjectResult(token);
            }

            //Create(loginModel);
            return new NotFoundObjectResult("Dupa");
        }

        private UserModel Authenticate(UserLogin loginModel)
        {
            var userInfo = _context.UserBaseInfo
                .Include(x => x.PersonalInfo)
                .Include(y => y.UserRoles)
                .Where(x => x.UserName.ToLower() == loginModel.UserName.ToLower()).FirstOrDefault();

            if (userInfo != null)
            {
                var comparisonResult = BCrypt.Net.BCrypt.Verify(loginModel.Password, userInfo.Password);
                if (!comparisonResult) { return null; }

                return new UserModel()
                {
                    UserName = loginModel.UserName,
                    FirstName = userInfo.PersonalInfo.FirstName,
                    LastName = userInfo.PersonalInfo.LastName,
                    Role = userInfo.UserRoles.Join(_rolesForApplication, x => x.RoleForApplicationId, y => y.Id, (x, y) => y).FirstOrDefault()?.Role.Name
                };
            }

            return null;
        }

        private string Generate(UserModel model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserName),
                new Claim(ClaimTypes.GivenName, String.Format("{0} {1}", model.FirstName, model.LastName)),
                new Claim(ClaimTypes.Role, model.Role ?? "")
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], 
                _configuration["Jwt:Audience"], 
                claims, 
                expires: DateTime.Now.AddMinutes(15), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void Create(UserLogin userLogin)
        {
            var pw = BCrypt.Net.BCrypt.HashPassword(userLogin.Password);
            _context.UserBaseInfo.Add(new UserBaseInfo()
            {
                UserName = userLogin.UserName,
                Password = pw,
                PersonalInfo = new UserPersonalInfo()
                {
                    FirstName = "Test",
                    LastName = "User",
                    Position = "Test entity",
                    Department = "Test department",
                }
            });

            _context.SaveChanges();
        }

        private bool ResolveApplicationId()
        {
            try
            {
                var application = _context.Applications
                    .Include(x => x.RolesForApplications)
                    .ThenInclude(y => y.Role)
                    .First(x => x.InternalName == _applicationName);

                _rolesForApplication = application.RolesForApplications.ToList();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
