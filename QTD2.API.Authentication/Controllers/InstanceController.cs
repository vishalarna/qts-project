using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Infrastructure.Model.Instance;

namespace QTD2.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class InstanceController : ControllerBase
    {
        private readonly IInstanceService _instanceService;
        private readonly IInstanceIdentityProviderLinkService _instanceIdentityProviderLinkService;
        private readonly IStringLocalizer<InstanceController> _localization;

        public InstanceController(IInstanceService instanceService, IStringLocalizer<InstanceController> localization, IInstanceIdentityProviderLinkService instanceIdentityProviderLinkService)
        {
            _instanceService = instanceService;
            _localization = localization;
            _instanceIdentityProviderLinkService = instanceIdentityProviderLinkService;
        }

        /// <summary>
        /// Gets a list of instances available to authenticated user
        /// </summary>
        /// <returns>Http Response Code with data</returns>
        [HttpGet]
        [Route("/instances")]
        public async Task<IActionResult> GetAsync()
        {
            var instances = await _instanceService.GetAsync();

            return Ok(new { instances });
        }

        /// <summary>
        /// Adds a new instance to the database
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with data</returns>
        [HttpPost]
        [Route("/instances")]
        public async Task<IActionResult> CreateAsync(InstanceCreateOptions options)
        {
            var instance = await _instanceService.CreateAsync(options);

            return Ok(new { instance });
        }

        /// <summary>
        /// Gets an instance by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Http Response Code with data</returns>
        [HttpGet]
        [Route("/instances/{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var instance = await _instanceService.GetAsync(name);

            return Ok(new { instance });
        }

        /// <summary>
        /// Updates an instance by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with data</returns>
        [HttpPut]
        [Route("/instances/{name}")]
        public async Task<IActionResult> UpdateAsync(string name, InstanceUpdateOptions options)
        {
            var instance = await _instanceService.UpdateAsync(name, options);
            return Ok(new { instance });
        }

        /// <summary>
        /// Deactivates an instance
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Http Response Code with message and </returns>
        [HttpDelete]
        [Route("/instances/{name}")]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            await _instanceService.DeleteAsync(name);
            return Ok(new { message = _localization["InstanceDeleted"].Value });
        }

        /// <summary>
        /// Creates a new database for the specific Instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/instances/{name}/database")]
        public async Task<IActionResult> CreateDatabaseAsync(string name, DatabaseCreateOptions options)
        {
            await _instanceService.CreateDatabaseAsync(name, options);

            return Ok(new { message = _localization["DatabaseCreated"].Value });
        }

        /// <summary>
        /// Updates the Instance database to the specified version
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPut]
        [Route("/instances/{name}/database")]
        public async Task<IActionResult> UpdateDatabaseAsync(string name, DatabaseUpdateOptions options)
        {
            await _instanceService.UpdateDatabaseAsync(name, options);
            return Ok(new { message = "DatabaseUpdated" });
        }

        [HttpGet]
        [Route("instances/{instanceName}/client")]
        public async Task<IActionResult> GetClientByInstanceNameAsync(string instanceName)
        {
            var client = await _instanceService.GetClientByInstanceNameAsync(instanceName);
            return Ok(client);
        }
    }
}
