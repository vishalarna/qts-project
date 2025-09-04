using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ClientUser;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUsersController : ControllerBase
    {
        private readonly IClientUserService _clientUserService;
        private readonly IStringLocalizer<ClientUsersController> _localizer;

        public ClientUsersController(IClientUserService clientUserService, IStringLocalizer<ClientUsersController> localizer)
        {
            _clientUserService = clientUserService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of ClientUsers
        /// </summary>
        /// <returns>Http Response code with client users</returns>
        [HttpGet]
        [Route("/clientsUsers")]
        public async Task<IActionResult> GetAsync()
        {
            var clientUsers = await _clientUserService.GetAsync();
            return Ok(new { clientUsers });
        }

        /// <summary>
        /// Gets a client user by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response code with client user</returns>
        [HttpGet]
        [Route("/clientsUsers/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var clientUser = await _clientUserService.GetAsync(id);
            return Ok(new { clientUser });
        }

        /// <summary>
        /// Creates a client user by name
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response code with client user</returns>
        [HttpPost]
        [Route("/clientsUsers/")]
        public async Task<IActionResult> CreateAsync(ClientUserCreateOptions options)
        {
            var clientUser = await _clientUserService.CreateAsync(options);
            return Ok(new { clientUser, message = _localizer["ClientUserCreated"] });
        }

        /// <summary>
        /// Deactivates a client user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response code with message</returns>
        [HttpDelete]
        [Route("/clientsUsers/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _clientUserService.DeleteAsync(id);
            return Ok(new { message = _localizer["ClientUserDeleted"] });
        }
    }
}
