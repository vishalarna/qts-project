using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Attributes;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace QTD2.Web.Admin.Controllers
{
    [TestControllerAttribute]
    public class PcController : Controller
    {
        IConfiguration _configuration;

        public PcController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var userName = _configuration.GetValue<string>("DevelopmentSettings:UserName");

            var claimsIdentity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            claimsIdentity.AddClaim(new Claim(Domain.CustomClaimTypes.Username, userName));

            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }
    }
}
