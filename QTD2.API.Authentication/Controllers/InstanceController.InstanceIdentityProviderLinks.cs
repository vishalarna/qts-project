using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QTD2.API.Authentication.Controllers
{
    public partial class InstanceController : ControllerBase
    {
        [HttpGet]
        [Route("/instances/{name}/identityprovider")]
        public async Task<IActionResult> GetInstanceIdentityProviderListByInstanceName(string name)
        {
            var identityProviders = await _instanceIdentityProviderLinkService.GetInstanceIdentityProviderListByInstanceName(name);
            return Ok(new { identityProviders });
        }
    }
}
