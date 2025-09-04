using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Authentication;
using System.Threading.Tasks;

namespace QTD2.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityProviderController : ControllerBase
    {
        private readonly IIdentityProviderService _identityProviderService;
        public IdentityProviderController(IIdentityProviderService identityProviderService) 
        {
            _identityProviderService = identityProviderService;
        }

        [HttpGet]
        [Route("/identityprovider/{name}")]
        public async Task<IActionResult> GetIdentityProviderByNameAsync(string name)
        {
            var identityProvider = await _identityProviderService.GetIdentityProviderByNameAsync(name);
            return Ok(new { identityProvider });
        }
    }
}
