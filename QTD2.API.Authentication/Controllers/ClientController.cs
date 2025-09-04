using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Application.Interfaces.Services.Authentication;

namespace QTD2.API.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ClientController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IClientService _clientService;
        private readonly IStringLocalizer<ClientController> _localization;

        public ClientController(
            IAuthorizationService authorizationService,
            IClientService clientService,
            IStringLocalizer<ClientController> localization)
        {
            _clientService = clientService;
            _authorizationService = authorizationService;
            _localization = localization;
        }

        /// <summary>
        /// Gets a list of clients available to authenticated user
        /// </summary>
        /// <returns>Http Response Code with client</returns>
        [HttpGet]
        [Route("/clients")]
        public async Task<IActionResult> GetClientsAsync()
        {
            var clients = await _clientService.GetByUserAsync(User.Identity.Name);
            return Ok(new { clients });
        }

        /// <summary>
        /// Adds a new client to the database
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/clients")]
        public async Task<IActionResult> CreateClientAsync(CreateClientOptions client)
        {
            var createdClient = await _clientService.CreateClientAsync(client);
            return Ok(new { createdClient });
        }

        /// <summary>
        /// Gets a client by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Http Response Code with client</returns>
        [HttpGet]
        [Route("/clients/{name}")]
        public async Task<IActionResult> GetClientAsync(string name)
        {
            var client = await _clientService.GetAsync(name);

            return Ok(new { client });
        }

        /// <summary>
        /// Updates a client
        /// </summary>
        /// <param name="name"></param>
        /// <param name="updatedName"></param>
        /// <returns>Http Response Code with message and client</returns>
        [HttpPut]
        [Route("/clients/{name}/{updatedName}")]
        public async Task<IActionResult> UpdateClientAsync(string name, string updatedName)
        {
            await _clientService.UpdateClientAsync(name, updatedName);
            return Ok(new { message = _localization["ClientUpdated"].Value });
        }

        /// <summary>
        /// Deactivates a client
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Http Response Code with message and client</returns>
        [HttpDelete]
        [Route("/clients/{name}")]
        public async Task<IActionResult> DeleteClientAsync(string name)
        {
            await _clientService.DeleteClientAsync(name);
            return Ok(new { message = _localization["ClientDeleted"].Value });
        }

        [HttpGet]
        [Route("/clients/all")]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var clients = await _clientService.GetAllClientsAsync(User.Identity.Name);
            return Ok(new { clients });
        }

        [HttpGet]
        [Route("/clients/{name}/instances")]
        public async Task<IActionResult> GetClientInstancesAsync(string name)
        {
            var instances = await _clientService.GetInstancesAsync(name);
            return Ok(new { instances });
        }
    }
}
